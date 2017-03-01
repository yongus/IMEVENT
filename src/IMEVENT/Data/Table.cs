using IMEVENT.SharedEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Table:IObjectPersister
    {
        [Key]
        public int IdTable { get; set; }
        public int IdRefertoire { get; set; }
        public int Capacite { get; set; }
        public bool ForSpecialRegime { get; set; }
        public string Name { get; set; }
        public RegimeEnum RegimeType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdTable = GetIdTableByName(Name);
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


        public int GetIdTableByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var table = context.Tables.FirstOrDefault(d => d.Name.Equals(name));
            if (table != null)
            {
                return table.IdTable;
            }

            return 0;
        }
    }
}
