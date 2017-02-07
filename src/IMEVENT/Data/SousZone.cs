using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Data
{
    public class SousZone:IObjectPersister
    {
        public int IdZone { get; set; }
        [Key]
        public int Id { get; set; }
        public int IdParent { get; set; }
        public String Label { get; set; }
        public int persist(ApplicationDbContext context)
        {
            context.SousZones.Add(this);
            context.SaveChanges();
            return this.Id;
        }
        public static int GetIdRefectoryIdByLabel(ApplicationDbContext context, string label)
        {
            var zone = context.SousZones.FirstOrDefault(d => d.Label.Equals(label));
            if (zone != null)
            {
                return zone.Id;
            }
            else return 0;
        }
    }
}
