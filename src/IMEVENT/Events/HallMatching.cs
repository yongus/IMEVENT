using IMEVENT.Data;
using IMEVENT.SharedEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Events
{
    public class HallMatching : EventResource
    {        
        private Dictionary<int, Hall> seatsInHall;
        public Dictionary<int, Hall> SeatsInHall
        {
            get
            {
                EnsureLoaded();            
                return seatsInHall;
            }
        }
                        
        public HallMatching(int eventId)
        {
            this.EventId = eventId;
            this.IsDataLoaded = false;
        }

        Dictionary<HallSectionTypeEnum, Stack<HallEntry>> ListAvailableSections = null;

        protected HallEntry GetHallEntry(HallSectionTypeEnum sectionType)
        {
            if (this.ListAvailableSections.IsNullOrEmpty()
               || !this.ListAvailableSections.ContainsKey(sectionType)
               || this.ListAvailableSections[sectionType].IsNullOrEmpty())
            {
                Console.WriteLine("GetHallEntry(): No seat available!");
                return null;
            }

            return this.ListAvailableSections[sectionType].Pop();
        }

        public List<Section> BalanceShuffle(Dictionary<int, Stack<Section>> inputList)
        {
            if (inputList == null || !inputList.Any())
            {
                return null;
            }

            List<Section> ret = new List<Section>();
            int index = 0;
            int nbrElem = inputList.Count();//total seats in the list
            int count = 0;
            while (count < nbrElem)
            {
                index = index % inputList.Count;
                if (inputList[index].IsNullOrEmpty())
                {
                    index++;
                    continue;
                }

                Section tmp = inputList[index++].Pop();
                ret.Add(tmp);
                count++;
            }
            return null;
        }

        public Stack<HallEntry> ShuffleSectionEntries(Dictionary<int, Hall> inputSeatList)
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception in generating Seats per category" + ex);
                return null;
            }
        }

        protected override void EnsureLoaded()
        {
            //Get list of seats
            /*this.seatsInHall = Hall.GetHallSections(this.EventId);
            if (this.seatsInHall == null)
            {
                throw new System.NullReferenceException(string.Format("Seats not availaible for the event at {0}, starting on {1}"
                    , this.CurrentEvent.Place
                    , this.CurrentEvent.StartDate.ToString()));
            }
            this.IsDataLoaded = true;*/
        }

        public override bool GenerateItemsForMatching(bool mingle)
        {
            //if mingle true, no need to break per category
            Stack<HallEntry> tempStack;
            this.ListAvailableSections = new Dictionary<HallSectionTypeEnum, Stack<HallEntry>>();
            if (mingle)
            {
                tempStack = ShuffleSectionEntries(this.seatsInHall);
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
            foreach (KeyValuePair<int, Hall> section in this.seatsInHall)
            {
                if (!tempDict.ContainsKey(section.Value.HallType))
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
            foreach (KeyValuePair<HallSectionTypeEnum, Dictionary<int, Hall>> cat in tempDict)
            {
                tempStack = ShuffleSectionEntries(cat.Value);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating seats per category with participants not mingle");
                    return false;
                }
                this.ListAvailableSections[cat.Key] = tempStack;
            }

            return true;
        }        
    }
}
