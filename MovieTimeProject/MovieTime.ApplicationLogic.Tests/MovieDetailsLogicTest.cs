using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieTime.ApplicationLogicLibrary.Helpers;
using MovieTime.ApplicationLogicLibrary.Models;

namespace MovieTime.ApplicationLogic.Tests
{
    [TestClass]
    public class MovieDetailsLogicTest
    {
        [TestMethod]
        public void AddingANewReviewToMovie()
        {
            MovieScores movieService = new MovieScores();

            Movie movieCase1 = new Movie
            {
                Id = new System.Guid(),
                ReviewScoreValue = 0
            };

            MovieRating movieRatingCase1 = new MovieRating
            {
                Id = new System.Guid(),
                NumberOf1ReviewStars = 0,
                NumberOf2ReviewStars = 0,
                NumberOf3ReviewStars = 0,
                NumberOf4ReviewStars = 0,
                NumberOf5ReviewStars = 0
            };

            int actualScore1 = movieService.BuildNewScoreForMovie(movieRatingCase1, 4, movieCase1.ReviewScoreValue);
            Assert.AreEqual(4, actualScore1);
            Assert.IsTrue(movieRatingCase1.NumberOf4ReviewStars == 1);




            Movie movieCase2 = new Movie
            {
                Id = new System.Guid(),
                ReviewScoreValue = 3
            };

            MovieRating movieRatingCase2 = new MovieRating
            {
                Id = new System.Guid(),
                NumberOf1ReviewStars = 0,
                NumberOf2ReviewStars = 0,
                NumberOf3ReviewStars = 1,
                NumberOf4ReviewStars = 0,
                NumberOf5ReviewStars = 0
            };

            int actualScore2 = movieService.BuildNewScoreForMovie(movieRatingCase2, 5, movieCase2.ReviewScoreValue);
            Assert.AreEqual(4, actualScore2);
            Assert.IsTrue(movieRatingCase2.NumberOf5ReviewStars == 1);




            Movie movieCase3 = new Movie
            {
                Id = new System.Guid(),
                ReviewScoreValue = 4
            };

            MovieRating movieRatingCase3 = new MovieRating
            {
                Id = new System.Guid(),
                NumberOf1ReviewStars = 0,
                NumberOf2ReviewStars = 0,
                NumberOf3ReviewStars = 2,
                NumberOf4ReviewStars = 10,
                NumberOf5ReviewStars = 3
            };

            int actualScore3 = movieService.BuildNewScoreForMovie(movieRatingCase3, 1, movieCase3.ReviewScoreValue);
            Assert.AreEqual(3, actualScore3);
            Assert.IsTrue(movieRatingCase3.NumberOf1ReviewStars == 1);
        }



        [TestMethod]
        public void MovieScoresPercents()
        {
            Movie movieCase1 = new Movie
            {
                Id = new System.Guid(),
                MovieRating = new MovieRating
                {
                    Id = new System.Guid(),
                    NumberOf1ReviewStars = 0,
                    NumberOf2ReviewStars = 0,
                    NumberOf3ReviewStars = 2,
                    NumberOf4ReviewStars = 2,
                    NumberOf5ReviewStars = 0
                }
            };
            MovieScores movieService1 = new MovieScores(movieCase1);

            int threeSPercent = movieService1.percentCalculator(4, 2);
            int fourSPercent = movieService1.percentCalculator(4, 2);
            int fiveSPercent = movieService1.percentCalculator(4, 0);
            Assert.AreEqual(50, threeSPercent);
            Assert.AreEqual(50, fourSPercent);
            Assert.AreEqual(0, fiveSPercent);

            Assert.IsTrue(movieService1.threeSPercent == 50);
            Assert.IsTrue(movieService1.fourSPercent == 50);




            Movie movieCase2 = new Movie
            {
                Id = new System.Guid(),
                MovieRating = new MovieRating
                {
                    Id = new System.Guid(),
                    NumberOf1ReviewStars = 1,
                    NumberOf2ReviewStars = 0,
                    NumberOf3ReviewStars = 1,
                    NumberOf4ReviewStars = 0,
                    NumberOf5ReviewStars = 4
                }
            };
            MovieScores movieService2 = new MovieScores(movieCase2);

            int oneSPercent2 = movieService2.percentCalculator(6, 1);
            int threeSPercent2 = movieService2.percentCalculator(6, 1);
            int fiveSPercent2 = movieService2.percentCalculator(6, 4);
            Assert.AreEqual(16, oneSPercent2);
            Assert.AreEqual(16, threeSPercent2);
            Assert.AreEqual(66, fiveSPercent2);

            Assert.IsTrue(movieService2.twoSPercent == 0);
            Assert.IsTrue(movieService2.oneSPercent == 16);
            Assert.IsTrue(movieService2.fiveSPercent == 66);



            Movie movieCase3 = new Movie
            {
                Id = new System.Guid(),
                MovieRating = new MovieRating
                {
                    Id = new System.Guid(),
                    NumberOf1ReviewStars = 0,
                    NumberOf2ReviewStars = 10,
                    NumberOf3ReviewStars = 15,
                    NumberOf4ReviewStars = 5,
                    NumberOf5ReviewStars = 10
                }
            };
            MovieScores movieService3 = new MovieScores(movieCase3);

            int twoSPercent3 = movieService3.percentCalculator(40, 10);
            int threeSPercent3 = movieService3.percentCalculator(40, 15);
            Assert.AreEqual(25, twoSPercent3);
            Assert.AreEqual(37, threeSPercent3);

            Assert.IsTrue(movieService3.fourSPercent == 12);
            Assert.IsTrue(movieService3.fiveSPercent == 25);
        }
    }
}
