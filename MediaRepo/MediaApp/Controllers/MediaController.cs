using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
                    Title = (string)thing.Title,
                    Description = (string)thing.Description,
                    Creator = (string)thing.Creator,
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
                Title = cvm.Title,
                Description = cvm.Description,
                Creator = cvm.Creator,
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
            evm.Title = (string)mediaToEdit.Title;
            evm.Description = (string)mediaToEdit.Description;
            evm.Creator = (string)mediaToEdit.Creator;
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
            changedmedia.Title = vm.Title;
            changedmedia.Description = vm.Description;
            changedmedia.Creator = vm.Creator;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int Id)
        {
            Media mediaToDetail = await _dbContext.medias.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == Id);
            var reviews = await _dbContext.Reviews.Where(x => x.MediaId == Id).ToListAsync();


            MediaDetailViewModel vm = new MediaDetailViewModel()
            {
                Id = mediaToDetail.Id,
                Category = mediaToDetail.Category.Name,
                Title = (string)mediaToDetail.Title,
                Description = (string)mediaToDetail.Description,
                Creator = (string)mediaToDetail.Creator,
                Date = (DateTime)mediaToDetail.Date,
                Rating = (int)mediaToDetail.Rating,
                PhotoUrl = mediaToDetail.PhotoUrl,
                Watched = mediaToDetail.Watched,
                Reviews = reviews
            };
            return View(vm);

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
                Title = (string)mediaToDelete.Title,
                Description = (string)mediaToDelete.Description,
                Creator = (string)mediaToDelete.Creator,
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

        //    [HttpGet]
        //    [Authorize]
        //    //public async Task<IActionResult>AddReview(MediaDetailViewModel vm)
        //    //{
        //    //    Review NewReview = new Review();
        //    //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //    IEnumerable<Review>reviewFromDb = await _dbContext.Reviews
        //    //        .Where(Review => NewReview.UserId == userId
        //    //        .ToListAsync();
        //    //    return (RedirectToAction("Detail"));
        //    //}

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Review(int Id, MediaDetailViewModel vm)
        {

            _dbContext.Reviews.Add(new Review()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Comment = vm.Comment,
                PublishedDate = DateTime.Now,
                MediaId = Id,
                UserScore = 5
            });

            await _dbContext.SaveChangesAsync();
            return (RedirectToAction("Detail", new { Id = Id }));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteReview(string userId, int mediaId)
        {
            Review ReviewToDelete = await _dbContext.Reviews.Where(x => x.MediaId == mediaId && x.UserId == userId).FirstOrDefaultAsync();
            //(x => x.MediaId == Id).ToListAsync()
            //.Where(MediaId => Review.MediaId) = MediaId || UserId => Review.UserId)

            _dbContext.Reviews.Remove(ReviewToDelete);
            await _dbContext.SaveChangesAsync();
            return View("Index");
        }
    }
}
        
        //Review rvm = new Review()
        //{
        //    Comment = ReviewToDelete.Comment,
        //    PublishedDate = ReviewToDelete.PublishedDate,
        //    MediaId = ReviewToDelete.MediaId,
        //    UserId = ReviewToDelete.UserId,
        //    UserScore = ReviewToDelete.UserScore,
        //};

        //return View(rvm);
        //}
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> ConfirmDeleteReview(string UserId)
        //{
        //    _dbContext.Reviews.Remove(_dbContext.Reviews.Find(UserId));
        //    await _dbContext.SaveChangesAsync();
        //    return (RedirectToAction("Index"));
        //}







