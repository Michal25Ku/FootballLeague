using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Season;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public delegate void MatchEnded();
    public static class MatchEnd
    {
        public static event MatchEnded MatchIsEnd;
        public static event EndSeason EndSeasonIsSet;

        public static async Task<bool> UpdateAfterMatchIsOver(MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();
            var match = matchManager.PlayedMatch;

            Match playedMatch = await db.Matches.FirstOrDefaultAsync(m => m.IdMatch == match.IdMatch);

            if (playedMatch.GoalsHomeTeam > playedMatch.GoalsAwayTeam)
            {
                var clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Failures += 1;
            }
            else if (playedMatch.GoalsHomeTeam == playedMatch.GoalsAwayTeam) // draw
            {
                var clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (playedMatch.GoalsHomeTeam < playedMatch.GoalsAwayTeam)
            {
                var clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.HomeTeamId);

                clubToUpdate.Failures += 1;

                clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == playedMatch.AwayTeamId);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            db.Matches.FirstOrDefault(m => m.IdMatch == playedMatch.IdMatch).IsPlayed = true;

            int result = await Task.Run(() => db.SaveChanges());
            if (db.Matches.All(m => m.IsPlayed == true))
            {
                SeasonManager.IsSeasonEnd = true;
                EndSeasonIsSet.Invoke();
            }
            MatchIsEnd.Invoke();
            return result == 1;
        }
    }
}
