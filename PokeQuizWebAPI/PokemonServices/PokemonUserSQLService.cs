using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;
using System.Collections.Generic;
using Identity.Dapper.Entities;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService : IPokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }


        public void CreatePokemonUserData(QuizAttemptResultsViewModel model)
        {
            var dalModel = new PokemonDALModel();
            
            dalModel.TotalAccumlatiedPoints += model.AmountCorrect;
            dalModel.TotalPossiblePoints += model.QuestionsAttempted;
            dalModel.RecentTotalCorrect = model.AmountCorrect;
            dalModel.RecentAmountOfQuestions = model.QuestionsAttempted;
            dalModel.WhichQuizTaken = model.QuestionsAttempted.ToString();

            _pokemonUserSQLStore.UpdateUserStatusAtQuizEnd(dalModel);

        }
    }
}
