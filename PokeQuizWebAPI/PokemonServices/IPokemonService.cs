using PokeQuizWebAPI.Models.PokemonViewModels;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonServices
{
    public interface IPokemonService
    {
        Task<PokemonResponse> MapPokemonInfo(int id);
        Task<PokedexViewModel> GetAdditionalPokemonInfo(int id);
        Task<PokedexViewModel> GetAdditionalPokemonInfo(string name);
        Task<TypeViewModel> GetTypeInformation(string typeName);
        Task<SelectPokemonViewModel> GetPokemonByGeneration();
    }
}

