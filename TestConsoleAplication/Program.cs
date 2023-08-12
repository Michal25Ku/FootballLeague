using FootballLeagueLib.Model;
using FootballLeagueLib.Season;
using FootballLeagueLib.Table;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new FootballLeague();

            db.Goals.RemoveRange();
            db.Matches.RemoveRange(db.Matches.Select(m => m));
            db.SaveChanges();

            SeasonManager seaon1 = new SeasonManager();

            TableData tb = new TableData();

            foreach(var c in tb.Table)
            {
                Console.WriteLine(c.Item1 + " " + c.Item2.ClubName + " " + c.Item3);
            }

        }
    }
}