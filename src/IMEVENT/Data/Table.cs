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
        public int Id { get; set; }
        public int RefertoireId { get; set; }
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
            var table = context.Tables.FirstOrDefault(d => d.Name.Equals(name));
            if (table != null)
            {
                return table.Id;
            }

            return 0;
        }
    }
}
