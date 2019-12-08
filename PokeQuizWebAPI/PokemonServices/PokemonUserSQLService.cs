using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;
using System.Collections.Generic;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }


        //public QuizResultsViewModel CreatePokemonUserData(QuizResultsViewModel model)
        //{

        //}
    }


}
