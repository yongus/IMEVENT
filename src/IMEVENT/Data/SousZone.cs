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
        public void persist(ApplicationDbContext context)
        {
            context.SousZones.Add(this);
            context.SaveChanges();
        }
    }
}
