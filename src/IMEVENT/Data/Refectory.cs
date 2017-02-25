using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class Refectory:BaseSection,IObjectPersister
    {
        [Key]
        public int IdRefectory { get; set; }
        public int TableCapacity { get; set; }
        public RegimeEnum RegimeType { get; set; }
        public void persist(ApplicationDbContext context)
        {
            context.Refectories.Add(this);
            context.SaveChanges();
        }
    }
}
