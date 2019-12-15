using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeQuizWebAPI.Models.PokemonViewModels;
using PokeQuizWebAPI.PokemonServices;

namespace PokeQuizWebAPI.Controllers
{
    public class PokedexController : Controller
    {
        private readonly IPokemonService _pokemonService;
        public PokedexController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }
        public IActionResult SubmitPokemonId()
        {
            return View();
        }
        public async Task<IActionResult> GetPokemonDetails(int id)
        {
            var pokedexViewModel = new PokedexViewModel();
            pokedexViewModel = await _pokemonService.GetAdditionalPokemonInfo(id);

            return View(pokedexViewModel);
        }

        public async Task<IActionResult> GetPokemonTypeInfo(string name)
         {
            var typeViewModel = new TypeViewModel();
            typeViewModel = await _pokemonService.GetTypeInformation(name);
            return View(typeViewModel);
        }
    }
}