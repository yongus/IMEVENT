using IMEVENT.Data;
using IMEVENT.SharedEnums;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Events
{
    public class SharingGroupMatching : BaseEventResourceManager
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public SharingGroupMatching(Event eventI) : base(eventI)
        {
        }

        int tableIndex;

        private Dictionary<SharingGroupCategoryEnum, int> sharingGroups;
        public Dictionary<SharingGroupCategoryEnum, int> SharingGroups
        {
            get
            {
                EnsureLoaded();
                return sharingGroups;
            }
        }

        public Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>> ListAvailableSharingGroups;

        public override int CountAvailableResource()
        {
            throw new NotImplementedException();
        }

        //Split input attendee in groups (Round robin Algorithm)
        public List<GroupSharingEntry> ShuffleSharingGroupEntries(List<string> inputList, int nbrGroup, int capacity)
        {
            inputList.Shuffle();
            List<GroupSharingEntry> ret = new List<GroupSharingEntry>();
            int index = 0;
            int groupeIndex = 0;

            foreach (string userId in inputList)
            {
                /*ret.Add(new GroupSharingEntry
                    {
                        UserId = userId,
                        Place = (totalAttendee - index) % nbrGroup
                    }
                );
                index--;
                */
                if (index == capacity)
                {
                    groupeIndex++;
                    this.tableIndex++;
                    index = 0;
                }

                index++;
                ret.Add(new GroupSharingEntry
                    {
                        UserId = userId,
                        Place = groupeIndex,
                        Table = this.tableIndex                        
                    }
                );                
            }

            this.tableIndex++;//move next group to the next table
            return ret;
        }

        public override bool GenerateItemsForMatching()
        {
            return false;
        }

        public bool GenerateGroupsForMatching(Dictionary<string, EventAttendee> attendeesList)
        {
            List<GroupSharingEntry> tempStack;
            this.ListAvailableSharingGroups = new Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>>();
            if (this.Event.MingleAttendees)
            {
                int capacity = this.SharingGroups[SharingGroupCategoryEnum.ADULTE];
                tempStack = ShuffleSharingGroupEntries(attendeesList.Keys.ToList(), capacity, capacity);
                if (tempStack == null)
                {
                    log.Error("GenerateGroupsForMatching: Error in generating sharing groups per category with participants mingle");
                    return false;
                }

                this.ListAvailableSharingGroups[SharingGroupCategoryEnum.ADULTE] = tempStack;
                return true;
            }

            //Get attendees keys and shuffle
            List<string> attendeeKeys = new List<string>(attendeesList.Keys);
            attendeeKeys.Shuffle();

            Dictionary<SharingGroupCategoryEnum, List<string>> tempDict = new Dictionary<SharingGroupCategoryEnum, List<string>>();
            
            foreach (string attendee in attendeeKeys)
            {
                //ADULTE_S | ADULTE_M | JEUNE_MARIE are mapped to ADULTE
                SharingGroupCategoryEnum sGroup = ((attendeesList[attendee].SharingCategory == SharingGroupCategoryEnum.JEUNE_MARIE)
                                                    || (attendeesList[attendee].SharingCategory == SharingGroupCategoryEnum.ADULTE_MARIE)
                                                    || ((attendeesList[attendee].SharingCategory == SharingGroupCategoryEnum.ADULTE_SINGLE)))
                                                    ? SharingGroupCategoryEnum.ADULTE : attendeesList[attendee].SharingCategory;

                if (!tempDict.ContainsKey(sGroup))
                {
                    tempDict[sGroup] = new List<string> { attendeesList[attendee].UserId };
                }
                else
                {
                    tempDict[sGroup].Add(attendeesList[attendee].UserId);
                }
            }

            // regroup per category
            this.tableIndex = 1;
            foreach (KeyValuePair<SharingGroupCategoryEnum, List<string>> cat in tempDict)
            {
                //Number sharing groups is the number of attendee per category divided by the capacity of that category
                double nbGroupPerType = cat.Value.Count / (double)this.SharingGroups[cat.Key];
                tempStack = ShuffleSharingGroupEntries(cat.Value, (int)Math.Ceiling(nbGroupPerType), this.SharingGroups[cat.Key]);
                if (tempStack == null)
                {
                    log.Error("GenerateGroupsForMatching: Error in generating sharing groups per category with participants not mingle");
                    return false;
                }
                this.ListAvailableSharingGroups[cat.Key] = tempStack;
            }

            return true;
        }

        public override List<string> GetListOfRemainingItems()
        {
            throw new NotImplementedException();
        }

        public override void SaveRemainingItemsInDB()
        {
            throw new NotImplementedException();
        }

        protected override void EnsureLoaded()
        {
            if (this.IsDataLoaded)
            {
                //Data already loaded
                return;
            }

            this.sharingGroups = SharingGroup.GetSharingGroups(this.Event.Id);
            if (this.sharingGroups == null)
            {
                log.Error(string.Format("Sharing groups not configured for event {0}"
                    , this.Event.Id));
                throw new System.NullReferenceException(string.Format("Sharing groups not configured for event {0}"
                    , this.Event.Id));
            }
            this.IsDataLoaded = true;
        }
    }
}
