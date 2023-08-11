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

            for (int clubHome = 0; clubHome < clubCount; clubHome++)
            {
                for (int clubAway = 0; clubAway < clubCount; clubAway++)
                {
                    int homeTeamId = clubList[clubHome].IdClub;
                    int awayTeamId = clubList[clubAway].IdClub;

                    if (awayTeamId != homeTeamId)
                    {
                        db.Matches.Add(new Model.Match(homeTeamId, awayTeamId, DateTime.Now));
                    }
                }
            }

            db.SaveChanges();
            return db.Matches.ToList();
        }

        // To do
        public Dictionary<int, IList<Match>> SortMatchesIntoRound(IList<Match> allMatchesList)
        {
            using var db = new FootballLeague();
            int round = 1;
            Dictionary<int, IList<Match>> matchesIntoRounds = new Dictionary<int, IList<Match>>();

            while (allMatchesList.Count > 0)
            {
                List<Match> matchesIntoRound = new List<Match>();
                List<Club> clubs = db.Clubs.ToList();

                foreach (var match in allMatchesList.Reverse().ToList())
                {
                    if (clubs.Count <= 0)
                        break;

                    if (clubs.Any(c => c.IdClub == match.IdHomeTeam) && clubs.Any(c => c.IdClub == match.IdAwayTeam))
                    {
                        clubs.RemoveAll(c => c.IdClub == match.IdHomeTeam || c.IdClub == match.IdAwayTeam);

                        var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == match.IdMatch);
                        matchToUpdate.Round = round;
                        db.SaveChanges();

                        matchesIntoRound.Add(match);
                        allMatchesList.Remove(match);
                    }
                }

                if (!matchesIntoRounds.ContainsKey(round))
                    matchesIntoRounds.Add(round, matchesIntoRound);

                round++;
            }

            return matchesIntoRounds;
        }
    }
}
