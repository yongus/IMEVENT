using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMEVENT.Models.AccountViewModels
{
    public class UpdateUserViewModel
    {
        

        [Display(Name = "Date de Naissance")]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Now de Ville")]
        public Guid TownId { get; set; }

        [Display(Name = "Sex")]
        public String Sex { get; set; }

        [Display(Name = "Niveau d'engagement")]
        public int Level { get; set; }
       
        [Display(Name = "Numeros de Telephone")]
        public String Telephone { get; set; }
        [Display(Name = "Invite(e) par")]
        public String InvitedBy { get; set; }
        [Display(Name ="Groupe")]
        public int IdGroup { get; set; }
    }
}
