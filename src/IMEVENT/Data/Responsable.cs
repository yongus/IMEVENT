using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Responsable:IObjectPersister
    {
        [Key]
        public int Id { get; set; }
        public int IdUSer { get; set; }
        public int IdEntity { get; set; }
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
                context.Responsables.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }

        public object GetRecordID()
        {
            return Id;
        }
    }
}
