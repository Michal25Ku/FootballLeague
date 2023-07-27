using FootballLeagueLib.Model;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeagueLib.DataForWPF
{
    public class Season
    {
        public static int actualMatch = 0;
        public int RoundCount => (Db.Clubs.Count() - 1) * 2;
        public int ClubCount => Db.Clubs.Count();
        FootballLeague Db { get; }
        public List<MatchTracking> Matches { get; private set; }
        public List<Club> Clubs { get; private set; }
        public List<List<MatchTracking>> Rounds { get; private set; }

        public Season()
        {
            Db = new FootballLeague();
            Matches = new List<MatchTracking>();
            Clubs = Db.Clubs.ToList();

            GenerateAllMatches();
        }

        public void GenerateAllMatches()
        {
            Rounds = new List<List<MatchTracking>>();

            for (int round = 0; round < ClubCount; round++)
            {
                for (int clubHome = 0; clubHome < ClubCount; clubHome++)
                {
                    int homeTeamId = Clubs[clubHome].IdClub;
                    int awayTeamId = Clubs[(clubHome + round) % ClubCount].IdClub;

                    if (awayTeamId != homeTeamId)
                    {
                        Console.WriteLine(homeTeamId + "-" + awayTeamId);
                        Matches.Add(new MatchTracking(1000, new Model.Match(homeTeamId, awayTeamId, DateTime.Now)));
                    }
                }
            }

            Rounds.Add(Matches);
        }

        public void StartRound()
        {
            if (actualMatch < Rounds.Count)
            {
                foreach (var match in Rounds[actualMatch])
                {
                    match.StartMatch();
                }
                actualMatch++;
            }
            else
            {
                Console.WriteLine("Season completed. All matches have been played.");
            }
        }
    }
}
