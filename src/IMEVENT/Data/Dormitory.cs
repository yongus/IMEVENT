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

        public int persist(ApplicationDbContext context)
        {
            
            context.Dorms.Add(this);
            context.SaveChanges();
            var dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(this.Name));
            
            return dorm.IdDormitory;
          
           
        }
        public Nullable<Int32> GetIdDormIdByName(ApplicationDbContext context,string name)
        {
            var dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(name));
            if (dorm != null)
            {
                return dorm.IdDormitory;
            }
            else return null;
        }
    }
}
