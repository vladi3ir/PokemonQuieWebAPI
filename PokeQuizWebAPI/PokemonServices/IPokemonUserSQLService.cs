using PokeQuizWebAPI.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IPokemonUserSQLService
    {
         Task CreatePokemonUserData(QuizAttemptResultsViewModel model);
    }
}
