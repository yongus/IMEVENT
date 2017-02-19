using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IMEVENT.Models.EventViewModels
{
    public class EventCreateViewModel 
    {
        [Required]
        public string EventName { get; set; }
    }
}
