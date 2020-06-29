
using MediaApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MediaApp.Models
{
    public class StatisticsIndexViewModel

    {

        public Media Highestrating { get; set; }
        public Groupedmedias HighestDaymedia { get; set; }
        public IEnumerable<Groupedmedias> Monthlymedias { get; set; }
        public Media Lowestmedia { get; set; }
        public IEnumerable<Media> medias { get; set; }
        public Groupedmedias MostPopular { get; set; }
        public Groupedmedias LeastPopular { get; set; }


    }

}