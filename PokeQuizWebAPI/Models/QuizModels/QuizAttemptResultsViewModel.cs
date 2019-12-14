using System.Collections.Generic;

namespace PokeQuizWebAPI.Models.QuizModels
{
    public class QuizAttemptResultsViewModel
    {
        public int AmountCorrect { get; set; }
        public int QuestionsAttempted { get; set; }
        public double ScoreThisAttempt { get; set; }
        public List<string> CorrectAnswers = new List<string>();
        public List<string> SelectedAnswers = new List<string>();

    }
}
