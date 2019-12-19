namespace PokeQuizWebAPI.Models.PokemonViewModels
{
    public class PokemonResponse
    {
        public string PokemonName { get; set; }
        public int PokemonId { get; set; }
        public string PokemonImageUrl { get; set; }
        public bool IsCorrect { get; set; }
    }
}
