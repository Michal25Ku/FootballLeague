using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchEnd : IEndMatch<Match>
    {
        public bool UpdateAfterMatchIsOver(Match match)
        {
            using var db = new FootballLeagueContext();

            if (match.GoalsHomeTeam > match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.HomeTeamId);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.AwayTeamId);
                clubToUpdate.Failures += 1;
            }
            else if (match.GoalsHomeTeam == match.GoalsAwayTeam) // draw
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.HomeTeamId);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.AwayTeamId);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (match.GoalsHomeTeam < match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.HomeTeamId);
                clubToUpdate.Failures += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.AwayTeamId);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            db.Matches.FirstOrDefault(m => m.IdMatch == match.IdMatch).IsPlayed = true;

            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
