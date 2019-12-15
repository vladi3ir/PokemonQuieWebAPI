
using Identity.Dapper.Entities;
using PokeQuizWebAPI.PokemonDAL;
using System;
using System.Linq;

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

        public int PrecentileFinder(DapperIdentityUser user)
        {
            var currentUserScore = _pokemonUserSQLStore.SelectPlayerAverageScore(user.Id);
            var listOfUsers = _pokemonUserSQLStore.SelectAllScores();
            int numOfBottomPrecentile = 0;
            var userCount = listOfUsers.Count();

            foreach (var allPlayersScores in listOfUsers)
            {
                { if (allPlayersScores < currentUserScore) 
                    {
                        numOfBottomPrecentile += 1;
                    }
                }
            }

            var userPrecentile = (1 - (numOfBottomPrecentile / userCount));

            return userPrecentile;


        }

      
    }
}
