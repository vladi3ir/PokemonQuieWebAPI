using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonDAL
{
    public class PokemonDALModel
    {

        public string Username { get; set; }
        public int TotalAccumlatiedPoints { get; set; }
        public int TotalPossiblePoints { get; set; }

        public int QuizLength25Attempts { get; set; }
        public int QuizLength50Attempts { get; set; }
        public int QuizLength100Attempts { get; set; }

        public float AverageScore { get; set; } 
        public int RecentAmountOfQuestions { get; set; } 
        public int RecentTotalCorrect { get; set; }

        public string WhichQuizTaken { get; set; } 
        public int AttemptsPerQuiz { get; set; } 

    }
}
