using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.Data;
using IMEVENT.SharedEnums;

namespace IMEVENT.Events
{
    public class DataMatchingGenerator
    {
        #region private data
        //Is the data loaded from the DB
        public bool IsAllDataLoaded
        {
            get
            {
                return this.IsAttendeeInfoLoaded && this.IsSeatsDataLoaded 
                       && this.IsBedsDataLoaded && this.IsTablesDataLoaded;
            }
        }
               
        public Event CurrentEvent { get; set; }
        //Constructor
        public DataMatchingGenerator(Event ev)
        {
            this.CurrentEvent = ev;
            InvalidateAllData();
        }

        //Test Constructor
        public void LoadDataInMatchingGenerator(Dictionary<string, EventAttendee> participants, Dictionary<string, User> participantsInfo, 
            Dictionary<int, Hall> seats, Dictionary<int, Dormitory> beds, Dictionary<int, Refectory> refs,
            Dictionary<int, Table> tables)
        {
            attendees = participants;
            attendeesInfo = participantsInfo;
            seatsInHall = seats;
            bedsInDorms = beds;
            refectories = refs;
            tablesInRefs = tables;            
        }                              

        //Input attendees
        private Dictionary<string, EventAttendee> attendees;
        public Dictionary<string, EventAttendee> Attendees
        {
            get
            {
                EnsureLoaded();
                return attendees;
            }
        }

        //Attendees Info
        private Dictionary<string, User> attendeesInfo;
        public Dictionary<string, User> AttendeesInfo
        {
            get
            {
                EnsureLoaded();
                return attendeesInfo;
            }
        }

        private Dictionary<int, Hall> seatsInHall;
        public Dictionary<int, Hall> SeatsInHall
        {
            get
            {
                EnsureLoaded();
                return seatsInHall;
            }
        }

        private Dictionary<int, Dormitory> bedsInDorms;
        public Dictionary<int, Dormitory> BedsInDorms
        {
            get
            {
                EnsureLoaded();
                return bedsInDorms;
            }
        }

        private Dictionary<int, Refectory> refectories;
        public Dictionary<int, Refectory> Refectories
        {
            get
            {
                EnsureLoaded();
                return refectories;
            }
        }

        private Dictionary<int, Table> tablesInRefs;
        public Dictionary<int, Table> TablesInRefs
        {
            get
            {
                EnsureLoaded();
                return tablesInRefs;
            }
        }

        public Dictionary<HallSectionTypeEnum, Stack<HallEntry>> ListAvailableSections;
        public Dictionary<DormitoryTypeEnum, Stack<DormEntry>> ListAvailableDorms;
        public Dictionary<RegimeEnum, Stack<TableEntry>> ListAvailableTables;

