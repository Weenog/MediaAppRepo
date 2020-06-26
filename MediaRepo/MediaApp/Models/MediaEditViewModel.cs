using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaApp.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaApp.Models
{
    public class MediaEditViewModel
    {
       
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Category { get; set; } = new List<SelectListItem>();
    }

    
}