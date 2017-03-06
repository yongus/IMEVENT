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
        public int IdTable { get; set; }
        public int IdRefectory { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public RegimeEnum RegimeType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdTable = GetTableIdByName(Name);
            if (IdTable != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Tables.Add(this);
            }

            context.SaveChanges();
            return this.IdTable;
        }


        public int GetTableIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Table table = context.Tables.FirstOrDefault(d => d.Name.Equals(name));
            if (table != null)
            {
                return table.IdTable;
            }

            return 0;
        }

        public static Dictionary<int, Table> GetAllTables(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Dictionary<int, Table> ret = (from Item1 in context.Refectories
                       join Item2 in context.Tables
                       on Item1.IdRefectory equals Item2.IdRefectory
                       select new { Item1, Item2 })
                       .Where(x => x.Item1.IdEvent == eventId)
                       .ToDictionary(x => x.Item2.IdTable, x => x.Item2);

            return ret;            
        }        
    }
}
