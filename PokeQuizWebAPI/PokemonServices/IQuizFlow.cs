using PokeQuizWebAPI.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IQuizFlow
    {
        Task<QuizViewModel> SetupQuiz(QuizDifficultyViewModel userEnteredQuestion, string pokemonName);
        Task<QuizAttemptResultsViewModel> SetQuizResults();
        int TotalQuetions { get; }
        int QuestionsCorrect { get; }
    }
}
