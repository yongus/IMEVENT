using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;
using IMEVENT.Services;

namespace IMEVENT.Data
{
    public class EventAttendee:IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
       
        public Int32 AmountPaid { get; set; }
        public string Remarks { get; set; }
        public bool OnDiet { get; set; }
        public string UserId { get; set; }
     
        public string InvitedBy { get; set; }     
       
        public string Regime { get; set; }
        public string Precision { get; set; }
        public HallSectionTypeEnum SectionType { get; set; }
        public DormitoryTypeEnum DormType { get; set; }
        public RegimeEnum TableType { get; set; }
        public SharingGroupCategoryEnum SharingCategory { get; set; }
        public int HallId { get; set; }       
        public int SeatNbr { get; set; }
        public int DormitoryId { get; set; }  
        public int BedNbr { get; set; }
        public int RefectoryId { get; set; }
        public int TableId { get; set; }
        public int TableSeatNbr { get; set; }

        public override string ToString()
        {
            string ret = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}"
                , InvitedBy
                , AmountPaid
                , Remarks
                , Regime
                , Precision
                , Id
                , SeatNbr
                , Id
                , BedNbr
                , RefectoryId
                , TableId
                , TableSeatNbr
                );

            return ret;
        }

        public string ToString(Dictionary<int, Hall> Halls, Dictionary<int, Dormitory> Dorms
            , Dictionary<int, Refectory> Refectories, Dictionary<int, Table> Tables)
        {
            string ret = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}"
                , InvitedBy
                , AmountPaid
                , Remarks
                , Regime
                , Precision
                , Halls[Id].Name
                , SeatNbr
                , Dorms[Id].Name
                , BedNbr
                , Refectories[RefectoryId].Name
                , Tables[TableId].Name
                , TableSeatNbr
                );

            return ret;
        }
        
        public static Dictionary<string, EventAttendee> GetAttendeeList(int eventID)
        {            
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.EventAttendees.Where(x => x.Id == eventID).ToDictionary(x => x.UserId, x => x); 
        }

        public EventAttendee GetEventAttendeeById(int eventId, string userId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.EventAttendees.Where(x => x.EventId == eventId).FirstOrDefault(e => e.UserId == userId);                        
        }

        public static List<EventAttendee> GetAttendeeListByEvent(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.EventAttendees.Where(x => x.EventId == eventId).ToList();
        }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
           
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.EventAttendees.Add(this);
            }

            context.SaveChanges();

            return this.Id;
        }
    }
}
