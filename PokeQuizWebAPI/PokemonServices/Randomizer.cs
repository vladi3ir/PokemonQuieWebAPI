using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public class Randomizer : IRandomizer
    {
        public List<int> RandomizeAditionalPokemon(int answer, int amountOfPossibleAnswers)
        {
            var aditionalFillerAnswers = new List<int> { };

            return aditionalFillerAnswers;
        }

        public List<int> RandomizeListOfAnsweres(int quizLength)
        {
          var answerList = new List<int> { };


            return answerList;
        }
    }
}
