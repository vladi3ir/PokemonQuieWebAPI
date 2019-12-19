using PokeQuizWebAPI.Models.PlayerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PlayerServices
{
    public interface IPlayerService
    {
        IEnumerable<string> SelectTopTenPlayers();
        Task<PlayerRankModel> AssemblePlayerRank();
    }
}
