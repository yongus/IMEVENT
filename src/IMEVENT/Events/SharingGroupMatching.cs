using IMEVENT.Data;
using IMEVENT.SharedEnums;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Events
{
    public class SharingGroupMatching : BaseEventResource
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public SharingGroupMatching(Event eventI) : base(eventI)
        {
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

        public Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>> ListAvailableSharingGroups;

        public override int CountAvailableResource()
        {
            throw new NotImplementedException();
        }

        //Split input attendee in groups (Round robin Algorithm)
        public List<GroupSharingEntry> ShuffleSharingGroupEntries(List<string> inputList, int nbrGroup)
        {
            inputList.Shuffle();
            List<GroupSharingEntry> ret = new List<GroupSharingEntry>();
            int index = inputList.Count;
            int totalAttendee = inputList.Count;
            foreach (string userId in inputList)
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

        public override bool GenerateItemsForMatching()
        {
            return false;
        }

        public bool GenerateGroupsForMatching(Dictionary<string, EventAttendee> attendees)
        {
            List<GroupSharingEntry> tempStack;
            this.ListAvailableSharingGroups = new Dictionary<SharingGroupCategoryEnum, List<GroupSharingEntry>>();
            if (this.Event.MingleAttendees)
            {
                tempStack = ShuffleSharingGroupEntries(attendees.Keys.ToList(), this.SharingGroups[SharingGroupCategoryEnum.ADULTE]);
                if (tempStack == null)
                {
                    Console.WriteLine("Error in generating sharing groups per category with participants mingle");
                    return false;
                }

                this.ListAvailableSharingGroups[SharingGroupCategoryEnum.ADULTE] = tempStack;
                return true;
            }

            Dictionary<SharingGroupCategoryEnum, List<string>> tempDict = new Dictionary<SharingGroupCategoryEnum, List<string>>();
            foreach (KeyValuePair<string, EventAttendee> attendee in attendees)
            {
                //ADULTE_S | ADULTE_M | JEUNE_MARIE are mapped to ADULTE
                SharingGroupCategoryEnum sGroup = ((attendee.Value.SharingCategory == SharingGroupCategoryEnum.JEUNE_MARIE)
                                                    || (attendee.Value.SharingCategory == SharingGroupCategoryEnum.ADULTE_MARIE)
                                                    || ((attendee.Value.SharingCategory == SharingGroupCategoryEnum.ADULTE_SINGLE)))
                                                    ? SharingGroupCategoryEnum.ADULTE : attendee.Value.SharingCategory;

                if (!tempDict.ContainsKey(sGroup))
                {
                    tempDict[sGroup] = new List<string> { attendee.Value.UserId };
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
                double nbGroup = cat.Value.Count / (double)this.SharingGroups[cat.Key];
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
