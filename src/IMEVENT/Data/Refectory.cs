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
        public int Id { get;  set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Id = GetIdByName(Name);
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Refectories.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }

        public static Dictionary<int, Refectory> GetRefectoryList(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Refectories.Where(x => x.Id == eventId).ToDictionary(x => x.Id, x => x);
        }

        public int GetIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var refectory = context.Refectories.FirstOrDefault(d => d.Name.Equals(name));
            if (refectory != null)
            {
                return refectory.Id;
            }

            return 0;
        }
    }
}
