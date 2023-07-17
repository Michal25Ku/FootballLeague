using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class Season
    {
        public static int actualMatch = 0;
        public int RoundCount => (Db.Clubs.Count() * (Db.Clubs.Count()-1)) / 2;
        FootballLeague Db { get; }
        public List<MatchTracking> PlayedMatches { get; private set; }
        public List<Club> Clubs { get; private set; }

        public Season()
        {
            Db = new FootballLeague();
            PlayedMatches = new List<MatchTracking>();
            Clubs = Db.Clubs.ToList();

            GenerateAllMatches();
        }

        public void GenerateAllMatches()
        {
            List<string> pary = new List<string>();

            for (int round = 0; round < RoundCount; round++)
            {
                for (int match = 0; match < Clubs.Count; match++)
                {
                    int homeTeamIndex = (match + round) % Clubs.Count;
                    int awayTeamIndex = ((Clubs.Count / 2) + round) % Clubs.Count;

                    int homeTeamId = Clubs[homeTeamIndex].IdClub;
                    int awayTeamId = Clubs[awayTeamIndex].IdClub;
                    string para = homeTeamId + "-" + awayTeamId;

                    if (homeTeamId != awayTeamId)
                    {
                        if (!pary.Contains(para))
                        {
                            PlayedMatches.Add(new MatchTracking(1000, new PlayedMatch(homeTeamId, awayTeamId, DateTime.Now)));
                        }
                        pary.Add(para);
                    }
                }
            }

            foreach (var p in PlayedMatches)
            {
                Console.WriteLine(p.Match.HomeTeamName + "-" + p.Match.AwayTeamName);
            }
        }

        public void StartMatch()
        {
            PlayedMatches[actualMatch].StartMatch();
            actualMatch++;
        }
    }
}
