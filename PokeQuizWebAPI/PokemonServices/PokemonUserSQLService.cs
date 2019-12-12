using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService : IPokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }


        public void CreatePokemonUserData(QuizAttemptResultsViewModel model)
        {


            var dalModel = new PokemonDALModel();
            dalModel.TotalAccumlatiedPoints += model.AmountCorrect;
            dalModel.TotalPossiblePoints += model.QuestionsAttempted;
            dalModel.RecentTotalCorrect = model.AmountCorrect;
            dalModel.RecentAmountOfQuestions = model.QuestionsAttempted;
            dalModel.WhichQuizTaken = model.QuestionsAttempted.ToString();

            _pokemonUserSQLStore.UpdateUserStatusAtQuizEnd(dalModel);




            //    //_starwarsStore.InsertNewPlanet(dalModel);

            //    ////MAPPING
            //    //var dalProducts = _starwarsStore.SelectAllPlanets();
            //    //var planets = new List<Planet>();

            //    //foreach (var dalProduct in dalProducts)
            //    //{
            //    //    var product = new Planet();
            //    //    product.Name = dalProduct.LinkToURL;
            //    //    planets.Add(product);
            //    //}

            //    //var StarwarsViewModel = new StarwarsViewModel();
            //    //StarwarsViewModel.Planet = planets;

            //    //return StarwarsViewModel;
            //}
        }


    }
}
