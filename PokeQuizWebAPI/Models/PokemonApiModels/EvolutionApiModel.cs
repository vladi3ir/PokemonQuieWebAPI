using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonApiModels
{
    public class EvolutionApiModel
        {
            public Evolution_Chain evolution_chain { get; set; }
        }

        public class Evolution_Chain
        {
            public string url { get; set; }
        }

    
}
