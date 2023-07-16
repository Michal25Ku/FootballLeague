using FootballLeagueLib;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayedMatch nowyMecz = new PlayedMatch(1, 2);

            MatchTracking rozegrajMecz = new MatchTracking(12, nowyMecz);
            rozegrajMecz.StartMatch();

        }
    }
}