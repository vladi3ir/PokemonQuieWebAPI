using PokeQuizWebAPI.Models.PokemonViewModels;
using System.Collections.Generic;

namespace PokeQuizWebAPI.Models.QuizModels
{
    public class QuizViewModel
    {
        public PokemonResponse CorrectPokemon { get; set; }
        public PokemonResponse WrongAnswer1 { get; set; }
        public PokemonResponse WrongAnswer2 { get; set; }
        public PokemonResponse WrongAnswer3 { get; set; }
        public Stack<int> PokemonAnswers = new Stack<int>();
        public PokemonResponse SelectedAnswer { get; set; }
        public List<PokemonResponse> QuizAnswers = new List<PokemonResponse>();
        public List<string> ListOfAnswers = new List<string>();
        public List<string> UserAnswers = new List<string>();
    }
}

