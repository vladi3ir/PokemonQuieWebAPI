using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PokeQuizWebAPI.CalculationsService;
using PokeQuizWebAPI.Models.QuizModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public class QuizFlow : IQuizFlow
    {
        private readonly ISession _session;
        private readonly IRandomizer _randomizer;
        private readonly IPokemonService _pokemonService;
        private readonly IQuizCalculations _quizCalculations;

        public QuizFlow
            (IHttpContextAccessor httpContextAccessor,
            IRandomizer randomizer,
            IPokemonService pokemonService,
            IQuizCalculations quizCalculations)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _randomizer = randomizer;
            _pokemonService = pokemonService;
            _quizCalculations = quizCalculations;
        }
        public async Task<QuizViewModel> SetupQuiz(QuizDifficultyViewModel userEnteredQuestion, string pokemonName)
        {
            var quizModel = new QuizViewModel();
            var testSession = _session.GetString("answerList");
            var testSession2 = _session.GetString("userAnswer");
            if (testSession != null)
            {
                quizModel.ListOfAnswers = JsonConvert.DeserializeObject<List<string>>(_session.GetString("answerList"));
            }
            if (pokemonName != null)
            {
                if (testSession2 != null)
                {
                    quizModel.UserAnswers = JsonConvert.DeserializeObject<List<string>>(_session.GetString("userAnswer"));
                }

                quizModel.UserAnswers.Add(pokemonName);
                var sessionUserAnswers = JsonConvert.SerializeObject(quizModel.UserAnswers);
                _session.SetString("userAnswer", sessionUserAnswers);
            }

            var totalCorrectAnswers = _session.GetInt32("amountCorrect").GetValueOrDefault();
            if (pokemonName == _session.GetString("pokemonAnswer") & pokemonName != null)
            {

                totalCorrectAnswers++;
                _session.SetInt32("amountCorrect", totalCorrectAnswers);
            }
            if (userEnteredQuestion.SelectedNumberOfQuestions != 0)
            {
                _session.SetInt32("questionsAttempted", userEnteredQuestion.SelectedNumberOfQuestions);
            }

            userEnteredQuestion.SelectedNumberOfQuestions = userEnteredQuestion.SelectedNumberOfQuestions + 1;

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

            _session.SetString("pokemonAnswer", quizModel.CorrectPokemon.PokemonName);

            if (quizModel.PokemonAnswers.Count != 1)
            {
                quizModel.ListOfAnswers.Add(quizModel.CorrectPokemon.PokemonName);
            }
            quizModel.PokemonAnswers.Pop();

            var storeStackIntoString = JsonConvert.SerializeObject(quizModel.PokemonAnswers);
            var storeAnswerListIntoString = JsonConvert.SerializeObject(quizModel.ListOfAnswers);

            _session.SetString("pokemonStack", storeStackIntoString);
            _session.SetString("answerList", storeAnswerListIntoString);

            quizModel.QuizAnswers.Add(quizModel.CorrectPokemon);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer1);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer2);
            quizModel.QuizAnswers.Add(quizModel.WrongAnswer3);
            quizModel.QuizAnswers = _randomizer.RandomizePossibleAnswerOrder(quizModel.QuizAnswers);

            return quizModel;
        }

        public async Task<QuizAttemptResultsViewModel> SetQuizResults()
        {
            var quizResults = new QuizAttemptResultsViewModel();
            quizResults.AmountCorrect = _session.GetInt32("amountCorrect") ?? 0;
            quizResults.QuestionsAttempted = _session.GetInt32("questionsAttempted") ?? 0;
            quizResults.ScoreThisAttempt = _quizCalculations.CalculateCurrentAttemptScore(quizResults.AmountCorrect, quizResults.QuestionsAttempted);
            quizResults.CorrectAnswers = JsonConvert.DeserializeObject<List<string>>(_session.GetString("answerList"));
            quizResults.SelectedAnswers = JsonConvert.DeserializeObject<List<string>>(_session.GetString("userAnswer"));
            _session.Clear();
            return quizResults;
        }


        public int TotalQuetions => _session.GetInt32("questionsAttempted") ?? 0;
        public int QuestionsCorrect => _session.GetInt32("amountCorrect") ?? 0;
    }
}
