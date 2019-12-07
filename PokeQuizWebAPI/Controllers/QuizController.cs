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
            var correctAnswers = 0;
            _session.SetInt32("amountCorrect", correctAnswers);

            var viewModel = new QuizDifficultyViewModel();
            return View(viewModel);
        }

        public async Task<IActionResult> QuizView(QuizDifficultyViewModel userEnteredQuestion) //feeding into eds
        {
            userEnteredQuestion.SelectedNumberOfQuestions = userEnteredQuestion.SelectedNumberOfQuestions + 1;
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
            _session.SetString("pokemonAnswer", quizModel.CorrectPokemon.PokemonName);
            if (quizModel.PokemonAnswers.Count == 0)
            {
                return View("QuizResults");
            }
            
            return View(quizModel);
            
        }

        public async Task<ActionResult> CheckAnswer(string pokemonName)
        {
            var totalCorrectAnswers = _session.GetInt32("amountCorrect").GetValueOrDefault();
            if (pokemonName == _session.GetString("pokemonAnswer"))
            {

                totalCorrectAnswers++;
                _session.SetInt32("amountCorrect", totalCorrectAnswers);
            }
            QuizViewModel quizModel = await RunQuiz();
            if (quizModel.PokemonAnswers.Count == 0)
            {
                return View("QuizResults");
            }

            return View("QuizView", quizModel);
        }

        private async Task<QuizViewModel> RunQuiz()
        {
            var quizModel = new QuizViewModel();

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
            _session.SetString("pokemonAnswer", quizModel.CorrectPokemon.PokemonName);
            return quizModel;
        }

        public IActionResult QuizResults()
        {
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