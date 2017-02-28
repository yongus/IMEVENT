using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class User:IdentityUser
    {
        public string UserId { get; set; }
        public DateTime DateofBirth { get; set; }
        public string TownId { get; set; }
        public string Sex { get; set; }
        public string Level { get; set; }
        public int Status { get; set; }                
        public string Language { get; set; }
        public SharingGroupCategoryEnum Category { get; set; }
        public String Telephone { get; set; }        
        public String InvitedBy { get; set; }
        public int IdGroup { get; set; }
        public int IdZone { get; set; }
        public int IdSousZone { get; set; }
        public string Town { get; set; }
        public bool IsGroupResponsible { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipLevelEnum MembershipLevel { get; set; }

        public override string ToString()
        {
            string ret = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}"
                 , LastName
                 , FirstName
                 , Sex
                 , Town
                 , ""//TODO - read group
                 , IsGroupResponsible ? "Oui" : "Non"
                 , MembershipLevel.MemberShipLevelToString()
                 , Category.SharingGroupCategoryToString()
                 , Language
                 , Email
                 , PhoneNumber
                 );

            return ret;
        }

        public int OriginZone { get; set; }

        /// <summary>
        /// gets the Id of the user associated with the full name passed as parameter.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fullName">Fullname in format LASTNAME-FIRSTMANE of the user</param>
        /// <returns></returns>
        public static String GetUserIdByName(string fullName)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            User user = context.Users.FirstOrDefault(d => (d.LastName + d.FirstName).Equals(fullName));
            if (user == null)
            {
                return String.Empty; 
            }

            return user.Id;
        }

        public static Dictionary<string, User> GetRegisteredUsers(List<string> keys)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Users.Where(x => keys.Contains(x.Id)).ToDictionary(x => x.Id, x => x);
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
