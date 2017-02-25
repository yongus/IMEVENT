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
        public int Level { get; set; }
        public int Status { get; set; }                
        public string Language { get; set; }        
        public int IdGroup { get; set; }
        public string Town { get; set; }
        public bool IsGroupResponsible { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MembershipLevelEnum MembershipLevel { get; set; }
        public SharingGroupCategoryEnum Category { get; set; }                        

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
    }
}
