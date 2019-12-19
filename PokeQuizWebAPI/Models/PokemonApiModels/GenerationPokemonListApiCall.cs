using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.Models.PokemonApiModels
{

    public class GenerationPokemonListApiCall
    {
        public object[] abilities { get; set; }
        public int id { get; set; }
        public Main_Region main_region { get; set; }
        public Move[] moves { get; set; }
        public string name { get; set; }
        public Name[] names { get; set; }
        public Pokemon_Species[] pokemon_species { get; set; }
        public Type[] types { get; set; }
        public Version_Groups[] version_groups { get; set; }
    }

    public class Main_Region
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Moves
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Names
    {
        public Language language { get; set; }
        public string name { get; set; }
    }

    public class Languages
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Pokemon_Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Version_Groups
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
