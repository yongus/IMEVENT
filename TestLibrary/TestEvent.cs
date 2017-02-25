using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT;
using IMEVENT.Data;
using IMEVENT.Events;
using IMEVENT.SharedEnums;

namespace TestLibrary
{
    [TestClass]
    public class TestEvent
    {
        const int EVENTID = 1;
        protected Dictionary<int, Hall> GetHalls(string[] hallLines)
        {            
            try
            {
                Dictionary<int, Hall> ret = new Dictionary<int, Hall>();
                foreach (string line in hallLines)
                {
                    string[] aHall = line.Split(',');
                    int id = int.Parse(aHall[0]);
                    ret[id] = new Hall
                    {
                        IdEvent = EVENTID,
                        IdHall = id,
                        Name = aHall[1],
                        Capacity = int.Parse(aHall[2]),
                        HallType = (HallSectionTypeEnum)int.Parse(aHall[3])                        
                    };
                }

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected Dictionary<int, Dormitory> GetDorms(string[] dormLines)
        {            
            try
            {
                Dictionary<int, Dormitory> ret = new Dictionary<int, Dormitory>();
                foreach (string line in dormLines)
                {
                    string[] aDorm = line.Split(',');
                    int id = int.Parse(aDorm[0]);
                    ret[id] = new Dormitory
                    {
                        IdEvent = EVENTID,
                        IdDormitory = id,
                        Name = aDorm[1],
                        Capacity = int.Parse(aDorm[2]),
                        DormType = (DormitoryTypeEnum)int.Parse(aDorm[3])
                    };
                }

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected Dictionary<int, Refectory> GetRefs(string[] refLines)
        {            
            try
            {
                Dictionary<int, Refectory> ret = new Dictionary<int, Refectory>();
                foreach (string line in refLines)
                {
                    string[] aRef = line.Split(',');
                    int id = int.Parse(aRef[0]);
                    ret[id] = new Refectory
                    {
                        IdEvent = EVENTID,
                        IdRefectory = id,
                        Name = aRef[1],
                        Capacity = int.Parse(aRef[2]),
                        TableCapacity = int.Parse(aRef[3]),
                        RegimeType = RegimeEnum.NONE
                    };
                }

                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DormitoryTypeEnum GetDormType(string sex, string cat)
        {
            if(sex == "M")
            {
                if (cat.ToLower().Contains("adulte"))
                {
                    return DormitoryTypeEnum.MALE;
                }

                return DormitoryTypeEnum.YOUNGBOYS;
            }
            //Ladies
            if (cat.ToLower().Contains("adulte"))
            {
                return DormitoryTypeEnum.FEMALE;
            }

            return DormitoryTypeEnum.YOUNGGIRLS;
        }

        protected bool GetParticipants(string[] partLines, out Dictionary<string, EventAttendee> attendee
            , out Dictionary<string, User> attendeeInfo)
        {            
            attendee = null;
            attendeeInfo = null;            

            if (partLines == null || partLines.Length == 0)
            {
                return false;
            }

            Dictionary<string, MembershipLevelEnum> level = new Dictionary<string, MembershipLevelEnum>
            {
                {"", MembershipLevelEnum.INVITE},
                {"invite", MembershipLevelEnum.INVITE},
                {"simple", MembershipLevelEnum.SIMPLE},
                {"regulier", MembershipLevelEnum.REGULIER},
                {"actif 1", MembershipLevelEnum.ACTIF_1},
                {"actif 2", MembershipLevelEnum.ACTIF_2},
                {"actif 3", MembershipLevelEnum.ACTIF_3},
                {"accompagnateur", MembershipLevelEnum.ACCOMPAGNATEUR},
                {"aef", MembershipLevelEnum.AEF},
                {"jeune phare", MembershipLevelEnum.JEUNE_PHARE},
                {"cmp", MembershipLevelEnum.CMP},
                {"mp", MembershipLevelEnum.MP},
                {"incarnateur", MembershipLevelEnum.INCARNATEUR},
                {"rg", MembershipLevelEnum.RG}
            };

            Dictionary<string, SharingGroupCategoryEnum> category = new Dictionary<string, SharingGroupCategoryEnum>
            {
                {"", SharingGroupCategoryEnum.SECOND_JUNIOR},
                {"second junior", SharingGroupCategoryEnum.SECOND_JUNIOR},
                {"second interm", SharingGroupCategoryEnum.SECOND_INTERMEDIARE},
                {"univers debt", SharingGroupCategoryEnum.UNIVERSITAIRE_DEBUTANT},
                {"univers maj", SharingGroupCategoryEnum.UNIVERSITAIRE_MAJEUR},
                {"jeune marie", SharingGroupCategoryEnum.JEUNE_MARIE},
                {"jeune trav", SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR},
                {"jeune trav maj", SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_MAJEUR},
                {"jeune trav sen", SharingGroupCategoryEnum.JEUNE_TRAVAILLEUR_SENIOR},
                {"adulte m", SharingGroupCategoryEnum.ADULTE_M},
                {"adulte s", SharingGroupCategoryEnum.ADULTE_S}                                
            };

            try
            {
                attendee = new Dictionary<string, EventAttendee>();
                attendeeInfo = new Dictionary<string, User>();

                foreach (string line in partLines)
                {
                    string[] aPart = line.Split(',');
                    string id = Guid.NewGuid().ToString();
                    attendee[id] = new EventAttendee
                    {
                        IdEvent = EVENTID,
                        UserId = id,
                        InvitedBy = aPart[7],
                        AmountPaid = int.Parse(aPart[10]),
                        Remarks = aPart[14],
                        Regime = aPart[15],
                        Precision = aPart[16], 
                        sectionType = aPart[0].ToLower().StartsWith("abbe")? HallSectionTypeEnum.SPECIAL_GUEST : HallSectionTypeEnum.NONE,
                        DormType = GetDormType(aPart[2], aPart[8]),                                               
                        RefectoryType = RegimeEnum.NONE
                    };

                    attendeeInfo[id] = new User
                    {
                        UserId = id,
                        LastName = aPart[0],
                        FirstName = aPart[1],
                        Sex = aPart[2],
                        Town = aPart[4],
                        //Group = aPart[5],
                        MembershipLevel = level[aPart[6].ToLower()],
                        Category = category[aPart[8].ToLower()],
                        Language = aPart[9],
                        Email = aPart[11],
                        PhoneNumber = aPart[12],
                        IsGroupResponsible = (aPart[13].ToLower() == "oui") ? true : false                        
                    };
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            string[] hallsLines = File.ReadAllLines("C:\\Users\\fyonga\\Source\\Repos\\IMEVENT2\\InputData\\Reduced\\Halls.txt");
            string[] dormsLines = File.ReadAllLines("C:\\Users\\fyonga\\Source\\Repos\\IMEVENT2\\InputData\\Reduced\\Dormitories.txt");
            string[] refsLines = File.ReadAllLines("C:\\Users\\fyonga\\Source\\Repos\\IMEVENT2\\InputData\\Reduced\\Refectories.txt");
            string[] attendeeList = File.ReadAllLines("C:\\Users\\fyonga\\Source\\Repos\\IMEVENT2\\InputData\\Reduced\\Participants.txt");

            Dictionary<int, Hall> halls = GetHalls(hallsLines);
            Dictionary<int, Dormitory> dorms = GetDorms(dormsLines);
            Dictionary<int, Refectory> refs = GetRefs(refsLines);
            Dictionary<string, EventAttendee> attendees;
            Dictionary<string, User> attendeesInfo;
            if (!GetParticipants(attendeeList, out attendees, out attendeesInfo))
            {
                return;
            };

            DataMatchingGenerator badge = new DataMatchingGenerator(EVENTID);
            badge.LoadDataInMatchingGenerator(attendees, attendeesInfo, halls, dorms, refs);
            if (!badge.GenerateAllBadges(false))
            {
                return;
            };
            badge.PrintAllBadgesToFile("C:\\Users\\fyonga\\Source\\Repos\\IMEVENT2\\InputData\\Reduced\\Results.csv", false);
            return;
            //<ProjectGuid>2c4f4925-8651-4533-96ff-6d53dae66163</ProjectGuid>
        }
    }
}
