using MediaApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MediaApp.Models
{

    public class MediaDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public bool Watched { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public string Comment { get; set; }

    }

}