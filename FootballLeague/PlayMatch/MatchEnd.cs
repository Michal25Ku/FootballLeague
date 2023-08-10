using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchEnd : Interfaces.IEndMatch<Model.Match>
    {
        public bool UpdateAfterMatchIsOver(Match match)
        {
            using var db = new FootballLeague();

            if (match.GoalsHomeTeam > match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdHomeTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdAwayTeam);
                clubToUpdate.Failures += 1;
            }
            else if (match.GoalsHomeTeam == match.GoalsAwayTeam) // draw
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdHomeTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdAwayTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (match.GoalsHomeTeam < match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdHomeTeam);
                clubToUpdate.Failures += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == match.IdAwayTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            match.IsPlayed = true;

            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
