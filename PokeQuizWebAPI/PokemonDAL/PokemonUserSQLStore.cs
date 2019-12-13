using Dapper;
using System.Collections.Generic;
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
        SET   TotalAccumlatiedPoints  +=  @{nameof(dalModel.TotalAccumlatiedPoints)},
              TotalPossiblePoints     +=  @{nameof(dalModel.TotalPossiblePoints)},
              QuizLength25Attempts    +=  @{nameof(dalModel.QuizLength25Attempts)},
              QuizLength50Attempts    +=  @{nameof(dalModel.QuizLength50Attempts)},
              QuizLength100Attempts   +=  @{nameof(dalModel.QuizLength100Attempts)},
              AverageScore            =   @{nameof(dalModel.AverageScore)},
              RecentAmountOfQuestions =   @{nameof(dalModel.RecentAmountOfQuestions)},
              RecentTotalCorrect      =   @{nameof(dalModel.RecentTotalCorrect)},
        WHERE Username                =   @{nameof(dalModel.Username)}";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);
                return true;
            }
        }

 
        public bool InsertUserStatusAtQuizEnd(PokemonDALModel dalModel)
            {
                var sql = $@"Insert INTO  
                        UserScoreData( Username, 
                        FK_UsernameID,
                        TotalAccumlatiedPoints, 
                        TotalPossiblePoints, 
                        QuizLength25Attempts, 
                        QuizLength50Attempts, 
                        QuizLength100Attempts, 
                        AverageScore, 
                        OverallPercent,
                        RecentAmountOfQuestions, 
                        RecentTotalCorrect, 
                        WhichQuizTaken, 
                        AttemptsPerQuiz) 

                    Values (@{nameof(dalModel.Username)},
                            @{nameof(dalModel.FK_UsernameID)},
                            @{nameof(dalModel.TotalAccumlatiedPoints)},
                            @{nameof(dalModel.TotalPossiblePoints)},
                            @{nameof(dalModel.QuizLength25Attempts)},
                            @{nameof(dalModel.QuizLength50Attempts)},
                            @{nameof(dalModel.QuizLength100Attempts)},
                            @{nameof(dalModel.AverageScore)},
                            @{nameof(dalModel.OverallPercent)},
                            @{nameof(dalModel.RecentAmountOfQuestions)},
                            @{nameof(dalModel.RecentTotalCorrect)},
                            @{nameof(dalModel.WhichQuizTaken)},
                            @{nameof(dalModel.AttemptsPerQuiz)})";

                using (var connection = new SqlConnection(_config.ConnectionString))
                {
                    var result = connection.Execute(sql, dalModel);
                    return true;
                }
            }

        public IEnumerable<float> SelectAllScores()
        {
            var sql = @"SELECT AverageScore FROM UserScoreData";

            using (var connection = new SqlConnection(_config.ConnectionString)) //Idisposable
            {
                var result = connection.Query<float>(sql);
                return result;
            }
        }

        public float SelectPlayerAverageScore(int id)
        {
            var sql = "SELECT AverageScore FROM UserScoreData Where FK_UsernameID @FK_UsernameID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<float>(sql, new { ProductID = id });

                return result;
            }
        }
    }
}
