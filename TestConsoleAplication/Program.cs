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

            db.Goals.RemoveRange(db.Goals.Select(m => m));
            db.Matches.RemoveRange(db.Matches.Select(m => m));
            db.SaveChanges();

            SeasonManager seaon1 = new SeasonManager();

            foreach(var r in seaon1.Rounds)
            {
                foreach(var m in r.Value)
                Console.WriteLine(m.ClubHost.ClubName + " - " + m.ClubGuest.ClubName);
            }

        }
    }
}