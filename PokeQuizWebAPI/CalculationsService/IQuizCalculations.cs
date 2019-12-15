using Identity.Dapper.Entities;

namespace PokeQuizWebAPI.CalculationsService
{
    public interface IQuizCalculations
    {
        double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted);
        int PrecentileFinder(DapperIdentityUser user);
    }
}
