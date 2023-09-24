using Labb1_Minimal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1_Minimal_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Book> book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = 1,
                Title = "The Night",
                Author = "Filip",
                Genre = "Horror",
                IsAvalible = true,
                Description = "Horror book based on teenagers getting lost in the deep forest.",
                Released = new DateTime(23 / 5 / 2014)
            },
            new Book()
            {
                Id = 2,
                Title = "Hungergames",
                Author = "Anas",
                Genre = "Thriller",
                IsAvalible = false,
                Description = "Fighting for survival, one man standing.",
                Released = new DateTime(5 / 11 / 2009)
            },
            new Book()
            {
                Id = 3,
                Title = "Urasic Park",
                Author = "Ulrika",
                Genre = "Action",
                IsAvalible = true,
                Description = "Island Zoo full of dinosours, but things get out of hand.",
                Released = new DateTime(28/1/1999)
            },
            new Book()
            {
                Id = 4,
                Title = "The Meg",
                Author = "Isabella",
                Genre = "Horror",
                IsAvalible = true,
                Description = "Explorer tries to get rid off big white shark that is causing problems for residents.",
                Released = new DateTime(23/5/2014)
            },
            new Book()
            {
                Id = 5,
                Title = "Star Wars",
                Author = "Joakim",
                Genre = "Science fiction",
                IsAvalible = true,
                Description = "The good verses the evel fight over the galaxy.",
                Released = new DateTime(23/5/2014)
            }
            );

        }
    }
}
