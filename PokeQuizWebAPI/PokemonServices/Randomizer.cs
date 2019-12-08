using PokeQuizWebAPI.Models.PokemonViewModels;
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
            var aditionalFillerAnswers = new List<int>();
            var rand = new Random();
            
            //Neeed to get actual length of lsit here!
            var pokemonListLength = 807;
            ////////////

            for (int i = 0; i < amountOfPossibleAnswers-1; i++)
            {
                int temp;
                do
                {
                    do
                    {
                       
                        temp = rand.Next(1, pokemonListLength);

                    } while (answer == temp);
                } while (aditionalFillerAnswers.Contains(temp));

                aditionalFillerAnswers.Add(temp);
            }
            
            return aditionalFillerAnswers;
        }

        public  Stack<int> RandomizeListOfAnsweres(int quizLength)
        {
          var answerStack = new Stack<int>();
            var rand = new Random();

            //Neeed to get actual length of lsit here!
            var pokemonListLength = 807;
            ////////////
            
            for (int i = 0; i < quizLength; i++)
            {
                int temp;
                do
                {
                        temp = rand.Next(1, pokemonListLength);
                    
                } while (answerStack.Contains(temp));

                answerStack.Push(temp);
            }

            return answerStack;
        }

        public List<PokemonResponse> RandomizePossibleAnswerOrder(List<PokemonResponse> pokeAnswers)
        {
            var reorderedAnswerList = new List<PokemonResponse>();
            var rand = new Random();
            var originalLength = pokeAnswers.Count();

            
                do
                {
                    var temp = rand.Next(0, pokeAnswers.Count - 1);
                    if (!reorderedAnswerList.Contains(pokeAnswers[temp]))
                    {
                        reorderedAnswerList.Add(pokeAnswers[temp]);

                    }
                    pokeAnswers.Remove(pokeAnswers[temp]);
                } while (reorderedAnswerList.Count < originalLength);
            
            return reorderedAnswerList;
        }
    }
}
