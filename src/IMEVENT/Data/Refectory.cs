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

        public int Persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            
            Id = Convert.ToInt32(GetRecordID());
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
            return context.Refectories.Where(x => x.EventId == eventId).ToDictionary(x => x.Id, x => x);
        }

        public int GetIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Refectory refectory = context.Refectories.FirstOrDefault(d => d.Name.Equals(name));            
            return (refectory == null) ? 0 : refectory.Id;                        
        }

        public object GetRecordID()
        {
            return GetIdByName(Name);
        }
    }
}
