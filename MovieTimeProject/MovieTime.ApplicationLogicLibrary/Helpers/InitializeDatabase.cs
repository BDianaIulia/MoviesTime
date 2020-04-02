using MovieTime.ApplicationLogicLibrary.Interfaces;
using MovieTime.ApplicationLogicLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MovieTime.ApplicationLogicLibrary.Helpers
{
    public class InitializeDatabase
    {
        IGenreRepository _genreRepository;
        IMovieRepository _movieRepository;
        public InitializeDatabase(IGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
        }
        public void collectMovieData()
        {
            StreamReader r = new StreamReader("moviesDbJson.txt");
            var json = r.ReadToEnd();

            List<MovieJson> allMoviesJson = JsonConvert.DeserializeObject<List<MovieJson>>(json);


            foreach (var movieJson in allMoviesJson)
            {
                if (movieJson.PosterPath != null)
                {
                    //movieJson.PosterPath = "1A1jKAoUe0Ksbe9YMhY1P9kFMwU";
                    if (File.Exists(@"coverPhotos\" + movieJson.PosterPath))
                    {
                        ICollection<MovieGenre> genresFromAMovie = new List<MovieGenre>();

                        movieJson.Genres.Distinct();
                        foreach (var genreName in movieJson.Genres)
                        {
                            MovieGenre movieGenre = new MovieGenre();

                            Genre newGenre;
                            Genre existingGenre = _genreRepository.GetGenreAfterName(genreName);

                            if (existingGenre == null)
                            {
                                newGenre = new Genre { GenreName = genreName };
                                newGenre.Movies = new List<MovieGenre>();
                            }
                            else
                            {
                                newGenre = existingGenre;
                            }
                            movieGenre.Genre = newGenre;

                            newGenre.Movies.Add(movieGenre);
                            genresFromAMovie.Add(movieGenre);
                        }


                        Movie newMovie = new Movie()
                        {
                            Title = movieJson.Title,
                            Overview = movieJson.Overview,
                            ReleaseDate = movieJson.ReleaseDate,
                            PosterPath = movieJson.PosterPath,
                            Popularity = movieJson.Popularity
                        };

                        foreach (var movieGenre in genresFromAMovie)
                        {
                            movieGenre.Movie = newMovie;
                        }

                        newMovie.Genres = genresFromAMovie;

                        try
                        {
                            _movieRepository.Add(newMovie);
                            _movieRepository.SaveChanges();
                        }
                        catch { }
                    }
                }

            }
        }
    }
}
