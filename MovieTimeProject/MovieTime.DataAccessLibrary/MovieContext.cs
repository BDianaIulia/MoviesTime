using Microsoft.EntityFrameworkCore;
using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.DataAccessLibrary
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> contextOptions)
            : base(contextOptions)
        { }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<MovieRating> MovieRating { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many to many Movie - Genre
            modelBuilder.Entity<MovieGenre>()
                .HasKey(bc => new { bc.IdMovie, bc.IdGenre });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.Genres)
                .HasForeignKey(bc => bc.IdMovie);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(bc => bc.Genre)
                .WithMany(c => c.Movies)
                .HasForeignKey(bc => bc.IdGenre);


            //many to many Movie - User
            modelBuilder.Entity<UserMovieActivity>()
                .HasKey(bc => new { bc.IdMovie, bc.IdUser });
            modelBuilder.Entity<UserMovieActivity>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.RelatedListUsersActivity)
                .HasForeignKey(bc => bc.IdMovie);
            modelBuilder.Entity<UserMovieActivity>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.RelatedListMovies)
                .HasForeignKey(bc => bc.IdUser);


            //one to one Movie - Movie Rating
            modelBuilder.Entity<Movie>()
                .HasOne<MovieRating>(s => s.MovieRating)
                .WithOne(ad => ad.Movie)
                .HasForeignKey<MovieRating>(ad => ad.IdMovie);


            //one to many Movie - Comments
            modelBuilder.Entity<Comment>()
                .HasOne<Movie>(s => s.Movie)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.IdMovie);

            //one to many User - Comments
            modelBuilder.Entity<Comment>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.IdUser);

            modelBuilder.Entity<Movie>()
                    .Property(b => b.ReviewScoreValue)
                    .HasDefaultValue(0);

            foreach (var numberOfReviews in _numberOfReviews)
            {
                modelBuilder.Entity<MovieRating>()
                    .Property(numberOfReviews.Key)
                    .HasDefaultValue(numberOfReviews.Value);
            }
        }

        Dictionary<string, int> _numberOfReviews = new Dictionary<string, int>()
        {
            { nameof(MovieTime.ApplicationLogicLibrary.Models.MovieRating.NumberOf1ReviewStars), 0 },
            { nameof(MovieTime.ApplicationLogicLibrary.Models.MovieRating.NumberOf2ReviewStars), 0 },
            { nameof(MovieTime.ApplicationLogicLibrary.Models.MovieRating.NumberOf3ReviewStars), 0 },
            { nameof(MovieTime.ApplicationLogicLibrary.Models.MovieRating.NumberOf4ReviewStars), 0 },
            { nameof(MovieTime.ApplicationLogicLibrary.Models.MovieRating.NumberOf5ReviewStars), 0 }
        };

    }
}
