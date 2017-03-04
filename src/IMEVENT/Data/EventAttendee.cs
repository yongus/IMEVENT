﻿using System;
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
        public int IdHall { get; set; }
       
        public int SeatNbr { get; set; }
        public int IdDormitory { get; set; }
  
        public int BedNbr { get; set; }
        public int IdRefectory { get; set; }
        public int TableNbr { get; set; }
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
                , IdRefectory
                , TableNbr
                , TableSeatNbr
                );

            return ret;
        }

        public string ToString(Dictionary<int, Hall> Halls, Dictionary<int, Dormitory> Dorms, Dictionary<int, Refectory> Refectories)
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
                , Refectories[IdRefectory].Name
                , TableNbr
                , TableSeatNbr
                );

            return ret;
        }
        
        public static Dictionary<string, EventAttendee> GetAllAttendee(int eventID)
        {            
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.EventAttendees.Where(x => x.Id == eventID).ToDictionary(x => x.UserId, x => x); 
        }

        public EventAttendee GetEventAttendeeById(int eventId, string userId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            EventAttendee attendee = context.EventAttendees.Where(x => x.Id == eventId).FirstOrDefault(e => e.UserId == userId);
            if (attendee != null)
            {
                return attendee;
            }

            return null;
        }

        public static List<EventAttendee>  GetAllByEvent(int eventId)
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
