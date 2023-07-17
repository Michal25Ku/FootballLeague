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
        public static int actualRound = 1;
        public int RoundCount => (Db.Clubs.Count() * (Db.Clubs.Count()-1)) / 2;
        FootballLeague Db { get; }
        public List<MatchTracking> PlayedMatches { get; private set; }
        public List<Club> Clubs { get; private set; }

        public Round()
        {
            Db = new FootballLeague();
            PlayedMatches = new List<MatchTracking>();
            Clubs = Db.Clubs.ToList();

            GenerateAllMatchesForSeason();
        }

        void GenerateAllMatchesForSeason()
        {
            for(int round = 0; round < RoundCount; round++)
            {
                int matchPerRound = Clubs.Count / 2;

                if (actualRound < RoundCount / 2)
                {
                    for (int match = 0; match < matchPerRound; match++)
                    {
                        int homeTeamIndex = (round + match) % (Clubs.Count);
                        int awayTeamIndex = (Clubs.Count - match - round - 1) % (Clubs.Count);

                        // 0 - 0,3; 1,2; 
                        // 1 - 1,2; 2,1
                        // 2 - 2,1; 3,0

                        int homeTeamId = Clubs[homeTeamIndex].IdClub;
                        int awayTeamId = Clubs[awayTeamIndex].IdClub;

                        PlayedMatches.Add(new MatchTracking(25, new PlayedMatch(homeTeamId, awayTeamId, DateTime.Now)));
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
}
