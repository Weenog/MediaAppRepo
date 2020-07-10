
using MediaApp.Domain;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;



namespace MediaApp.Database
{
    public class MediaDbContext : IdentityDbContext<MediaAppIdentity>
    {
        public MediaDbContext(DbContextOptions<MediaDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>().HasKey(
                x => new { x.UserId, x.MediaId }
);
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Movie"
                },

                new Category()
                {
                    Id = 2,
                    Name = "Podcast"
                },

                new Category()
                {
                    Id = 3,
                    Name = "Music"
                },

                new Category()
                {
                    Id = 4,
                    Name = "Book"
                },

                new Category()
                {
                    Id = 5,
                    Name = "Game"
                },

                new Category()
                {
                    Id = 6,
                    Name = "Other"
                }

                );
        }

        public DbSet<Media> medias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}


