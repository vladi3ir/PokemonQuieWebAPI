using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonDAL
{
    public class PokemonDALModel
    {
        public string UserName { get; set; }
        public int Score { get; set; }
        public int QuizLength25 { get; set; }
        public int QuizLength50 { get; set; }
        public int QuizLength100 { get; set; }
        public int CorrectAmount { get; set; } 
        public float AverageScore { get; set; } 
        public int LastScoreSession { get; set; } 
        public string WhichQuizTaken { get; set; } 
        public int AttemptsPerQuiz { get; set; } 

    }
}
