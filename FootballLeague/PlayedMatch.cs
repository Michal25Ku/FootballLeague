using FootballLeagueLib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class PlayedMatch
    {
        public int IdMatch { get; private set; }
        public int IdHomeTeam { get; }
        public int IdAwayTeam { get; }

        public string HomeTeamName { get; }
        public string AwayTeamName { get; }

        public PlayedMatch(int idHomeTeam, int idAwayTeam, DateTime matchDate = default)
        {
            using var db = new FootballLeague();

            if (matchDate == default)
                matchDate = DateTime.Now;

            if (idHomeTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idHomeTeam).IdClub || idAwayTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idAwayTeam).IdClub)
                throw new ArgumentException("Podana drużyna nie istnieje!");

            HomeTeamName = db.Clubs.Where(c => c.IdClub == idHomeTeam).Select(c => c.ClubName).FirstOrDefault();
            AwayTeamName = db.Clubs.Where(c => c.IdClub == idAwayTeam).Select(c => c.ClubName).FirstOrDefault();
            IdHomeTeam = idHomeTeam;
            IdAwayTeam = idAwayTeam;
            PlayMatch(idHomeTeam, idAwayTeam, matchDate);
            IdMatch = db.Matches.OrderBy(m => m.IdMatch).Select(m => m.IdMatch).FirstOrDefault();
        }

        private bool PlayMatch(int idHomeTeam, int idAwayTeam, DateTime matchDate)
        {
            using var db = new FootballLeague();

            var newMatch = new Match
            {
                HomeTeam = HomeTeamName,
                AwayTeam = AwayTeamName,
                MatchName = HomeTeamName + " - " + AwayTeamName,

                MatchDate = matchDate,
                IdHomeTeam = idHomeTeam,
                IdAwayTeam = idAwayTeam
            };

            db.Matches.Add(newMatch);
            return SaveChange(db);
        }

        public bool ShootGoal(int minuteOfMatch, int idClub, int idPlayer)
        {
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                throw new ArgumentOutOfRangeException("Podano nieprawidłową minutę meczu!");
            if (idClub != IdHomeTeam && idClub != IdAwayTeam)
                throw new ArgumentException("Podana drużyna nie gra w tym meczu!");

            using var db = new FootballLeague();

            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdHomeTeam && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdAwayTeam)
                throw new ArgumentException("Podany zawodnik nie gra w tym meczu!");

            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != idClub)
                throw new ArgumentException("Podany zawodnik nie gra dla tego klubu!");

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                IdPlayer = idPlayer,
                IdClub = idClub,
                IdMatch = this.IdMatch
            };

            db.Goals.Add(newGoal);
            return SaveChange(db);
        }

        static bool SaveChange(FootballLeague db)
        {
            int result = db.SaveChanges();
            return (result == 1);
        }
    }
}
