using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class Round
    {
        Random rand = new Random();
        public static int actualRound = 1;
        public int RoundCount => Db.Clubs.Count() * 2 - 2;
        FootballLeague Db { get; }
        public List<MatchTracking> PlayedMatches { get; private set; }
        public List<Club> Clubs { get; private set; }

        public Round()
        {
            Db = new FootballLeague();
            PlayedMatches = new List<MatchTracking>();
            Clubs = Db.Clubs.ToList();

            int matchPerRound = Clubs.Count / 2;

            if (actualRound < RoundCount - 1)
            {
                for (int i = 0; i < RoundCount; i++)
                {
                    for (int j = 0; j < matchPerRound; j++)
                    {
                        int homeTeamIndex = (i + j) % (Clubs.Count - 1);
                        int awayTeamIndex = (Clubs.Count - 1 - j + i) % (Clubs.Count - 1);

                        int homeTeamId = Clubs[homeTeamIndex].IdClub;
                        int awayTeamId = Clubs[awayTeamIndex].IdClub;

                        PlayedMatches.Add(new MatchTracking(25, new PlayedMatch(homeTeamId, awayTeamId, DateTime.Now)));
                    }

                }
                actualRound++;
            }
            else
            {
                for (int i = 0; i < RoundCount; i++)
                {
                    for (int j = 0; j < matchPerRound; j++)
                    {
                        int homeTeamIndex = (i + j) % (Clubs.Count - 1);
                        int awayTeamIndex = (Clubs.Count - 1 - j + i) % (Clubs.Count - 1);

                        int homeTeamId = Clubs[homeTeamIndex].IdClub;
                        int awayTeamId = Clubs[awayTeamIndex].IdClub;

                        PlayedMatches.Add(new MatchTracking(25, new PlayedMatch(awayTeamId, homeTeamId, DateTime.Now)));
                    }

                }
                actualRound++;
            }
        }


    }
}
