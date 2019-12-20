﻿using Identity.Dapper.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PokeQuizWebAPI.CalculationsService;
using PokeQuizWebAPI.Models.PlayerModels;
using PokeQuizWebAPI.PokemonDAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;
        private readonly UserManager<DapperIdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQuizCalculations _quizCalculations;

        public PlayerService(IPokemonUserSQLStore pokemonUserSQLStore, UserManager<DapperIdentityUser> userManager, IHttpContextAccessor httpsContextAccessor, IQuizCalculations quizCalculations)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
            _userManager = userManager;
            _httpContextAccessor = httpsContextAccessor;
            _quizCalculations = quizCalculations;
        }

        public async Task<PlayerRankModel> AssemblePlayerRank()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var playerRank = new PlayerRankModel
            {
                PlayerPerrcentile = _quizCalculations.PrecentileFinder(user.Id) * 100d,
                PlayerRank = _quizCalculations.RankFinder(user.Id),
                AverageScore = _pokemonUserSQLStore.SelectPlayerAverageScore(user.Id) * 100d,
                TopTenPlayers = new List<string> { },
                Username = user.UserName
            };
            
            foreach (var player in SelectTopTenPlayers())
            {
                playerRank.TopTenPlayers.Add(player);
            }
            
            return playerRank;
        }

        public IEnumerable<string> SelectTopTenPlayers()
        {
            var playerLists = new List<string>();
            foreach (var player in _pokemonUserSQLStore.SelectOrderedPlayers(10))
            {
                playerLists.Add(player);
            }
            return playerLists;
        }
    }
}
