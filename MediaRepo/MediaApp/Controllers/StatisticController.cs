
using System;
using System.Collections.Generic;
using System.Linq;
using MediaApp.Database;
using System.Threading.Tasks;
using MediaApp.Domain;
using MediaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaApp.Controllers
{
    public class StatisticsController : Controller
    {
       
        //private readonly IEnumerable<media> _medias;
        private readonly MediaDbContext _mediaDatabase;

        public StatisticsController(MediaDbContext mediaDatabase)
        {
            _mediaDatabase = mediaDatabase;
            //_medias = _mediaDatabase.Getmedias();
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            IEnumerable<Media> _medias = await _mediaDatabase.medias.ToListAsync();
            StatisticsIndexViewModel vm = new StatisticsIndexViewModel()
            {
                medias = _medias,
                Highestmedia = _medias.OrderByDescending(x => x.Rating).First(),
                Lowestmedia = _medias.OrderBy(x => x.Rating).First(),
                Monthlymedias = _medias.GroupBy(x => new { x.Date.Date.Month, x.Date.Year }).Select(g => new Groupedmedias { Date = new DateTime(g.Key.Year, g.Key.Month, 01), Rating = g.Sum(m => m.Rating) }).OrderBy(x => x.Date),
                HighestDaymedia = _medias.GroupBy(x => x.Date.Date).Select(x => new Groupedmedias { Date = x.Key, Rating = x.Sum(m => m.Rating) }).OrderByDescending(x => x.Rating).First(),
                MostExpensive = _medias.GroupBy(x => x.Category).Select(g => new Groupedmedias { Category = g.Key, Rating = g.Sum(m => (decimal)m.Rating) }).OrderByDescending(x => x.Rating).First(),
                LeastExpensive = _medias.GroupBy(x => x.Category).Select(g => new Groupedmedias { Category = g.Key, Rating = g.Sum(m => (decimal)m.Rating) }).OrderBy(x => x.Rating).First()
            };
            return View(vm);
        }
    }
}