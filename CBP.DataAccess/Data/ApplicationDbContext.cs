﻿using CBP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CBP.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Gallery> Gallerys { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<DogImage> DogImages { get; set; }
        public DbSet<ApplicationDetail> ApplicationDetails { get; set; }

        public DbSet<ApplicationHeader> ApplicationHeaders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>().HasData(
                new Dog
                {
                    Id = 1,
                    Name = "Joy",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Blonde",
                    Gender = "Female",
                    Dob = "10-2-2023"
                },
                new Dog
                {
                    Id = 2,
                    Name = "Sally",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Black",
                    Gender = "Female",
                    Dob = "10-3-2023"
                },
                new Dog
                {
                    Id = 3,
                    Name = "Cupid",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Dark Red",
                    Gender = "Male",
                    Dob = "10-3-2023"
                },
                new Dog
                {
                    Id = 4,
                    Name = "Comet",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Black",
                    Gender = "Male",
                    Dob = "10-2-2023"
                },
                new Dog
                {
                    Id = 5,
                    Name = "Holly",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Black",
                    Gender = "Female",
                    Dob = "10-2-2023"
                },
                new Dog
                {
                    Id = 6,
                    Name = "Jack",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Black",
                    Gender = "Male",
                    Dob = "10-2-2023"
                },
                new Dog
                {
                    Id = 7,
                    Name = "Hope",
                    Description = "Placeholder dog description here.",
                    ListPrice = 1500,
                    Color = "Dark Red",
                    Gender = "Female",
                    Dob = "10-2-2023"
                }

                );

            modelBuilder.Entity<Gallery>().HasData(
                new Gallery
                {
                    Id = 1,
                    GalleryImages = new List<GalleryImage>()
                });
        }
    }
}
