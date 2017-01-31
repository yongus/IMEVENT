using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Event:IObjectPersister
    {
        public int IdEvent { get; set; }
        public string Theme { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Place { get; set; }
        public EventType type { get; set; }
        public int Fee { get; set; }

        public void persist(ApplicationDbContext context)
        {
            context.Events.Add(this);
            context.SaveChanges();
        }

    }
}
