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
                       && this.IsBedsDataLoaded && this.IsTablesDataLoaded && this.IsSharingGroupLoaded;
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

        private Dictionary<SharingGroupCategoryEnum, int> sharingGroups;
        public Dictionary<SharingGroupCategoryEnum, int> SharingGroups
        {
            get
            {
                EnsureLoaded();
                return sharingGroups;
            }
        }

        public Dictionary<HallSectionTypeEnum, Stack<HallEntry>> ListAvailableSections;
        public Dictionary<DormitoryTypeEnum, Dictionary<DormitoryCategoryEnum, Stack<DormEntry>>> ListAvailableDorms;
        public Dictionary<RegimeEnum, Stack<TableEntry>> ListAvailableTables;
        public Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>> ListAvailableSharingGroups;

        #endregion

        #region Data load

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

            //sharing groups
            this.sharingGroups = SharingGroup.GetSharingGroups(this.CurrentEvent.Id);
            if (this.sharingGroups == null)
            {
                throw new System.NullReferenceException(string.Format("Sharing groups not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsSharingGroupLoaded = true;
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

        private bool isSharingGroupLoaded;
        public bool IsSharingGroupLoaded
        {
            get
            {
                return isSharingGroupLoaded;
            }

            set
            {
                isSharingGroupLoaded = value;
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

            this.seatsInHall = null;
            this.bedsInDorms = null;
            this.tablesInRefs = null;
        }        
        #endregion        

        #region Assign attendees to seat in halls        

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

        public Stack<HallEntry> ShuffleHallSectionEntries(Dictionary<int, Hall> inputSeatList)
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
                tempStack = ShuffleHallSectionEntries(this.seatsInHall);
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
            foreach(KeyValuePair<int,Hall> section in this.seatsInHall)
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
                tempStack = ShuffleHallSectionEntries(cat.Value);
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

        #region Assgin attendees to beds in Dorms  
              
        protected DormEntry GetDormEntry(DormitoryTypeEnum dormType, DormitoryCategoryEnum dormCat, int? index = null)
        {
            if (this.ListAvailableDorms.IsNullOrEmpty() 
                || !this.ListAvailableDorms.ContainsKey(dormType)
                || this.ListAvailableDorms[dormType].IsNullOrEmpty()
                || !this.ListAvailableDorms[dormType].ContainsKey(dormCat)
                || this.ListAvailableDorms[dormType][dormCat].IsNullOrEmpty()
               )
            {
                Console.WriteLine("GetDormEntry(): No bed available!");
                return null;
            }

            return index == null ? this.ListAvailableDorms[dormType][dormCat].Pop()
                                 : this.ListAvailableDorms[dormType][dormCat].ElementAt((int)index);
        }
                
        public Stack<DormEntry> ShuffleBedEntries(Dictionary<int, Dormitory> inputBedList)
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
            this.ListAvailableDorms = new Dictionary<DormitoryTypeEnum, Dictionary<DormitoryCategoryEnum, Stack<DormEntry>>>();
            if (mingle)
            {
                tempStack = ShuffleBedEntries(this.bedsInDorms);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating beds per category with participants mingle");
                    return false;
                }

                //when we migle, we put all attendees in Beds
                this.ListAvailableDorms[DormitoryTypeEnum.NONE][DormitoryCategoryEnum.BED] = tempStack;
                return true;
            }

            //Break the list per category and type if mingle false
            Dictionary <DormitoryTypeEnum, Dictionary<DormitoryCategoryEnum, Dictionary<int, Dormitory>>> tempDict 
                = new Dictionary<DormitoryTypeEnum, Dictionary<DormitoryCategoryEnum, Dictionary<int, Dormitory>>>();

            foreach (KeyValuePair<int, Dormitory> bed in this.bedsInDorms)
            {
                if (!tempDict.ContainsKey(bed.Value.DormType))
                {
                    tempDict[bed.Value.DormType] = new Dictionary<DormitoryCategoryEnum, Dictionary<int, Dormitory>>();
                }

                if (!tempDict[bed.Value.DormType].ContainsKey(bed.Value.DormCategory))
                {
                    tempDict[bed.Value.DormType][bed.Value.DormCategory] = new Dictionary<int, Dormitory>
                    {
                        { bed.Value.Id, bed.Value }
                    };
                    continue;
                }

                tempDict[bed.Value.DormType][bed.Value.DormCategory][bed.Value.Id] = bed.Value;                
            }

            // Match per category
            foreach (KeyValuePair<DormitoryTypeEnum, Dictionary<DormitoryCategoryEnum, Dictionary<int,Dormitory>>> type in tempDict)
            {
                this.ListAvailableDorms[type.Key] = new Dictionary<DormitoryCategoryEnum, Stack<DormEntry>>();
                foreach (KeyValuePair<DormitoryCategoryEnum, Dictionary<int, Dormitory>> cat in type.Value)
                {
                    tempStack = ShuffleBedEntries(cat.Value);
                    if (tempStack == null)
                    {
                        Console.WriteLine("Error in generating beds per category with participants not mingle");
                        return false;
                    }
                    this.ListAvailableDorms[type.Key][cat.Key] = tempStack;
                }
            }

            return true;
        }

        #endregion

        #region Assign attendees to tables in refectories        

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
        
        public Stack<TableEntry> ShuffleTableEntries(Dictionary<int, Table> inputTableList)
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
                tempStack = ShuffleTableEntries(this.tablesInRefs);
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
            foreach (KeyValuePair<int, Table> table in this.tablesInRefs)
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
                tempStack = ShuffleTableEntries(cat.Value);
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

        #region Divide attendees in sharing Groups

        //Split input attendee in groups (Round robin Algorithm)
        public List<GroupSharingEntry> ShuffleSharingGroupEntries(List<string> inputList, int nbrGroup)
        {            
            inputList.Shuffle();
            List<GroupSharingEntry> ret = new List<GroupSharingEntry>();
            int index = inputList.Count;
            int totalAttendee = inputList.Count;       
            foreach(string userId in inputList)
            {
                ret.Add(new GroupSharingEntry
                    {
                        UserId = userId,
                        Place = (totalAttendee - index) % nbrGroup                   
                    }
                );
                index--;
            }

            return ret;
        }

        public bool GenerateSharingGroupsForMatching(bool mingle = false)
        {
            List<GroupSharingEntry> tempStack;
            this.ListAvailableSharingGroups = new Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>>();
            if (mingle)
            {
                tempStack = ShuffleSharingGroupEntries(this.attendees.Keys.ToList(), this.sharingGroups[SharingGroupCategoryEnum.ADULTE]);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating sharing groups per category with participants mingle");
                    return false;
                }

                this.ListAvailableSharingGroups[SharingGroupCategoryEnum.ADULTE] = tempStack;
                return true;
            }

            Dictionary<SharingGroupCategoryEnum, List<string>> tempDict = new Dictionary<SharingGroupCategoryEnum, List<string>>();
            foreach (KeyValuePair<string, EventAttendee> attendee in this.attendees)
            {
                //ADULTE_S | ADULTE_M | JEUNE_MARIE are mapped to ADULTE
                SharingGroupCategoryEnum sGroup = ((attendee.Value.SharingCategory == SharingGroupCategoryEnum.JEUNE_MARIE)
                                                    || (attendee.Value.SharingCategory == SharingGroupCategoryEnum.ADULTE_M)
                                                    || ((attendee.Value.SharingCategory == SharingGroupCategoryEnum.ADULTE_S)))
                                                    ? SharingGroupCategoryEnum.ADULTE : attendee.Value.SharingCategory;

                if (!tempDict.ContainsKey(sGroup))
                {
                    tempDict[sGroup] = new List<string>{ attendee.Value.UserId };
                }
                else
                {
                    tempDict[sGroup].Add(attendee.Value.UserId);
                }
            }

            // regroup per category
            foreach (KeyValuePair<SharingGroupCategoryEnum, List<string>> cat in tempDict)
            {
                //Number sharing groups is the number of attendee per category divided by the capacity of that category
                double nbGroup = cat.Value.Count / (double)this.sharingGroups[cat.Key];                 
                tempStack = ShuffleSharingGroupEntries(cat.Value, (int)Math.Ceiling(nbGroup));
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating sharing groups per category with participants not mingle");
                    return false;
                }
                this.ListAvailableSharingGroups[cat.Key] = tempStack;
            }

            return true;
        }

        #endregion

        #region Badge Generation
        public void GenerateAllBadges()
        {
            EnsureLoaded();
            if (!this.GenerateSeatsForMatching(this.CurrentEvent.MingleAttendees)
                || !this.GenerateBedsForMatching(this.CurrentEvent.MingleAttendees)
                || !this.GenerateTablesForMatching(this.CurrentEvent.MingleAttendees)
                || !this.GenerateSharingGroupsForMatching(this.CurrentEvent.MingleAttendees))
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

            int nbAssignment = 0;
            foreach (KeyValuePair<SharingGroupCategoryEnum, List<GroupSharingEntry>> cat in this.ListAvailableSharingGroups)
            {
                foreach (GroupSharingEntry gshare in cat.Value)
                {
                    string attendeeKey = gshare.UserId;
                    EventAttendee attendee = this.attendees[attendeeKey];
                    HallEntry aSeat = GetHallEntry(this.CurrentEvent.MingleAttendees ? HallSectionTypeEnum.NONE : attendee.SectionType);
                    if (aSeat == null)
                    {
                        return;
                    }

                    DormitoryTypeEnum dType = this.attendeesInfo[attendee.UserId].Sex.Trim().ToLower() == "f" 
                                              ? DormitoryTypeEnum.WOMEN
                                              : DormitoryTypeEnum.MEN;

                    DormEntry aBed = GetDormEntry(this.CurrentEvent.MingleAttendees ? DormitoryTypeEnum.NONE : dType,
                                                  this.CurrentEvent.MingleAttendees ? DormitoryCategoryEnum.BED : attendee.DormCategory);
                    if (aBed == null)
                    {
                        return;
                    }

                    TableEntry aTable = GetTableEntry(this.CurrentEvent.MingleAttendees ? RegimeEnum.NONE : attendee.TableType);
                    if (aTable == null)
                    {
                        return;
                    }

                    this.attendees[attendeeKey].HallId = aSeat.HallId;
                    this.attendees[attendeeKey].SeatNbr = aSeat.SeatNbr;

                    this.attendees[attendeeKey].DormitoryId = aBed.IdDormitory;
                    this.attendees[attendeeKey].BedNbr = aBed.BedNbr;

                    this.attendees[attendeeKey].RefectoryId = aTable.RefectoryId;
                    this.attendees[attendeeKey].TableId = aTable.TableId;
                    this.attendees[attendeeKey].TableSeatNbr = aTable.SeatNbr;
                    
                    this.attendees[attendeeKey].SharingGroupNbr = gshare.Place + 1; //+1 because groups are numbered from 0on...

                    this.attendees[attendeeKey].persist();//save data in DB
                    nbAssignment++;
                }                                
            }
            
            if(nbAssignment != nbrAttendees)
            {
                throw new System.NullReferenceException(string.Format("Failure to generate badges of registered participants for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
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
            temp.Add(",,,,,,,,,,,,,,,,,,,,,,,");
            temp.Add(String.Format(" Prix: {0} Fcfa", this.CurrentEvent.Fee));
            temp.Add(",,,,,,,,,,,,,,,,,,,,,,,");
            string header = "Nom,Prenom,Sexe,Ville,Groupe,Responsable Groupe,Niveau,Langue,Email,Telehone,"
                            + "Invite Par,Frais Payes,Remarques,Precision,Section Hall,Nr Siege,Dortoir,"
                            + "Nr Lit,Refectoire,Nr Table, Nr Siege, Groupe Partage";

            temp.Add(header);

            //Rows
            foreach (var entry in this.Attendees)
            {
                string aMatching = String.Format("{0},{1}"
                    , attendeesInfo[entry.Key].ToString()
                    , entry.Value.ToString(this.seatsInHall, this.bedsInDorms, this.refectories, this.tablesInRefs)
                   );

                temp.Add(aMatching);
            }

            File.WriteAllLines(FilePath, temp.ToArray());
        }

        #endregion
    }
}
