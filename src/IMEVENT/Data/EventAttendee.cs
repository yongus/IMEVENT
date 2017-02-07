using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class EventAttendee:IObjectPersister
    {
        [Key]
        public int IdEventAttendee { get; set; }
        public string UserId { get; set; }
        public string InvitedBy { get; set; }
        public int IdEvent { get; set; }
        public int AmountPaid { get; set; }
        public string Remarks { get; set; }

        public int persist(ApplicationDbContext context)
        {
      
            context.EventAttendees.Add(this);
            context.SaveChanges();

            return this.IdEventAttendee;

        }
    }
}
