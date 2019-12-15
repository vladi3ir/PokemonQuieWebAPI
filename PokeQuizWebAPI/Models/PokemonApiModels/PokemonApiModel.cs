namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class AllPokemonInfo
    {
        public string form_name { get; set; }
        public object[] form_names { get; set; }
        public int form_order { get; set; }
        public int id { get; set; }
        public bool is_battle_only { get; set; }
        public bool is_default { get; set; }
        public bool is_mega { get; set; }
        public string name { get; set; }
        public object[] names { get; set; }
        public int order { get; set; }
        public Pokemon pokemon { get; set; }
        public Sprites sprites { get; set; }
        public Version_Group version_group { get; set; }
    }

    public class Pokemon
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Sprites
    {
        public string back_default { get; set; }
        public string back_shiny { get; set; }
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }

    public class Version_Group
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    /// <summary>
    /// This was added to pull the full count of pokemon from the API
    /// </summary>

    public class Rootobject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
