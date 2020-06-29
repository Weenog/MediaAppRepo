using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaApp.Database;
using MediaApp.Domain;
using MediaApp.Models;
using MediaApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;


namespace MediaApp.Controllers
{

    public class MediaController : Controller
    {

        private readonly MediaDbContext _dbContext;
        private readonly IPhotoService _photoService;

        public MediaController(IPhotoService photoService, MediaDbContext dbContext)
        {
            _photoService = photoService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<MediaListViewModel> XpList = new List<MediaListViewModel>();
            IEnumerable<Media> medias = await _dbContext.medias.Include(x => x.Category).ToListAsync();
            IEnumerable<Media> sortedmedias = medias.OrderBy(x => x.Date);
            var media = new MediaEditViewModel();

            foreach (var thing in sortedmedias)
            {
                MediaListViewModel Xp = new MediaListViewModel()
                {
                    Id = thing.Id,
                    Category = thing.Category.Name,
                    Description = (string)thing.Description,
                    Date = (DateTime)thing.Date,
                    Rating = (int)thing.Rating,
                    PhotoUrl = thing.PhotoUrl,
                    Watched = thing.Watched
                };
                XpList.Add(Xp);
            }
            return View(XpList);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            MediaCreateViewModel vm = new MediaCreateViewModel();
            vm.Date = DateTime.Now;
            var categories = await _dbContext.Categories.ToListAsync();
            foreach (Category category in categories)
            {
                vm.Category.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
            return View(vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MediaCreateViewModel cvm)
        {
            Media newmedia = new Media()
            {
                Rating = cvm.Rating,
                CategoryId = cvm.CategoryId,
                Description = cvm.Description,
                Date = cvm.Date,
                PhotoUrl = cvm.PhotoUrl,
                Watched = cvm.Watched
            };
            newmedia.Category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == newmedia.CategoryId);
            if (String.IsNullOrEmpty(newmedia.PhotoUrl))
            {
                _photoService.AssignPicTomedia(newmedia);
            }

            _dbContext.Add(newmedia);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            Media mediaToEdit = await _dbContext.medias.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            var categories = await _dbContext.Categories.ToListAsync();
            MediaEditViewModel evm = new MediaEditViewModel();

            foreach (Category category in categories)
            {
                evm.Category.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()

                {

                    Value = category.Id.ToString(),
                    Text = category.Name

                });

            }

            evm.Rating = (int)mediaToEdit.Rating;
            evm.CategoryId = mediaToEdit.Category.Id;
            evm.Description = (string)mediaToEdit.Description;
            evm.Date = (DateTime)mediaToEdit.Date;
            evm.Watched = mediaToEdit.Watched;

            return View(evm);

        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]


        public async Task<IActionResult> Edit(int id, MediaEditViewModel vm)
        {

            Media changedmedia = await _dbContext.medias.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            changedmedia.Rating = vm.Rating;
            changedmedia.CategoryId = vm.CategoryId;
            changedmedia.Description = vm.Description;
            changedmedia.Date = vm.Date;
            changedmedia.Watched = vm.Watched;

            var media = _dbContext.medias.SingleOrDefault(a => a.Id == id);
            _dbContext.Remove(media);
            _dbContext.medias.Update(changedmedia);
            await _dbContext.SaveChangesAsync();
            return (RedirectToAction("Index"));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateWatchStatus(int id)
        {
            var media = await _dbContext.medias.FirstOrDefaultAsync(x =>
x.Id
 == id);
            media.Watched = !media.Watched;
            await _dbContext.SaveChangesAsync();
            return (RedirectToAction("Index"));
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Media mediaToDelete = await _dbContext.medias.FindAsync(id);
            MediaDeleteViewModel dvm = new MediaDeleteViewModel()
            {
                Id = mediaToDelete.Id,
                Rating = (int)mediaToDelete.Rating,
                Description = (string)mediaToDelete.Description,
                Date = (DateTime)mediaToDelete.Date,
                Watched = mediaToDelete.Watched,
            };

            return View(dvm);
        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            _dbContext.medias.Remove(_dbContext.medias.Find(id));
            await _dbContext.SaveChangesAsync();
            return (RedirectToAction("Index"));
        }
    }
}






