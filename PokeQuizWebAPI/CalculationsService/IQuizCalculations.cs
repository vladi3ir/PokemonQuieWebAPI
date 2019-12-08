using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.CalculationsService
{
    public interface IQuizCalculations
    {
        double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted);
    }
}
