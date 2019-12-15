using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class TypeViewModel
    {
        public string TypeName { get; set; }
        public List<PokemonType> DoubleDamageFrom = new List<PokemonType>();
        public List<PokemonType> DoubleDamageTo = new List<PokemonType>();
        public List<PokemonType> HalfDamageFrom = new List<PokemonType>();
        public List<PokemonType> HalfDamageTo = new List<PokemonType>();
        public List<PokemonType> NoDamageFrom = new List<PokemonType>();
        public List<PokemonType> NoDamageTo = new List<PokemonType>();
    }
}
