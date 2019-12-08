using PokeQuizWebAPI.Models.QuizModels;
using PokeQuizWebAPI.PokemonDAL;
using System.Collections.Generic;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonUserSQLService
    {
        private readonly IPokemonUserSQLStore _pokemonUserSQLStore;

        public PokemonUserSQLService(IPokemonUserSQLStore pokemonUserSQLStore)
        {
            _pokemonUserSQLStore = pokemonUserSQLStore;
        }


        public QuizResultsViewModel CreatePokemonUserData(QuizResultsViewModel model)
        {


            var dalModel = new QuizViewModel();
            dalModel. = model.UserSelectedPlanet;
            _starwarsStore.InsertNewPlanet(dalModel);

            //MAPPING
            var dalProducts = _starwarsStore.SelectAllPlanets();
            var planets = new List<Planet>();

            foreach (var dalProduct in dalProducts)
            {
                var product = new Planet();
                product.Name = dalProduct.LinkToURL;
                planets.Add(product);
            }

            var StarwarsViewModel = new StarwarsViewModel();
            StarwarsViewModel.Planet = planets;

            return StarwarsViewModel;
        }
    }


}
