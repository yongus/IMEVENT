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

        public void persist(ApplicationDbContext context)
        {
            context.Dorms.Add(this);
            context.SaveChanges();
        }
    }
}
