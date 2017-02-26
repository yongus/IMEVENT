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
       
        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdRefectory = GetIdRefectoryIdByName(Name);
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

        public int GetIdRefectoryIdByName( string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var refectory = context.Refectories.FirstOrDefault(d => d.Name.Equals(name));
            if (refectory != null)
            {
                return refectory.IdRefectory;
            }
            else return 0;
        }
    }
}