        private void EnsureLoaded()
        {
            if (this.IsAllDataLoaded)
            {                
                return; //data already loaded
            }

            //Get list of attendees
            this.attendees = EventAttendee.GetAttendeeList(this.CurrentEvent.Id);
            if (this.attendees == null)
            {
                throw new System.NullReferenceException(string.Format("No Attendee registered yet for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            
            //Get attendees information
            this.attendeesInfo = User.GetRegisteredUsersPerEventId(this.CurrentEvent.Id);
            if (this.attendeesInfo == null)
            {
                throw new System.NullReferenceException(string.Format("Infos not available for registered users for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsAttendeeInfoLoaded = true;

            //Get list of seats
            this.seatsInHall = Hall.GetHallSections(this.CurrentEvent.Id);
            if (this.seatsInHall == null)
            {
                throw new System.NullReferenceException(string.Format("Seats not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsSeatsDataLoaded = true; 

            //Get list of beds
            this.bedsInDorms = Dormitory.GetDormitoryList(this.CurrentEvent.Id);
            if (this.bedsInDorms == null)
            {
                throw new System.NullReferenceException(string.Format("Beds not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsBedsDataLoaded = true; 

            //Get list of refectories
            this.refectories = Refectory.GetRefectoryList(this.CurrentEvent.Id);
            if (this.refectories == null)
            {
                throw new System.NullReferenceException(string.Format("Tables not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }            

            //Get list of tables
            this.tablesInRefs = Table.GetTableList(this.CurrentEvent.Id);
            if (this.tablesInRefs == null)
            {
                throw new System.NullReferenceException(string.Format("Tables not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsTablesDataLoaded = true;
        }

        private bool isSeatsDataLoaded;
        public bool IsSeatsDataLoaded
        {
            get
            {
                return isSeatsDataLoaded;
            }

            set
            {
                isSeatsDataLoaded = value;
            }
        }

        private bool isBedsDataLoaded;
        public bool IsBedsDataLoaded
        {
            get
            {
                return isBedsDataLoaded;
            }

            set
            {
                isBedsDataLoaded = value;
            }
        }

        private bool isTablesDataLoaded;
        public bool IsTablesDataLoaded
        {
            get
            {
                return isTablesDataLoaded;
            }

            set
            {
                isTablesDataLoaded = value;
            }
        }

        private bool isAttendeeInfoLoaded;
        public bool IsAttendeeInfoLoaded
        {
            get
            {
                return isAttendeeInfoLoaded;
            }

            set
            {
                isAttendeeInfoLoaded = value;
            }
        }
        #endregion

        #region Invalidate data
        private void InvalidateAllData()
        {
            this.IsAttendeeInfoLoaded = this.IsSeatsDataLoaded = 
                this.IsBedsDataLoaded = this.IsTablesDataLoaded = false;
        }        
        #endregion        

        #region Map Attendees to Seat in Halls        

        protected HallEntry GetHallEntry(HallSectionTypeEnum sectionType, int? index = null)
        {
            if(this.ListAvailableSections.IsNullOrEmpty() 
               || !this.ListAvailableSections.ContainsKey(sectionType)
               || this.ListAvailableSections[sectionType].IsNullOrEmpty())
            {
                Console.WriteLine("GetHallEntry(): No seat available!");
                return null;
            }

            return index == null ? this.ListAvailableSections[sectionType].Pop() 
                                 : this.ListAvailableSections[sectionType].ElementAt((int)index);
        }        

        public Stack<HallEntry> GetSeatsPerCategoryType(Dictionary<int, Hall> inputSeatList)
        {            
            Dictionary<int, Section> listofSeats = new Dictionary<int, Section>();
            try
            {
                int index = 0;
                foreach (KeyValuePair<int, Hall> seat in inputSeatList)
                {
                    for (int j = 1; j <= seat.Value.Capacity; j++)
                    {
                        index++;
                        listofSeats[index] = new Section
                        {
                            Id = seat.Value.Id,
                            PlaceNbr = j
                        };
                    }
                }

                //Shuffle Ids
                List<int> seatsIds = new List<int>(listofSeats.Keys);
                seatsIds.Shuffle();

                //do the assignement
                Stack<HallEntry> outputSeatList = new Stack<HallEntry>();
                foreach (int seatID in seatsIds)
                {
                    outputSeatList.Push(new HallEntry
                    {                        
                        HallId = listofSeats[seatID].Id,
                        SeatNbr = listofSeats[seatID].PlaceNbr
                    });
                }

                return outputSeatList;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in generating Seats per category" + ex);
                return null;
            }           
        }

        public bool GenerateSeatsForMatching(bool mingle = false)
        {            
            //if mingle true, no need to break per category
            Stack<HallEntry> tempStack;
            this.ListAvailableSections = new Dictionary<HallSectionTypeEnum, Stack<HallEntry>>();
            if (mingle)
            {
                tempStack = GetSeatsPerCategoryType(this.SeatsInHall);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating seats per category with participants mingle");
                    return false;
                }

                this.ListAvailableSections[HallSectionTypeEnum.NONE] = tempStack;
                return true;
            }

            //Break the list per category if mingle false
            Dictionary<HallSectionTypeEnum, Dictionary<int, Hall>> tempDict = new Dictionary<HallSectionTypeEnum, Dictionary<int, Hall>>();
            foreach(KeyValuePair<int,Hall> section in this.SeatsInHall)
            {
                if(!tempDict.ContainsKey(section.Value.HallType))
                {
                    tempDict[section.Value.HallType] = new Dictionary<int, Hall>
                    {
                        { section.Value.Id, section.Value }
                    };
                }
                else
                {
                    tempDict[section.Value.HallType][section.Value.Id] = section.Value;
                }                
            }

            // Match per category
            foreach(KeyValuePair<HallSectionTypeEnum, Dictionary<int, Hall>> cat in tempDict)
            {
                tempStack = GetSeatsPerCategoryType(cat.Value);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating seats per category with participants not mingle");
                    return false;
                }
                this.ListAvailableSections[cat.Key] = tempStack;
            }
            
            return true;
        }
        #endregion

        #region Map Attendees to Beds in Dorms  
              
        protected DormEntry GetDormEntry(DormitoryTypeEnum dormType, int? index = null)
        {
            if (this.ListAvailableDorms.IsNullOrEmpty() 
                || !this.ListAvailableDorms.ContainsKey(dormType)
                || this.ListAvailableDorms[dormType].IsNullOrEmpty())
            {
                Console.WriteLine("GetDormEntry(): No bed available!");
                return null;
            }

            return index == null ? this.ListAvailableDorms[dormType].Pop()
                                 : this.ListAvailableDorms[dormType].ElementAt((int)index);
        }
                
        public Stack<DormEntry> GetBedsPerCategoryType(Dictionary<int, Dormitory> inputBedList)
        {
            Dictionary<int, Section> listofBeds = new Dictionary<int, Section>();
            try
            {
                //build the list of beds Indexes
                int index = 0;
                foreach (KeyValuePair<int, Dormitory> dorm in inputBedList)
                {
                    for (int j = 1; j <= dorm.Value.Capacity; j++)
                    {
                        index++;
                        listofBeds[index] = new Section
                        {
                            Id = dorm.Value.Id,
                            PlaceNbr = j
                        };
                    }
                }

                //Shuffle Indexes
                List<int> bedsIds = new List<int>(listofBeds.Keys);
                bedsIds.Shuffle();

                //Perform the assignement
                Stack<DormEntry> outputBedList = new Stack<DormEntry>();
                foreach (int bedId in bedsIds)
                {
                    outputBedList.Push(new DormEntry
                    {                        
                        IdDormitory = listofBeds[bedId].Id,
                        BedNbr = listofBeds[bedId].PlaceNbr
                    });
                }

                return outputBedList;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in generating Beds per category" + ex);
                return null;
            }
        }

        public bool GenerateBedsForMatching(bool mingle = false)
        {
            //if mingle true, no need to break per category            
            Stack<DormEntry> tempStack;
            this.ListAvailableDorms = new Dictionary<DormitoryTypeEnum, Stack<DormEntry>>();
            if (mingle)
            {
                tempStack = GetBedsPerCategoryType(this.BedsInDorms);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating beds per category with participants mingle");
                    return false;
                }

                this.ListAvailableDorms[DormitoryTypeEnum.NONE] = tempStack;
                return true;
            }

            //Break the list per category if mingle false
            Dictionary<DormitoryTypeEnum, Dictionary<int, Dormitory>> tempDict = new Dictionary<DormitoryTypeEnum, Dictionary<int, Dormitory>>();
            foreach (KeyValuePair<int, Dormitory> bed in this.BedsInDorms)
            {
                if (!tempDict.ContainsKey(bed.Value.DormType))
                {
                    tempDict[bed.Value.DormType] = new Dictionary<int, Dormitory>
                    {
                        { bed.Value.Id, bed.Value }
                    };
                }
                else
                {
                    tempDict[bed.Value.DormType][bed.Value.Id] = bed.Value;
                }
            }

            // Match per category
            foreach (KeyValuePair<DormitoryTypeEnum, Dictionary<int,Dormitory>> cat in tempDict)
            {
                tempStack = GetBedsPerCategoryType(cat.Value);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating beds per category with participants not mingle");
                    return false;
                }
                this.ListAvailableDorms[cat.Key] = tempStack;
            }

            return true;
        }

        #endregion

        #region Map Attendees to Tables in Refectories        

        protected TableEntry GetTableEntry(RegimeEnum refType, int? index = null)
        {
            if (this.ListAvailableTables.IsNullOrEmpty()
                || !this.ListAvailableTables.ContainsKey(refType)
                || this.ListAvailableTables[refType].IsNullOrEmpty())
            {
                Console.WriteLine("GetTableEntry(): No table available!");
                return null;
            }

            return index == null ? this.ListAvailableTables[refType].Pop()
                                 : this.ListAvailableTables[refType].ElementAt((int)index);
        }
        
        public Stack<TableEntry> GetTablesPerCategoryType(Dictionary<int, Table> inputTableList)
        {
            Dictionary<int, Section> listofTableSeats = new Dictionary<int, Section>();
            try
            {
                int index = 0;
                foreach (KeyValuePair<int, Table> table in inputTableList)
                {                    
                    for(int i = 1; i <= table.Value.Capacity; i++)
                    {                            
                        listofTableSeats[++index] = new Section
                        {
                            Id = table.Value.Id,
                            IdRef = table.Value.RefectoryId,                            
                            PlaceNbr = i
                        };
                    }                                            
                }

                //Shuffle Ids
                List<int> tablesIds = new List<int>(listofTableSeats.Keys);
                tablesIds.Shuffle();

                //do the assignement
                Stack<TableEntry> outputTableList = new Stack<TableEntry>();
                foreach (int refID in tablesIds)
                {
                    outputTableList.Push(new TableEntry
                    {                        
                        RefectoryId = listofTableSeats[refID].IdRef,
                        TableId = listofTableSeats[refID].Id,
                        SeatNbr = listofTableSeats[refID].PlaceNbr
                    });
                }

                return outputTableList;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in generating Tables per category" + ex);
                return null;
            }
        }

        public bool GenerateTablesForMatching(bool mingle = false)
        {
            //if mingle true, no need to break per category            
            Stack<TableEntry> tempStack;
            this.ListAvailableTables = new Dictionary<RegimeEnum, Stack<TableEntry>>();
            if (mingle)
            { 
                tempStack = GetTablesPerCategoryType(this.TablesInRefs);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating tables per category with participants mingle");
                    return false;
                }

                this.ListAvailableTables[RegimeEnum.NONE] = tempStack;
                return true;
            }

            //Break the list per category if mingle false
            Dictionary<RegimeEnum, Dictionary<int, Table>> tempDict = new Dictionary<RegimeEnum, Dictionary<int, Table>>();
            foreach (KeyValuePair<int, Table> table in this.TablesInRefs)
            {
                if (!tempDict.ContainsKey(table.Value.RegimeType))
                {
                    tempDict[table.Value.RegimeType] = new Dictionary<int, Table>
                    {
                        { table.Value.Id, table.Value }
                    };
                }
                else
                {                 
                    tempDict[table.Value.RegimeType][table.Value.RefectoryId] = table.Value;
                }
            }

            // Match per category
            foreach (KeyValuePair<RegimeEnum, Dictionary<int, Table>> cat in tempDict)
            {
                tempStack = GetTablesPerCategoryType(cat.Value);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating tables per category with participants not mingle");
                    return false;
                }
                this.ListAvailableTables[cat.Key] = tempStack;
            }

            return true;
        }

        #endregion
       
        public void GenerateAllBadges(bool mingleAttendees = false)
        {
            if (!this.GenerateSeatsForMatching(mingleAttendees)
                || !this.GenerateBedsForMatching(mingleAttendees)
                || !this.GenerateTablesForMatching(mingleAttendees))
            {
                return;
            }

            // Check that we have enough places (Seats, beds, tables) available for the matching
            int nbrAttendees = this.attendees.Count;
            int nbrSections = ListAvailableSections.Count();
            int nbrDorms = ListAvailableDorms.Count();
            int nbrTables = ListAvailableTables.Count();
            if (nbrSections < nbrAttendees
               || nbrDorms < nbrAttendees
               || nbrTables < nbrAttendees)
            {
                Console.WriteLine(String.Format("Not enought resources available for registered attendees! " +
                    "Sections: {0}, Dorms: {1}, Tables: {2}", nbrSections, nbrDorms, nbrTables));
                return;
            }
            
            //List of participants to an event                        
            List<string> attendeesKeys = new List<string>(this.Attendees.Keys);

            //Shuffle attendees Ids
            attendeesKeys.Shuffle();

            foreach (KeyValuePair<string, EventAttendee> attendee in this.Attendees)
            {                
                HallEntry aSeat = GetHallEntry(mingleAttendees ? HallSectionTypeEnum.NONE : attendee.Value.SectionType);
                if(aSeat == null)
                {
                    return;
                }

                DormEntry aBed = GetDormEntry(mingleAttendees ? DormitoryTypeEnum.NONE : attendee.Value.DormType);
                if (aBed == null)
                {
                    return;
                }

                TableEntry aTable = GetTableEntry(mingleAttendees ? RegimeEnum.NONE : attendee.Value.TableType);
                if (aTable == null)
                {
                    return;
                }

                this.attendees[attendee.Key].HallId = aSeat.HallId;
                this.attendees[attendee.Key].SeatNbr = aSeat.SeatNbr;
                this.attendees[attendee.Key].Id = aBed.IdDormitory;
                this.attendees[attendee.Key].BedNbr = aBed.BedNbr;
                this.attendees[attendee.Key].RefectoryId = aTable.RefectoryId;
                this.attendees[attendee.Key].TableId = aTable.TableId;
                this.attendees[attendee.Key].TableSeatNbr = aTable.SeatNbr;
                this.attendees[attendee.Key].persist();//save data in DB
            }                        
        }
       
        public void PrintAllBadgesToFile(string FilePath, bool forceRecompute)
        {
            //format output
            List<string> temp = new List<string>();
            if (forceRecompute || this.Attendees == null || !this.Attendees.Any())
            {
                GenerateAllBadges();
            }

            //file header
            temp.Add(String.Format("Evenenemnt organise du {1} {2} au {3} {4} dans la ville de {0}"
                     , this.CurrentEvent.Place
                     , this.CurrentEvent.StartDate.DayOfWeek
                     , this.CurrentEvent.StartDate
                     , this.CurrentEvent.EndDate.DayOfWeek
                     , this.CurrentEvent.EndDate));            
            temp.Add(String.Format("Theme: \"{0}\"",this.CurrentEvent.Theme));
            temp.Add(",,,,,,,,,,,,,,,,,,,,,,");
            temp.Add(String.Format(" Prix: {0} Fcfa", this.CurrentEvent.Fee));
            temp.Add(",,,,,,,,,,,,,,,,,,,,,,");
            string header = "Nom,Prenom,Sexe,Ville,Groupe,Responsable Groupe,Niveau,Categorie,Langue,Email,Telehone,"
                            + "Invite Par,Frais Payes,Remarques,Regime,Precision,Section Hall,Siege Hall,Dortoir,"
                            + "Lit,Refectoire,Table, Siege Refectoire";

            temp.Add(header);

            //Rows
            foreach (var entry in this.Attendees)
            {
                string aMatching = String.Format("{0},{1}"
                    , attendeesInfo[entry.Key].ToString()
                    , entry.Value.ToString(this.SeatsInHall, this.BedsInDorms, this.Refectories, this.TablesInRefs)
                   );

                temp.Add(aMatching);
            }

            File.WriteAllLines(FilePath, temp.ToArray());
        }
    }
}
