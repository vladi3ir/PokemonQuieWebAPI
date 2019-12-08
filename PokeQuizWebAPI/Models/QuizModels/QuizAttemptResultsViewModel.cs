using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.QuizModels
{
    public class QuizAttemptResultsViewModel
    {
        public int AmountCorrect { get; set; }
        public int QuestionsAttempted { get; set; }
        public double ScoreThisAttempt { get; set; }

    }
}
