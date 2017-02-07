using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Refectory:BaseSection,IObjectPersister
    {
        [Key]
        public int IdRefectory { get; set; }
        public int persist(ApplicationDbContext context)
        {
            context.Refectories.Add(this);
            context.SaveChanges();
            return this.IdRefectory;
        }

        public int GetIdRefectoryIdByName(ApplicationDbContext context, string name)
        {
            var refectory = context.Refectories.FirstOrDefault(d => d.Name.Equals(name));
            if (refectory != null)
            {
                return refectory.IdRefectory;
            }
            else return 0;
        }
    }
}
