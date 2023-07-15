using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class PlayedMatch
    {
        public int IdMatch { get; }
        public int IdHomeTeam { get; }
        public int IdAwayTeam { get; }

        public PlayedMatch(int idHomeTeam, int idAwayTeam, DateTime matchDate = default)
        {
            using var db = new FootballLeague();

            if (matchDate == default)
                matchDate = DateTime.Now;

            foreach (var c in db.Clubs.Select(i => i.IdClub))
            {
                if (idHomeTeam != c || idAwayTeam != c)
                    throw new ArgumentException("Podana drużyna nie istnieje!");
            }

            IdHomeTeam = idHomeTeam;
            IdAwayTeam = idAwayTeam;
            PlayMatch(idHomeTeam, idAwayTeam, matchDate);
        }

        public static bool PlayMatch(int idHomeTeam, int idAwayTeam, DateTime matchDate)
        {
            using var db = new FootballLeague();

            var newMatch = new Match
            {
                MatchDate = matchDate,
                IdHomeTeam = idHomeTeam,
                IdAwayTeam = idAwayTeam,
            };

            return SaveChange(db);
        }

        static bool SaveChange(FootballLeague db)
        {
            int result = db.SaveChanges();
            return (result == 1);
        }
    }
}
