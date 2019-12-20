﻿using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace PokeQuizWebAPI.PokemonDAL
{
    public class PokemonUserSQLStore : IPokemonUserSQLStore
    {
        private readonly PokemonConfig _config;

        public PokemonUserSQLStore(PokemonConfig config)
        {
            _config = config;
        }

        public bool UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel)
        {

            var sql = $@"
                UPDATE UserScoreData 
                SET   
                    TotalAccumlatiedPoints  =  @{nameof(dalModel.TotalAccumlatiedPoints)},
                    TotalPossiblePoints     =  @{nameof(dalModel.TotalPossiblePoints)},
                    QuizLength25Attempts    =  @{nameof(dalModel.QuizLength25Attempts)},
                    QuizLength50Attempts    =  @{nameof(dalModel.QuizLength50Attempts)},
                    QuizLength100Attempts   =  @{nameof(dalModel.QuizLength100Attempts)},
                    OverallPercent          =  @{nameof(dalModel.OverallPercent)},
                    RecentAmountOfQuestions =  @{nameof(dalModel.RecentAmountOfQuestions)},
                    RecentTotalCorrect      =  @{nameof(dalModel.RecentTotalCorrect)},
                    AttemptsPerQuiz         =  @{nameof(dalModel.AttemptsPerQuiz)}
                WHERE FK_UsernameID         =  @{nameof(dalModel.FK_UsernameID)}";


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
            var sql = @"SELECT OverallPercent FROM UserScoreData";

            using (var connection = new SqlConnection(_config.ConnectionString)) //Idisposable
            {
                var result = connection.Query<float>(sql);
                return result;
            }
        }

        public float SelectPlayerAverageScore(int id)
        {
            var sql = "SELECT OverallPercent FROM UserScoreData Where FK_UsernameID = @UserID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<float>(sql, new { UserID = id });
                return result;
            }
        }


        public PokemonDALModel GetUserScoreData(int userID)
        {

            var sql = @"
                SELECT *
                FROM UserScoreData 
                WHERE FK_UsernameID = @ActiveUserID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<PokemonDALModel>(sql, new { ActiveUserID = userID });
                return result;
            }
        }

        public IEnumerable<string> SelectOrderedPlayers(int topNumb)
        {
            var sql = @"
                SELECT Top (@TopNums) Username
                FROM UserScoreData 
                ORDER BY OverallPercent DESC";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Query<string>(sql, new { TopNums = topNumb });
                return result;
            }

        }
    }
}
