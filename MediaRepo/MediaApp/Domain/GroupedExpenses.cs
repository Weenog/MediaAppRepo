using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;



namespace MediaApp.Domain
{
    public class Groupedmedias
    {
        public DateTime Date { get; set; }
        public decimal Rating { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }

}