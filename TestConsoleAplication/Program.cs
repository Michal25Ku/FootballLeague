using FootballLeagueLib;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayedMatch nowyMecz = new PlayedMatch(1, 2);
            if (nowyMecz.ShootGoal(2, 1, 1))
                Console.WriteLine("Strzelono gola!");

        }
    }
}