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
        public int IdSousZone { get; set; }
        public int IdParent { get; set; }
        public String Label { get; set; }
        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            if (IdSousZone != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.SousZones.Add(this);
            }
           
            context.SaveChanges();
            return this.IdSousZone;
        }
        public static int GetIdRefectoryIdByLabel(ApplicationDbContext context, string label)
        {
            var zone = context.SousZones.FirstOrDefault(d => d.Label.Equals(label));
            if (zone != null)
            {
                return zone.IdSousZone;
            }
            else return 0;
        }
    }
}
