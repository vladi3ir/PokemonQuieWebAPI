﻿using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;
using System.Collections.Generic;
using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService : IPokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly HttpContext _httpContext;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore, UserManager<DapperIdentityUser> userManager, HttpContext httpsContext)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
            _userManager = userManager;
            _httpContext = httpsContext;
        }


        public async  Task CreatePokemonUserData(QuizAttemptResultsViewModel model)
        {
            
            var user = await _userManager.GetUserAsync(_httpContext.User);
            var dalModel = new PokemonDALModel();

            dalModel.Username = user.UserName;
            dalModel.TotalAccumlatiedPoints += model.AmountCorrect;
            dalModel.TotalPossiblePoints += model.QuestionsAttempted;
            dalModel.RecentTotalCorrect = model.AmountCorrect;
            dalModel.RecentAmountOfQuestions = model.QuestionsAttempted;
            dalModel.WhichQuizTaken = model.QuestionsAttempted.ToString();

            _pokemonUserSQLStore.UpdateUserStatusAtQuizEnd(dalModel);




          
        }


    }
}
