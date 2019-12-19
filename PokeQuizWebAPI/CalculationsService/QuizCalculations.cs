
using Identity.Dapper.Entities;
using PokeQuizWebAPI.PokemonDAL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.CalculationsService
{
    public class QuizCalculations : IQuizCalculations
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public QuizCalculations(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }
        public double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted)
        {
            var percentScoreThisAttempt = 0.0;
            var amountCorrect = Convert.ToDouble(questionsCorrect);
            var amountAttempted = Convert.ToDouble(questionsAttempted);

            percentScoreThisAttempt = (amountCorrect / amountAttempted) * 100;

            return percentScoreThisAttempt;
        }

        public double PrecentileFinder(int userID)
        {
            var currentUserScore = _pokemonUserSQLStore.SelectPlayerAverageScore(userID);
            var listOfUsers = _pokemonUserSQLStore.SelectAllScores();
            var numOfBottomPrecentile = 0;
            var userCount = listOfUsers.Count();


            foreach (var allPlayersScores in listOfUsers)
            {
                {
                    if (allPlayersScores < currentUserScore)
                    {
                        numOfBottomPrecentile += 1;
                    }
                }
            }

            var userPrecentile = (1d - (Convert.ToDouble(numOfBottomPrecentile) / Convert.ToDouble(userCount)));

            return userPrecentile;
        }

        public int RankFinder(int userID)
        {
            var currentUserScore = _pokemonUserSQLStore.SelectPlayerAverageScore(userID);
            var listOfUsers = _pokemonUserSQLStore.SelectAllScores();
            var numOfBottomPrecentile = 0;
            var userCount = listOfUsers.Count();

            foreach (var allPlayersScores in listOfUsers)
            {
                {
                    if (allPlayersScores < currentUserScore)
                    {
                        numOfBottomPrecentile += 1;
                    }
                }
            }
            return userCount-numOfBottomPrecentile;
        }


    }
}
