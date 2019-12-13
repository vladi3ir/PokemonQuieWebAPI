using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeQuizWebAPI.CalculationsService;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Controllers
{
    public class QuizController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly IRandomizer _randomizer;
        private readonly ISession _session;
        private readonly IQuizCalculations _quizCalulations;
        private readonly IQuizFlow _quizFlow;
        private readonly IPokemonUserSQLService _pokemonUserSQLService;

        public QuizController
        (IPokemonService pokemonService,
         IRandomizer randomizer,
         IHttpContextAccessor httpContextAccessor,
         IQuizCalculations quizCalculations,
         IQuizFlow quizFlow,
         IPokemonUserSQLService pokemonUserSQLService)

        {
            _pokemonService = pokemonService;
            _randomizer = randomizer;
            _session = httpContextAccessor.HttpContext.Session;
            _quizCalulations = quizCalculations;
            _quizFlow = quizFlow;
            _pokemonUserSQLService = pokemonUserSQLService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectQuizDifficulty()
        {
            var correctAnswers = 0;
            _session.SetInt32("amountCorrect", correctAnswers);

            var viewModel = new QuizDifficultyViewModel();
            return View(viewModel);
        }
        public async Task<IActionResult> QuizView(QuizDifficultyViewModel userEnteredQuestion, string pokemonName) //feeding into eds
        {
            QuizViewModel quizModel = await _quizFlow.SetupQuiz(userEnteredQuestion, pokemonName);
            if (quizModel.PokemonAnswers.Count == 0)
            {

                var quizResults = await _quizFlow.SetQuizResults();
                _pokemonUserSQLService.CreatePokemonUserData(quizResults);
                return View("QuizResults",quizResults);

            }
            return View(quizModel);
        }
        public IActionResult QuizResults()
        {
            var quizResultModel = new QuizAttemptResultsViewModel();
            return View(quizResultModel);
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