using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class Dormitory:BaseSection,IObjectPersister
    {
        [Key]
        public int IdDormitory { get; set; }
        public DormitoryEnum DormType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdDormitory = GetIdDormIdByName(Name);
            if (IdDormitory != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                  context.Dorms.Add(this);
            }
               
            context.SaveChanges();
            var dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(this.Name));
            
            return dorm.IdDormitory;
          
           
        }
        public int GetIdDormIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(name));

            if (dorm != null)
            {
                return dorm.IdDormitory;
            }
            else return 0;
        }
    }
}
