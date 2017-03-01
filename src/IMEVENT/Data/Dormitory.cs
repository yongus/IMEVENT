using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class Dormitory:BaseSection,IObjectPersister
    {
        [Key]
        public int IdDormitory { get; set; }
        public DormitoryTypeEnum DormType { get; set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            IdDormitory = GetDormIdByName(Name);
            if (IdDormitory != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Dorms.Add(this);
            }
               
            context.SaveChanges();
            Dormitory dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(this.Name));
            
            return dorm.IdDormitory;                     
        }

        public int GetDormIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Dormitory dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(name));
            if (dorm != null)
            {
                return dorm.IdDormitory;
            }
            return 0;
        }

        public static Dictionary<int, Dormitory> GetAllDorms(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Dorms.Where(x => x.IdEvent == eventID).ToDictionary(x => x.IdDormitory, x => x);
        }
    }
}
