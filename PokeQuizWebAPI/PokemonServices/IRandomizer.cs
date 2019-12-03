using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IRandomizer
    {
        List<int> RandomizeAditionalPokemon(int answer , int amountOfPossibleAnswers);

        List<int> RandomizeListOfAnsweres(int quizLength);
        

    }
}
