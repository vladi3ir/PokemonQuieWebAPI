using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeQuizWebAPI.PokemonDAL
{
    public interface IPokemonUserSQLStore
    {
        bool UpdateUserStatusAtQuizEnd(PokemonDALModel dalModel);
        bool InsertUserStatusAtQuizEnd(PokemonDALModel dalModel);
    }

}
