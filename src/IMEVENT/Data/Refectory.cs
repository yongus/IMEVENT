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
        public int NumberOfTable { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdRefectory = GetIdRefectoryByName(Name);
            if (IdRefectory != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Refectories.Add(this);
            }
           
            context.SaveChanges();
            return this.IdRefectory;
        }

        public static Dictionary<int, Refectory> GetAllRefs(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Refectories.Where(x => x.IdEvent == eventId).ToDictionary(x => x.IdRefectory, x => x);
        }

        public int GetIdRefectoryByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var refectory = context.Refectories.FirstOrDefault(d => d.Name.Equals(name));
            if (refectory != null)
            {
                return refectory.IdRefectory;
            }

            return 0;
        }
    }
}
