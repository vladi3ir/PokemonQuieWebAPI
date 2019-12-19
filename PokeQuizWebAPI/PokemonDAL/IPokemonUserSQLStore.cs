﻿using System.Collections.Generic;

namespace PokeQuizWebAPI.PokemonDAL
{
    public interface IPokemonUserSQLStore
    {
        bool UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel);
        bool InsertUserStatusAtQuizEnd(PokemonDALModel dalModel);
        PokemonDALModel GetUserScoreData(int userID);
        IEnumerable<float> SelectAllScores();
        float SelectPlayerAverageScore(int id);
        IEnumerable<string> SelectOrderedPlayers(int topNumb);
    }
}
