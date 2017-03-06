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
        public int Id { get; set; }
        public DormitoryTypeEnum DormType { get; set; }
       

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
                context.Dorms.Add(this);
            }
               
            context.SaveChanges();
            Dormitory dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(this.Name));
            
            return dorm.Id;                     
        }

        public int GetIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Dormitory dorm = context.Dorms.FirstOrDefault(d => d.Name.Equals(name));
            if (dorm != null)
            {
                return dorm.Id;
            }
            return 0;
        }

        public static Dictionary<int, Dormitory> GetDormitoryList(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Dorms.Where(x => x.Id == eventID).ToDictionary(x => x.Id, x => x);
        }
    }
}
