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
        public async Task<EvolutionApiModel> DetermineIfPokemonHasEvolutionChain(int id)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/pokemon-species/{id}/");

                return JsonConvert.DeserializeObject<EvolutionApiModel>(json);


            }
        }

        public async Task<EvolutionDetailsApiModel> GetEvolutionChain(string chainUrl)
        {
            var uriHalf = chainUrl.Substring(0, 18);
            var secondHalf = chainUrl.Substring(18);
            using (var httpClient = new HttpClient { BaseAddress = new Uri($"{uriHalf}") })
            {

                var json = await httpClient.GetStringAsync($"{secondHalf}");
                

                return JsonConvert.DeserializeObject<EvolutionDetailsApiModel>(json);


            }
        }

        public async Task<FullPokemonInfo> GetMorePokemonInfo(int id)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/pokemon/{id}");

                return JsonConvert.DeserializeObject<FullPokemonInfo>(json);


            }
        }
        public async Task<FullPokemonInfo> GetMorePokemonInfo(string name)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/pokemon/{name}");

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

        public async Task<GenerationPokemonListApiCall> GetPokemonByGeneration(int id)
        {
            
            using (var httpClient = new HttpClient { BaseAddress = new Uri("https://pokeapi.co") })
            {

                var json = await httpClient.GetStringAsync($"/api/v2/generation/{id}");

                return JsonConvert.DeserializeObject<GenerationPokemonListApiCall>(json);
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

