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
        private bool isLoaded;         
        public int EventID { get; set; }
        //Constructor
        public DataMatchingGenerator(int eventID)
        {
            this.EventID = eventID;
            InvalidateAllData();
        }

        //Test Constructor
        public void LoadDataInMatchingGenerator(Dictionary<string, EventAttendee> participants, Dictionary<string, User> participantsInfo, 
            Dictionary<int, Hall> seats, Dictionary<int, Dormitory> beds, Dictionary<int, Refectory> tables)
        {
            attendees = participants;
            attendeesInfo = participantsInfo;
            seatsInHall = seats;
            bedsInDorms = beds;
            tablesInRefs = tables;
            isLoaded = true;
        }

        //Total number of attendees at an event
        private int totalAttendees;
        public int TotalAttendees
        {
            get
            {
                EnsureLoaded();
                return totalAttendees;
            }            
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

        //attendees Info
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

        private Dictionary<int, Refectory> tablesInRefs;
        public Dictionary<int, Refectory> TablesInRefs
        {
            get
            {
                EnsureLoaded();
                return tablesInRefs;
            }
        }

        public Dictionary<HallSectionTypeEnum, Stack<HallEntry>> ListAvailableSections;
        public Dictionary<DormitoryTypeEnum, Stack<DormEntry>> ListAvailableDorms;
        public Dictionary<RegimeEnum, Stack<RefectoryEntry>> ListAvailableRefectories;

        private void EnsureLoaded()
        {
            if (isLoaded)
            {
                totalAttendees = attendees.Count;
                return; //data already loaded
            }

            //Get list of attendees
            this.attendees = EventAttendee.GetAllAttendee(this.EventID);

            this.seatsInHall = Hall.GetAllHalls(this.EventID);

            this.bedsInDorms = Dormitory.GetAllDorms(this.EventID);
            //set total attendees

            isLoaded = true;
        }

        public void LoadDataFromDB()
        {

        }

        private void InvalidateAllData()
        {
            isLoaded = false;
        }

        public void InvalidateAllButSeats()
        {

        }

        #endregion

        #region Map Attendees to Seat in Halls        

        protected HallEntry GetHallEntry(HallSectionTypeEnum sectionType, int? index = null)
        {
            if(this.ListAvailableSections.IsNullOrEmpty() 
               || !this.ListAvailableSections.ContainsKey(sectionType)
               || this.ListAvailableSections[sectionType].IsNullOrEmpty())
            {
                return null;
            }

            return index == null ? this.ListAvailableSections[sectionType].Pop() 
                                 : this.ListAvailableSections[sectionType].ElementAt((int)index);
        }        

        public bool MatchAttendeeToSeatPerCategory(Dictionary<int, Hall> inputSeatList, out Stack<HallEntry> outputSeatList)
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
                            Id = seat.Value.IdHall,
                            PlaceNbr = j
                        };
                    }
                }

                //Shuffle Ids
                List<int> seatsIds = new List<int>(listofSeats.Keys);
                seatsIds.Shuffle();

                //do the assignement
                outputSeatList = new Stack<HallEntry>();
                foreach (int seatID in seatsIds)
                {
                    outputSeatList.Push(new HallEntry
                    {                        
                        IdHall = listofSeats[seatID].Id,
                        SeatNbr = listofSeats[seatID].PlaceNbr
                    });
                }

                return true;
            }
            catch
            {
                outputSeatList = null;
                return false;
            }           
        }

        public bool MatchAttendeesToSeats(bool mingle = false)
        {
            //if mingle true, no need to break per categorie
            this.ListAvailableSections = new Dictionary<HallSectionTypeEnum, Stack<HallEntry>>();
            if (mingle)
            {
                Stack<HallEntry> tempStack;
                if (!MatchAttendeeToSeatPerCategory(this.SeatsInHall, out tempStack))
                {
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
                        { section.Value.IdHall, section.Value }
                    };
                }
                else
                {
                    tempDict[section.Value.HallType][section.Value.IdHall] = section.Value;
                }                
            }

            // Match per category
            foreach(KeyValuePair<HallSectionTypeEnum, Dictionary<int, Hall>> cat in tempDict)
            {
                Stack<HallEntry> tempStack;
                if (!MatchAttendeeToSeatPerCategory(cat.Value, out tempStack))
                {
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
                return null;
            }

            return index == null ? this.ListAvailableDorms[dormType].Pop()
                                 : this.ListAvailableDorms[dormType].ElementAt((int)index);
        }
                
        public bool MatchAttendeeToBedPerCategory(Dictionary<int, Dormitory> inputBedList, out Stack<DormEntry> outputBedList)
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
                            Id = dorm.Value.IdDormitory,
                            PlaceNbr = j
                        };
                    }
                }

                //Shuffle Indexes
                List<int> bedsIds = new List<int>(listofBeds.Keys);
                bedsIds.Shuffle();

                //Perform the assignement
                outputBedList = new Stack<DormEntry>();
                foreach (int bedId in bedsIds)
                {
                    outputBedList.Push(new DormEntry
                    {                        
                        IdDormitory = listofBeds[bedId].Id,
                        BedNbr = listofBeds[bedId].PlaceNbr
                    });
                }

                return true;
            }
            catch
            {
                outputBedList = null;
                return false;
            }
        }

        public bool MatchAttendeesToBeds(bool mingle = false)
        {
            //if mingle true, no need to break per categorie
            this.ListAvailableDorms = new Dictionary<DormitoryTypeEnum, Stack<DormEntry>>();
            if (mingle)
            {
                Stack<DormEntry> tempStack;
                if (!MatchAttendeeToBedPerCategory(this.BedsInDorms, out tempStack))
                {
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
                        { bed.Value.IdDormitory, bed.Value }
                    };
                }
                else
                {
                    tempDict[bed.Value.DormType][bed.Value.IdDormitory] = bed.Value;
                }
            }

            // Match per category
            foreach (KeyValuePair<DormitoryTypeEnum, Dictionary<int,Dormitory>> cat in tempDict)
            {
                Stack<DormEntry> tempStack;
                if (!MatchAttendeeToBedPerCategory(cat.Value, out tempStack))
                {
                    return false;
                }
                this.ListAvailableDorms[cat.Key] = tempStack;
            }

            return true;
        }

        #endregion

        #region Map Attendees to Tables in Refectories        

        protected RefectoryEntry GetRefectoryEntry(RegimeEnum refType, int? index = null)
        {
            if (this.ListAvailableDorms.IsNullOrEmpty()
                || !this.ListAvailableRefectories.ContainsKey(refType)
                || this.ListAvailableRefectories[refType].IsNullOrEmpty())
            {
                return null;
            }

            return index == null ? this.ListAvailableRefectories[refType].Pop()
                                 : this.ListAvailableRefectories[refType].ElementAt((int)index);
        }
        
        public bool MatchAttendeeToTablePerCategory(Dictionary<int, Refectory> inputTableList, out Stack<RefectoryEntry> outputTableList)
        {
            Dictionary<int, Section> listofTableSeats = new Dictionary<int, Section>();
            try
            {
                int index = 0;
                foreach (KeyValuePair<int, Refectory> refect in inputTableList)
                {
                    for (int j = 1; j <= refect.Value.Capacity; j++)
                    {
                        for(int k = 1; k <= refect.Value.TableCapacity; k++)
                        {                            
                            listofTableSeats[++index] = new Section
                            {
                                Id = refect.Value.IdRefectory,
                                TableNbr = j,
                                PlaceNbr = k
                            };
                        }                        
                    }
                }

                //Shuffle Ids
                List<int> tablesIds = new List<int>(listofTableSeats.Keys);
                tablesIds.Shuffle();

                //do the assignement
                outputTableList = new Stack<RefectoryEntry>();
                foreach (int refID in tablesIds)
                {
                    outputTableList.Push(new RefectoryEntry
                    {                        
                        IdRefectory = listofTableSeats[refID].Id,
                        TableNbr = listofTableSeats[refID].TableNbr,
                        SeatNbr = listofTableSeats[refID].PlaceNbr
                    });
                }

                return true;
            }
            catch
            {
                outputTableList = null;
                return false;
            }
        }

        public bool MatchAttendeesToTables(bool mingle = false)
        {
            //if mingle true, no need to break per categorie
            this.ListAvailableRefectories = new Dictionary<RegimeEnum, Stack<RefectoryEntry>>();
            if (mingle)
            { 
                Stack<RefectoryEntry> tempStack;
                if (!MatchAttendeeToTablePerCategory(this.TablesInRefs, out tempStack))
                {
                    return false;
                }

                this.ListAvailableRefectories[RegimeEnum.NONE] = tempStack;
                return true;
            }

            //Break the list per category if mingle false
            Dictionary<RegimeEnum, Dictionary<int, Refectory>> tempDict = new Dictionary<RegimeEnum, Dictionary<int, Refectory>>();
            foreach (KeyValuePair<int, Refectory> table in this.TablesInRefs)
            {
                /*if (!tempDict.ContainsKey(table.Value.RegimeType))
                {
                    tempDict[table.Value.RegimeType] = new Dictionary<int, Refectory>
                    {
                        { table.Value.IdRefectory, table.Value }
                    };
                }
                else
                {
                    tempDict[table.Value.RegimeType][table.Value.IdRefectory] = table.Value;
                }*/
            }

            // Match per category
            foreach (KeyValuePair<RegimeEnum, Dictionary<int, Refectory>> cat in tempDict)
            {
                Stack<RefectoryEntry> tempStack;
                if (!MatchAttendeeToTablePerCategory(cat.Value, out tempStack))
                {
                    return false;
                }
                this.ListAvailableRefectories[cat.Key] = tempStack;
            }

            return true;
        }

        #endregion

       
        public bool GenerateAllBadges(bool mingleAttendees = false)
        {
            if (!this.MatchAttendeesToSeats(mingleAttendees))
            {
                return false;
            }
            
            if (!this.MatchAttendeesToBeds(mingleAttendees))
            {
                return false;
            }
            
            if (!this.MatchAttendeesToTables(mingleAttendees))
            {
                return false;
            }

            /* Check that we have enough available places (Seats, beds, tables) for the matching
            if (ListAvailableSections.Count() < this.TotalAttendees 
               || AttendeeToBeds.Count() < this.TotalAttendees
               || AttendeeToTables.Count() < this.TotalAttendees)
            {
                return false;
            }
            */

            //List of participants to an event                        
            List<string> attendeesKeys = new List<string>(this.Attendees.Keys);

            //Shuffle attendees Ids
            attendeesKeys.Shuffle();

            foreach (KeyValuePair<string, EventAttendee> attendee in this.Attendees)
            {                
                HallEntry aSeat = GetHallEntry(HallSectionTypeEnum.NONE );
                if(aSeat == null)
                {
                    return false;
                }

                DormEntry aBed = GetDormEntry(mingleAttendees ? DormitoryTypeEnum.NONE : attendee.Value.DormType);
                if (aBed == null)
                {
                    return false;
                }

                RefectoryEntry aTable = GetRefectoryEntry( RegimeEnum.NONE );
                if (aTable == null)
                {
                    return false;
                }
                this.Attendees[attendee.Key].IdHall = aSeat.IdHall;
                this.Attendees[attendee.Key].SeatNbr = aSeat.SeatNbr;
                this.Attendees[attendee.Key].IdDormitory = aBed.IdDormitory;
                this.Attendees[attendee.Key].BedNbr = aBed.BedNbr;
                this.Attendees[attendee.Key].IdRefectory = aTable.IdRefectory;
                this.Attendees[attendee.Key].TableNbr = aTable.TableNbr;
                this.Attendees[attendee.Key].TableSeatNbr = aTable.SeatNbr;
            }
            
            return true;
        }
       
        public void PrintAllBadgesToFile(string FilePath, bool forceRecompute)
        {
            //format output
            List<string> temp = new List<string>();
            if (forceRecompute || this.Attendees == null || !this.Attendees.Any())
            {
                if (!GenerateAllBadges())
                {
                    return;
                }
            }

            //file header
            string header = "Nom,Prenom,Sexe,Ville,Groupe,Responsable Groupe,Niveau,Categorie,Langue,Email,Telehone,"
                            + "Invite Par,Frais Payes,Remarques,Regime,Precision,Section Hall,Siege Hall,Dortoir,"
                            + "Lit,Refectoire,Table, Siege Refectoire";

            temp.Add(header);

            //Rows
            foreach (var entry in this.Attendees)
            {
                string aMatching = String.Format("{0},{1}"
                    , attendeesInfo[entry.Key].ToString()
                    , entry.Value.ToString(this.SeatsInHall, this.BedsInDorms, this.TablesInRefs)
                   );

                temp.Add(aMatching);
            }

            File.WriteAllLines(FilePath, temp.ToArray());
        }
    }
}
