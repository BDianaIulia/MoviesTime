using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MovieTime.ApplicationLogicLibrary.Helpers
{
    public class MovieScores
    {
        public int oneStar = 0;
        public int twoStars = 0;
        public int threeStars = 0;
        public int fourStars = 0;
        public int fiveStars = 0;

        public int oneSPercent = 0;
        public int twoSPercent = 0;
        public int threeSPercent = 0;
        public int fourSPercent = 0;
        public int fiveSPercent = 0;

        public int NumberOfReviewsForMovie = 0;

        public MovieScores()
        {
        }
        public MovieScores(Movie movie)
        {
            if( movie.MovieRating != null )
            {
                oneStar = movie.MovieRating.NumberOf1ReviewStars;
                twoStars = movie.MovieRating.NumberOf2ReviewStars;
                threeStars = movie.MovieRating.NumberOf3ReviewStars;
                fourStars = movie.MovieRating.NumberOf4ReviewStars;
                fiveStars = movie.MovieRating.NumberOf5ReviewStars;

                NumberOfReviewsForMovie = oneStar + twoStars + threeStars + fourStars + fiveStars;

                oneSPercent = percentCalculator(NumberOfReviewsForMovie, oneStar);
                twoSPercent = percentCalculator(NumberOfReviewsForMovie, twoStars);
                threeSPercent = percentCalculator(NumberOfReviewsForMovie, threeStars);
                fourSPercent = percentCalculator(NumberOfReviewsForMovie, fourStars);
                fiveSPercent = percentCalculator(NumberOfReviewsForMovie, fiveStars);
            }
        }

        public int percentCalculator(int total, int current)
        {
            return (current * 100) / total;
        }

        public int BuildNewScoreForMovie(MovieRating movieRating, int reviewScore, int OldScore)
        {
            int numberOfReviews = movieRating.NumberOf1ReviewStars + movieRating.NumberOf2ReviewStars +
                                    movieRating.NumberOf3ReviewStars + movieRating.NumberOf4ReviewStars + movieRating.NumberOf5ReviewStars;

            Type type = movieRating.GetType();
            PropertyInfo prop = type.GetProperty(rating[reviewScore]);
            int lastValue = (int)prop.GetValue(movieRating);

            prop.SetValue(movieRating, lastValue + 1, null);

            return (OldScore * numberOfReviews + reviewScore) / (numberOfReviews + 1);
        }


        private Dictionary<int, string> rating = new Dictionary<int, string>
        {
            { 1, "NumberOf1ReviewStars" },
            { 2, "NumberOf2ReviewStars" },
            { 3, "NumberOf3ReviewStars" },
            { 4, "NumberOf4ReviewStars" },
            { 5, "NumberOf5ReviewStars" }
        };
    }
}
