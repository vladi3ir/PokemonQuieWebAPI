using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokeQuizWebAPI.Models.PokemonApiModels;
using PokeQuizWebAPI.Models.PokemonViewModels;

namespace PokeQuizWebAPI.PokemonApiCall
{
    public class PokemonApi : IPokemonApi
    {
        public async Task<FullPokemonInfo> GetMorePokemonInfo(int id)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/pokemon/{id}");

                return JsonConvert.DeserializeObject<FullPokemonInfo>(json);


            }
        }

        public async Task<AllPokemonInfo> GetPokemon(int id)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {
                
                var json = await httpClient.GetStringAsync($"/api/v2/pokemon-form/{id}");


                return JsonConvert.DeserializeObject<AllPokemonInfo>(json);


            }
        }

        public async Task<TypeFullApiModel> GetPokemonTypeInfo(string typeName)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/type/{typeName}");

                return JsonConvert.DeserializeObject<TypeFullApiModel>(json);
            }
        }
    }
}

