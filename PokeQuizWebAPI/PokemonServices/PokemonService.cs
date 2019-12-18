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

        public async Task<PokedexViewModel> GetAdditionalPokemonInfo(int id)
        {
            var apiPokemonInfo = await _pokemonApi.GetMorePokemonInfo(id);
            var pokemon = new PokedexViewModel();
            pokemon.PokemonWeight = apiPokemonInfo.weight;
            pokemon.PokemonName = apiPokemonInfo.name;
            pokemon.PokemonId = apiPokemonInfo.id;
            pokemon.PokemonImageUrl = apiPokemonInfo.sprites.front_default;
            pokemon.PokemonHeight = apiPokemonInfo.height;

            var apiModel =  await _pokemonApi.DetermineIfPokemonHasEvolutionChain(id);
            pokemon.HaveEvolutionChain = apiModel.evolution_chain.url;
            if (pokemon.HaveEvolutionChain != null)
            {
                var apiChain = await _pokemonApi.GetEvolutionChain(pokemon.HaveEvolutionChain);

                var pokemonEvolutionBaby = new PokemonResponse();
                pokemonEvolutionBaby.PokemonName = apiChain.chain.species.name;
                var apiImageUrlCall = await _pokemonApi.GetMorePokemonInfo(pokemonEvolutionBaby.PokemonName);
                pokemonEvolutionBaby.PokemonImageUrl = apiImageUrlCall.sprites.front_default;

                
                var pokemonEvolution2 = new PokemonResponse();
                pokemonEvolution2.PokemonName = apiChain.chain.evolves_to[0].species.name;
                var apiImageUrlCall2 = await _pokemonApi.GetMorePokemonInfo(pokemonEvolution2.PokemonName);
                pokemonEvolution2.PokemonImageUrl = apiImageUrlCall2.sprites.front_default;
                pokemon.EvolutionChain.Add(pokemonEvolutionBaby);
                pokemon.EvolutionChain.Add(pokemonEvolution2);

                if (apiChain.chain.evolves_to[0].evolves_to[0].species.name != null)
                {
                    var pokemonEvolution3 = new PokemonResponse();
                    pokemonEvolution3.PokemonName = apiChain.chain.evolves_to[0].evolves_to[0].species.name;
                    var apiImageUrlCall3 = await _pokemonApi.GetMorePokemonInfo(pokemonEvolution3.PokemonName);
                    pokemonEvolution3.PokemonImageUrl = apiImageUrlCall3.sprites.front_default;
                    pokemon.EvolutionChain.Add(pokemonEvolution3);
                }
               
            }

            foreach (var stat in apiPokemonInfo.stats)
            {
                var pokemonStat = new PokemonStat();
                pokemonStat.StatName = stat.stat.name;
                pokemonStat.PointsInStat = stat.base_stat;
                pokemon.PokemonStats.Add(pokemonStat);
            }

            foreach (var type in apiPokemonInfo.types)
            {
                pokemon.PokemonTypes.Add(type.type.name);
            }
            return pokemon;
        }

        public async Task<TypeViewModel> GetTypeInformation(string typeName)
        {
            var apiType = await _pokemonApi.GetPokemonTypeInfo(typeName);
            var pokemonType = new TypeViewModel();

            pokemonType.TypeName = apiType.name;

            foreach (var type in apiType.damage_relations.double_damage_from)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.DoubleDamageFrom.Add(thisType);
            }

            foreach (var type in apiType.damage_relations.double_damage_to)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.DoubleDamageTo.Add(thisType);
            }
            foreach (var type in apiType.damage_relations.half_damage_from)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.HalfDamageFrom.Add(thisType);
            }
            foreach (var type in apiType.damage_relations.half_damage_to)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.HalfDamageTo.Add(thisType);
            }
            foreach (var type in apiType.damage_relations.no_damage_from)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.NoDamageFrom.Add(thisType);
            }
            foreach (var type in apiType.damage_relations.no_damage_to)
            {
                var thisType = new PokemonType();
                thisType.TypeName = type.name;
                thisType.TypeUrl = type.url;
                pokemonType.NoDamageTo.Add(thisType);
            }
            return pokemonType;
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
