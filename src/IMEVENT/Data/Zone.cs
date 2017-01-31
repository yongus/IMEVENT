using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Zone:IObjectPersister
    {
        public int Id { get; set; }
        public String Label { get; set; }
        public void persist(ApplicationDbContext context)
        {
            context.Zones.Add(this);
            context.SaveChanges();
        }

    }
}
