using PokeQuizWebAPI.Models.PokemonApiModels;
using PokeQuizWebAPI.Models.PokemonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PokeQuizWebAPI.PokemonApiCall
{
    public interface IPokemonApi
    {
        Task<AllPokemonInfo> GetPokemon(int id);
        Task<FullPokemonInfo> GetMorePokemonInfo(int id);
        Task<FullPokemonInfo> GetMorePokemonInfo(string name);
        Task<TypeFullApiModel> GetPokemonTypeInfo(string typeName);
        Task<EvolutionApiModel> DetermineIfPokemonHasEvolutionChain(int id);
        Task<EvolutionDetailsApiModel> GetEvolutionChain(string chainUrl);
        Task<GenerationPokemonListApiCall> GetPokemonByGeneration(int id);
    }
}
