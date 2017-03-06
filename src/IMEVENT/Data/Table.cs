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
            Id = GetIdByName(Name);
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Tables.Add(this);
            }

            context.SaveChanges();
            return this.Id;
        }

        public int GetIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Table table = context.Tables.FirstOrDefault(d => d.Name.Equals(name));
            if (table != null)
            {
                return table.Id;
            }

            return 0;
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
