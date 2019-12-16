using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class PokedexViewModel
    {
        public string PokemonName { get; set; }
        public int PokemonId { get; set; }
        public string PokemonImageUrl { get; set; }
        public int PokemonWeight { get; set; }
        public List<string> PokemonTypes = new List<string>();
        public int PokemonHeight { get; set; }
        public List<PokemonStat> PokemonStats = new List<PokemonStat>();
        public string HaveEvolutionChain { get; set; }
        public List<PokemonResponse> EvolutionChain = new List<PokemonResponse>();
    }
}

