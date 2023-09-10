using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public static class MatchEnd
    {
        public static async Task<bool> UpdateAfterMatchIsOver(MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();
            var match = matchManager.PlayedMatch;

            Match playedMatch = db.Matches.FirstOrDefault(m => m.IdMatch == match.IdMatch);

            if (playedMatch.GoalsHomeTeam > playedMatch.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Failures += 1;
            }
            else if (playedMatch.GoalsHomeTeam == playedMatch.GoalsAwayTeam) // draw
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (playedMatch.GoalsHomeTeam < playedMatch.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Failures += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            db.Matches.FirstOrDefault(m => m.IdMatch == playedMatch.IdMatch).IsPlayed = true;

            int result = await Task.Run(() => db.SaveChanges());
            return result == 1;
        }
    }
}
