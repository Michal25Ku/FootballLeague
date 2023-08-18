using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonAllMatchesGenerator : IGeneratorAndSorterMatches
    {
        public IList<Match> GenerateMatches(IList<Club> clubList)
        {
            using var db = new FootballLeagueContext();

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
                        db.Matches.Add(new Match
                        {
                            HomeTeamName = db.Clubs.FirstOrDefault(c => c.IdClub == homeTeamId).ClubName,
                            AwayTeamName = db.Clubs.FirstOrDefault(c => c.IdClub == awayTeamId).ClubName,
                            MatchName = db.Clubs.FirstOrDefault(c => c.IdClub == homeTeamId).ClubName + " - " + db.Clubs.FirstOrDefault(c => c.IdClub == awayTeamId).ClubName,
                            HomeTeamId = homeTeamId,
                            HomeTeam = db.Clubs.FirstOrDefault(c => c.IdClub == homeTeamId),
                            AwayTeamId = awayTeamId,
                            AwayTeam = db.Clubs.FirstOrDefault(c => c.IdClub == awayTeamId),
                        });
                        db.SaveChanges();
                    }
                }
            }

            db.SaveChanges();
            return db.Matches.ToList();
        }

        // To do
        public Dictionary<int, IList<Match>> SortMatchesIntoRound(IList<Match> allMatchesList)
        {
            using var db = new FootballLeagueContext();
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

                    if (clubs.Any(c => c.IdClub == match.HomeTeamId) && clubs.Any(c => c.IdClub == match.AwayTeamId))
                    {
                        clubs.RemoveAll(c => c.IdClub == match.HomeTeamId || c.IdClub == match.AwayTeamId);

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
