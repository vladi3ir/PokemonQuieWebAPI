namespace PokeQuizWebAPI
{
    public class PokemonConfig
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }
}
