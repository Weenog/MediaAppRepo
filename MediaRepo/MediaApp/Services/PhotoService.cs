﻿using MediaApp.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MediaApp.Services
{
    public class PhotoService : IPhotoService
    {

        private readonly IWebHostEnvironment _hostEnvironment;
        public PhotoService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }


        public void DeletePicture(string url)
        {
            if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, url);
            System.IO.File.Delete(pathName);
        }



        public string UploadPicture(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, "media-pics");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return uniqueFileName;
        }

        public void AssignPicTomedia(Media media)
        {
            string pathToPicture = media.Category.Name.ToLower() + ".png";
            media.PhotoUrl = "/media-pics/" + pathToPicture;
        }
    }
}