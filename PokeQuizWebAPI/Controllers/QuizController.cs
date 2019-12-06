using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonServices;


namespace PokeQuizWebAPI.Controllers
{
    public class QuizController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly ISession _session;
       
        public QuizController(IPokemonService pokemonService, IHttpContextAccessor httpContextAccessor)
        {
            _pokemonService = pokemonService;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectQuizDifficulty()
        {
            var viewModel = new QuizDifficultyViewModel();
            return View(viewModel);
        }

        public IActionResult QuizView(QuizDifficultyViewModel userEnteredQuestion) //feeding into eds
        {
            Stack myStack = new Stack();
            myStack.Push("Hello");
            myStack.Push("World");
            myStack.Push("!");


            return View();
        }

        public IActionResult SubmitPokemonId()
        {
            return View();
        }

        public async Task<IActionResult> GetPokemonDetails(int id)
        {
            var result = await _pokemonService.MapPokemonInfo(id);
            return View(result);
        }
    }
}