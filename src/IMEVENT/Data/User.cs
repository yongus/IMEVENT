using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMEVENT.Data
{
    public class User:IdentityUser
    {
        public DateTime DateofBirth { get; set; }
        public Guid TownId { get; set; }
        public String Sex { get; set; }
        public int Level { get; set; }
        public int Status { get; set; }
        public int Category { get; set; }
        public String Telephone { get; set; }
        public String Language { get; set; }
        public String InvitedBy { get; set; }
        public int IdGroup { get; set; }
        

    }
}
