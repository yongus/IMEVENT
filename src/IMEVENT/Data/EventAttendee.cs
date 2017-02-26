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
        public Int32 AmountPaid { get; set; }
        public string Remarks { get; set; }
        public bool OnDiet { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            context.EventAttendees.Add(this);
            if (IdEventAttendee != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.EventAttendees.Add(this);
            }
            context.SaveChanges();

            return this.IdEventAttendee;

        }
    }
}
