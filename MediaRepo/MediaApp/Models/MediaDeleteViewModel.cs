using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaApp.Domain;

namespace MediaApp.Models
{
    public class MediaDeleteViewModel
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
    
    }
}
