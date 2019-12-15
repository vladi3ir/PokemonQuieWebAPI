using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeQuizWebAPI.Models.PokemonViewModels;
using PokeQuizWebAPI.PokemonApiCall;

namespace PokeQuizWebAPI.PokemonServices
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonApi _pokemonApi;

        public PokemonService(IPokemonApi pokemonApi)
        {
            _pokemonApi = pokemonApi;
        }
        public async Task<PokemonResponse> MapPokemonInfo(int id)
        {
            var apiPokemon =  await _pokemonApi.GetPokemon(id);
            var pokemon = new PokemonResponse();

            pokemon.PokemonName = apiPokemon.name;
            pokemon.PokemonId = apiPokemon.id;
            pokemon.PokemonImageUrl = apiPokemon.sprites.front_default;

            return pokemon;
        }

        
    }
}
