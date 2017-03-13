using IMEVENT.SharedEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.Events;

namespace IMEVENT.Data
{
    public class Table:IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public int RefectoryId { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public RegimeEnum RegimeType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            this.Id = GetIdByProperties(Name, RefectoryId, RegimeType);
            if (Id == 0)
            {
                context.Tables.Add(this);                
            }
            else
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
            return this.Id;
        }

        public int GetIdByProperties(string name, int RefectoryId, RegimeEnum RegimeType)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Table table = context.Tables.FirstOrDefault(d => d.Name.Equals(name) 
                            && d.RefectoryId == RefectoryId && d.RegimeType == RegimeType);
            return (table == null)? 0 : table.Id;
        }

        public static Dictionary<int, Table> GetTableList(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Dictionary<int, Table> ret = 
                      (from Item1 in context.Refectories
                       join Item2 in context.Tables
                       on Item1.Id equals Item2.RefectoryId
                       select new { Item1, Item2 })
                       .Where(x => x.Item1.EventId == eventId)
                       .ToDictionary(x => x.Item2.Id, x => x.Item2
                      );

            return ret;            
        }        
    }
}
