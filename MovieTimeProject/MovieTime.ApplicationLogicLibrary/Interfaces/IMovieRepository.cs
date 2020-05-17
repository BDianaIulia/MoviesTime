using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> getTopRatedListOfMovies();
        IEnumerable<Movie> getLatestListOfMovies();
        IEnumerable<Movie> getMayInterestListOfMovies();
        Movie getElementBy(Guid? id);
        void AddUserMovieActivity(UserMovieActivity userMovieActivity);
        void AddMovieReview(Guid idMovie, int reviewScore);
        void SaveMovieRating(Guid idMovie, MovieRating movieRating);
        int GetActualScoreForMovie(Guid idMovie);
        int GetNumberOfReviews(Guid idMovie);
        MovieRating GetMovieRatingFor(Guid idMovie);
        List<Movie> GetAllWithGenre(string genre);
    }
}
