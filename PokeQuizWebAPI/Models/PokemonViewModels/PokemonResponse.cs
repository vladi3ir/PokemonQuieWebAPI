using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class PokemonResponse
    {
        public string PokemonName { get; set; }
        public int PokemonId { get; set; }
        public string PokemonImageUrl { get; set; }
    }
}
