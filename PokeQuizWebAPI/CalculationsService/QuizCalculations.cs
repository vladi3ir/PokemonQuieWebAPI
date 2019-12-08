using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.CalculationsService
{
    public class QuizCalculations : IQuizCalculations
    {
        public double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted)
        {
            var percentScoreThisAttempt = 0.0;

            percentScoreThisAttempt = (questionsCorrect / questionsAttempted) * 100;

            return percentScoreThisAttempt;
        }
    }
}
