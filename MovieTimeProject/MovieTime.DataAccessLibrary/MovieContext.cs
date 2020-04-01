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
                .HasKey(bc => new { bc.MovieId, bc.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.Genres)
                .HasForeignKey(bc => bc.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(bc => bc.Genre)
                .WithMany(c => c.Movies)
                .HasForeignKey(bc => bc.GenreId);


            //many to many Movie - User
            modelBuilder.Entity<UserMovieActivity>()
                .HasKey(bc => new { bc.MovieId, bc.UserId });
            modelBuilder.Entity<UserMovieActivity>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.RelatedListUsersActivity)
                .HasForeignKey(bc => bc.MovieId);
            modelBuilder.Entity<UserMovieActivity>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.RelatedListMovies)
                .HasForeignKey(bc => bc.UserId);



            //one to one Movie - Movie Rating
            modelBuilder.Entity<Movie>()
                .HasOne<MovieRating>(s => s.MovieRating)
                .WithOne(ad => ad.Movie)
                .HasForeignKey<MovieRating>(ad => ad.MovieId);


            //one to many Movie - Comments
            modelBuilder.Entity<Comment>()
                .HasOne<Movie>(s => s.Movie)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.MovieId);

            //one to many User - Comments
            modelBuilder.Entity<Comment>()
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Comments)
                .HasForeignKey(s => s.UserId);

        }

    }
}
