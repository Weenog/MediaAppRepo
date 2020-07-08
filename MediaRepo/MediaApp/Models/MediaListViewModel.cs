﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaApp.Domain;

namespace MediaApp.Models
{
    public class MediaListViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; }
        public string PhotoUrl { get; set; }
        public bool Watched { get; set; }


    }
}
