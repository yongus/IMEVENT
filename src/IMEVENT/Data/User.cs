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
        public string Level { get; set; }
        public int Status { get; set; }
        public int Category { get; set; }
        public String Telephone { get; set; }
        public String Language { get; set; }
        public String InvitedBy { get; set; }
        public int IdGroup { get; set; }
        public int IdZone { get; set; }
        public int IdSousZone { get; set; }
        public string Town { get; set; }
        public bool IsGroupResponsible { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OriginZone { get; set; }

        /// <summary>
        /// gets the Id of the user associated with the full name passed as parameter.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fullName">Fullname in format LASTNAME-FIRSTMANE of the user</param>
        /// <returns></returns>
        public static String GetIdUserIdByName( string fullName)
        {
            if (String.IsNullOrEmpty(fullName))
            {
                return String.Empty;
            }
            fullName = fullName.Trim();
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var user = context.Users.FirstOrDefault(d => (d.LastName + d.FirstName).Equals(fullName));
            if (user != null)
            {
                return user.Id;
            }
            else return String.Empty;
        }

        public string persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            if (String.IsNullOrEmpty(Id))
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                context.Users.Add(this);
            }
           
            context.SaveChanges();
            return this.Id;
        }
    }
}
