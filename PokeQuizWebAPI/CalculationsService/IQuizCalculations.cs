using Identity.Dapper.Entities;

namespace PokeQuizWebAPI.CalculationsService
{
    public interface IQuizCalculations
    {
        double CalculateCurrentAttemptScore(int questionsCorrect, int questionsAttempted);
        double PrecentileFinder (int userID);
        int RankFinder(int userID);
    }
}
