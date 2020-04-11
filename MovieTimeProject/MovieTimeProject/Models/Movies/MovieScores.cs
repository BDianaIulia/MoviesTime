using MovieTime.ApplicationLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTimeProject.Models.Movies
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

        private int percentCalculator(int total, int current)
        {
            return (current * 100) / total;
        }
    }
}
