using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class Table:BaseSection,IObjectPersister
    {
        [Key]
        public int TableId { get; set; }
        public int RefectoryId { get; set; }        
        public RegimeEnum RegimeType { get; set; }        

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            TableId = GetTableIdByName(Name);
            if (TableId != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Tables.Add(this);
            }

            context.SaveChanges();
            return this.TableId;
        }


        public int GetTableIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Table table = context.Tables.FirstOrDefault(d => d.Name.Equals(name));
            if (table != null)
            {
                return table.TableId;
            }

            return 0;
        }

        public static Dictionary<int, Table> GetAllTables(int eventId)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Tables.Where(x => x.IdEvent == eventId).ToDictionary(x => x.TableId, x => x);
        }        
    }
}
