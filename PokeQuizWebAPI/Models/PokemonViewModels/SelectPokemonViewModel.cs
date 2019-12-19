using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class SelectPokemonViewModel
    {
        public string PokemonSelected { get; set; }
        public List<PokemonResponse> Gen1 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen2 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen3 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen4 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen5 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen6 = new List<PokemonResponse>();
        public List<PokemonResponse> Gen7 = new List<PokemonResponse>();
    }
}
