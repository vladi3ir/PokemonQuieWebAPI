using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonServices;

namespace PokeQuizWebAPI.Controllers
{
    public class QuizController : Controller
    {
        private readonly IPokemonService _pokemonService;
        private readonly IRandomizer _randomizer;
        private readonly ISession _session;
        //private Stack<int> PokemonAnswers = new Stack<int>();


        public QuizController
        (IPokemonService pokemonService, 
         IRandomizer randomizer, 
         IHttpContextAccessor httpContextAccessor)
        {
            _pokemonService = pokemonService;
            _randomizer = randomizer;
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

        public async Task<IActionResult> QuizView(QuizDifficultyViewModel userEnteredQuestion) //feeding into eds
        {
            var quizModel = new QuizViewModel();
            if (quizModel.PokemonAnswers.Count == 0)
            {
                quizModel.PokemonAnswers = _randomizer.RandomizeListOfAnsweres(userEnteredQuestion.SelectedNumberOfQuestions);
            }
            var testString = _session.GetString("pokemonStack");

            if (testString != null)
            {
                quizModel.PokemonAnswers = JsonConvert.DeserializeObject<Stack<int>>(_session.GetString("pokemonStack"));
            }

            quizModel.CorrectPokemon = await _pokemonService.MapPokemonInfo(quizModel.PokemonAnswers.Peek());
            var listOfWrongAnswers = _randomizer.RandomizeAditionalPokemon(quizModel.PokemonAnswers.Peek(), 4);
            quizModel.WrongAnswer1 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[0]);
            quizModel.WrongAnswer2 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[1]);
            quizModel.WrongAnswer3 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[2]);
            quizModel.PokemonAnswers.Pop();
            var storeStackIntoString = JsonConvert.SerializeObject(quizModel.PokemonAnswers);
            _session.SetString("pokemonStack", storeStackIntoString);
            return View(quizModel);
            
        }
        //public async Task<IActionResult> CreateQuiz(QuizDifficultyViewModel userEnteredQuestion) //feeding into eds
        //{
        //    var quizModel = new QuizViewModel();
        //    var pokemonAnswers = _randomizer.RandomizeListOfAnsweres(userEnteredQuestion.SelectedNumberOfQuestions);
        //    quizModel.CorrectPokemon = await _pokemonService.MapPokemonInfo(pokemonAnswers.Peek());
        //    var listOfWrongAnswers = _randomizer.RandomizeAditionalPokemon(pokemonAnswers.Peek(), 4);
        //    quizModel.WrongAnswer1 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[0]);
        //    quizModel.WrongAnswer2 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[1]);
        //    quizModel.WrongAnswer3 = await _pokemonService.MapPokemonInfo(listOfWrongAnswers[2]);
        //    pokemonAnswers.Pop();
        //    return View("QuizView", quizModel);
        //}

        //public IActionResult QuizView(QuizViewModel quizView)
        //{
        //    return View(quizView);
        //}

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