﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IMEVENT.SharedEnums;

namespace IMEVENT.Data
{
    public class Hall:BaseSection,IObjectPersister
    {
       
        public HallSectionTypeEnum HallType { get; set; }
        [Key]
        public int Id { get;  set; }

        public int persist()
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            Id = GetHallIdByName(Name); 
               
            if (Id != 0)
            {
                context.Entry(this).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                
                context.Halls.Add(this);
            }
            
            context.SaveChanges();
            return this.Id;
        }

        public int GetHallIdByName(string name)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            var hall = context.Halls.FirstOrDefault(d => d.Name.Equals(name));
            if (hall != null)
            {
                return hall.Id;
            }
            else return 0;
        }


        public static Dictionary<int, Hall> GetAllHalls(int eventID)
        {
            ApplicationDbContext context = ApplicationDbContext.GetDbContext();
            return context.Halls.Where(x => x.Id == eventID).ToDictionary(x => x.Id, x => x);            

        }

    }
}
