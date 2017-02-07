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
        public int persist(ApplicationDbContext context)
        {
            context.Zones.Add(this);
            context.SaveChanges();
            return this.Id;
        }

        public static int GetIdRefectoryIdByName(ApplicationDbContext context, string label)
        {
            var zone = context.Zones.FirstOrDefault(d => d.Label.Equals(label));
            if (zone != null)
            {
                return zone.Id;
            }
            else return 0;
        }

    }
}
