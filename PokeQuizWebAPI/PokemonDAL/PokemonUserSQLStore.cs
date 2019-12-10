using Dapper;
using System.Data.SqlClient;


namespace PokeQuizWebAPI.PokemonDAL
{
    public class PokemonUserSQLStore : IPokemonUserSQLStore
    {
        private readonly Database _config;

        public PokemonUserSQLStore(PokemonConfig config)
        {
            _config = config.Database;
        }
        public bool UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel)
        {
            var sql = $@"UPDATE UserScoreData 
        SET   TotalAccumlatiedPoints  = TotalAccumlatiedPoints  +  @{nameof(dalModel.TotalAccumlatiedPoints)},
              TotalPossiblePoints     = TotalPossiblePoints     +  @{nameof(dalModel.TotalPossiblePoints)},
              QuizLength25Attempts    = QuizLength25Attempts    +  @{nameof(dalModel.QuizLength25Attempts)},
              QuizLength50Attempts    = QuizLength50Attempts    +  @{nameof(dalModel.QuizLength50Attempts)},
              QuizLength100Attempts   = QuizLength100Attempts   +  @{nameof(dalModel.QuizLength100Attempts)},
              AverageScore            = AverageScore            +  @{nameof(dalModel.AverageScore)},
              RecentAmountOfQuestions = RecentAmountOfQuestions +  @{nameof(dalModel.RecentAmountOfQuestions)},
              RecentTotalCorrect      = RecentTotalCorrect      +  @{nameof(dalModel.RecentTotalCorrect)},
        WHERE Username                = @{nameof(dalModel.Username)}";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);
                return true;
            }
        }

        public bool InsertNewUserStatusAtQuizEnd(PokemonDALModel dalModel)
        {
            var sql = $@"Insert INTO  UserScoreData(Username, TotalAccumlatiedPoints, TotalPossiblePoints, QuizLength25Attempts, QuizLength50Attempts, QuizLength100Attempts, AverageScore, RecentAmountOfQuestions, RecentTotalCorrect, WhichQuizTaken, AttemptsPerQuiz) 
                    Values (@{nameof(dalModel.Username)},@{nameof(dalModel.TotalAccumlatiedPoints)},@{nameof(dalModel.TotalPossiblePoints)},@{nameof(dalModel.QuizLength25Attempts)},@{nameof(dalModel.QuizLength50Attempts)},@{nameof(dalModel.QuizLength100Attempts)},@{nameof(dalModel.AverageScore)},@{nameof(dalModel.RecentAmountOfQuestions)},@{nameof(dalModel.RecentTotalCorrect)},@{nameof(dalModel.WhichQuizTaken)},@{nameof(dalModel.AttemptsPerQuiz)})";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);
                return true;
            }

        }
    }
}
