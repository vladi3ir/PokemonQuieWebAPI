using PokeQuizWebAPI.Models.QuizModels;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IQuizFlow
    {
        Task<QuizViewModel> SetupQuiz(QuizDifficultyViewModel userEnteredQuestion, string pokemonName);
        Task<QuizAttemptResultsViewModel> SetQuizResults();
        void ResetSession();
        int TotalQuetions { get; }
        int QuestionsCorrect { get; }
    }
}
