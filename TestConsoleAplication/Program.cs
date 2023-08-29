using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using FootballLeagueLib.Table;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new FootballLeagueContext();

            db.Goals.RemoveRange(db.Goals.Select(m => m));
            db.Matches.RemoveRange(db.Matches.Select(m => m));
            db.SaveChanges();

            SeasonManager seaon1 = new SeasonManager();

            Club club = db.Clubs.FirstOrDefault(c => c.IdClub == 1);
            List<Match> MatchList = new List<Match>();
            MatchList = db.Matches.Select(m => m).Where(m => m.HomeTeamId == club.IdClub).OrderBy(m => m.Round).ToList();


            foreach (var m in MatchList)
            {
                Console.WriteLine(m.MatchName);
            }

        }
    }
}