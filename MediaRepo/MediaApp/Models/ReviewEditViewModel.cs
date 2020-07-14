using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaApp.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediaApp.Models
{
    public class ReviewEditViewModel
    {

        public int MediaId { get; set; }
        public bool Approved { get; set; }
        public string Comment { get; set; }
        public int UserScore { get; set; }
        public string UserId { get; set; }
        public DateTime PublishedDate { get; set; }
    }

    
}