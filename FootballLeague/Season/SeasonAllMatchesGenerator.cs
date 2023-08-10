using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonAllMatchesGenerator : Interfaces.IGeneratorAndSorterMatches
    {
        public IList<Match> GenerateMatches(IList<Club> clubList)
        {
            using var db = new FootballLeague();

            int clubCount = db.Clubs.Count();
            int RoundCount = (clubCount - 1) * 2;

            List<Model.Match> matches = new List<Model.Match>();

            for (int clubHome = 0; clubHome < clubCount; clubHome++)
            {
                for (int clubAway = 0; clubAway < clubCount; clubAway++)
                {
                    int homeTeamId = clubList[clubHome].IdClub;
                    int awayTeamId = clubList[clubAway].IdClub;

                    if (awayTeamId != homeTeamId)
                    {
                        matches.Add(new Model.Match(homeTeamId, awayTeamId, DateTime.Now));
                    }
                }
            }

            return matches;
        }

        public Dictionary<int, IList<Match>> SortMatchesIntoRound(IList<Match> AllMatchesList)
        {
            Dictionary<int, IList<Match>> matchesIntoRounds = new Dictionary<int, IList<Match>>();
            List<Club> clubs = new List<Club>();
            int round = 1;

            while (AllMatchesList.Count <= 0)
            {
                List<Match> matchesIntoRound = new List<Match>();

                foreach (Match match in AllMatchesList)
                {
                    if (!(clubs.Contains(match.ClubHost) || clubs.Contains(match.ClubGuest)))
                    {
                        clubs.Add(match.ClubHost);
                        clubs.Add(match.ClubGuest);
                        matchesIntoRound.Add(match);
                        AllMatchesList.Remove(match);
                    }
                }

                if (!matchesIntoRounds.ContainsKey(round))
                    matchesIntoRounds.Add(round, matchesIntoRound);

                clubs.Clear();
                round++;
            }

            return matchesIntoRounds;
        }
    }
}
