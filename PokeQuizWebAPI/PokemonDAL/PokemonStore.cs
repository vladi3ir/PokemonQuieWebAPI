using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonDAL
{
    public class PokemonStore:IPokemonStore
    {
        //public bool UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel)
        //{
        //    var sql = $@"Insert INTO UserScoreData Poketest(Username, Score, QuizLength25, QuizLength50, QuizLength100 , CorrectAmout , AverageScore  , LastScoreSession , WhichQuizTaken , AttempsPerQuiz) 
        //            Values (@{nameof(dalModel.UserName)},@{nameof(dalModel.Score)},@{nameof(dalModel.QuizLength25)},@{nameof(dalModel.QuizLength50)},@{nameof(dalModel.QuizLength100)},@{nameof(dalModel.CorrectAmount)},@{nameof(dalModel.AverageScore)},@{nameof(dalModel.LastScoreSession)},@{nameof(dalModel.WhichQuizTaken)},@{nameof(dalModel.AttemptsPerQuiz)})";

        //    using (var connection = new SqlConnection(_config.ConnectionString))
        //    {
        //        var result = connection.Execute(sql, dalModel);
        //        return true;
        //    }
        //}
    }
}
