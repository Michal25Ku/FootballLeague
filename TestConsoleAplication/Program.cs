using FootballLeagueLib;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayedMatch nowyMecz = new PlayedMatch(1, 2);
            PlayedMatch nowyMecz1 = new PlayedMatch(1, 2);

            MatchTracking rozegrajMecz = new MatchTracking(50, nowyMecz1);
            rozegrajMecz.StartMatch();

        }
    }
}