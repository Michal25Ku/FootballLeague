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
                return false;
            if (idClub != IdHomeTeam && idClub != IdAwayTeam)
                return false;

            using var db = new FootballLeague();

            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdHomeTeam && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdAwayTeam)
                return false;

            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != idClub)
                return false;

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                IdPlayer = idPlayer,
                IdClub = idClub,
                IdMatch = this.IdMatch
            };

            db.Goals.Add(newGoal);
            UpdateMatchAfterGoal(idClub);
            UpdatePlayerCount(idPlayer);
            UpdateClubGoals(idClub);
            return SaveChange(db);
        }

        bool UpdateMatchAfterGoal(int idClub)
        {
            using var db = new FootballLeague();

            var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == this.IdMatch);

            if(matchToUpdate is null)
                return false;

            if(idClub == db.Matches.FirstOrDefault(m => m.IdMatch == this.IdMatch).IdHomeTeam)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (idClub == db.Matches.FirstOrDefault(m => m.IdMatch == this.IdMatch).IdAwayTeam)
            {
                matchToUpdate.GoalsAwayTeam += 1;
            }

            matchToUpdate.Result = matchToUpdate.GoalsHomeTeam + " - " + matchToUpdate.GoalsAwayTeam;

            return SaveChange(db);
        }

        bool UpdatePlayerCount(int idPlayer)
        {
            using var db = new FootballLeague();

            var playerToUpdate = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer);

            if (playerToUpdate is null)
                return false;

            playerToUpdate.GoalsScored += 1;

            return SaveChange(db);
        }

        bool UpdateClubGoals(int idClub) 
        {
            using var db = new FootballLeague();

            if (idClub == IdHomeTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.GoalsScored -= 1;
                clubToUpdate.GoalBalance -= 1;
            }
            else if(idClub == IdAwayTeam) 
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.GoalsScored -= 1;
                clubToUpdate.GoalBalance -= 1;
            }

            return SaveChange(db);
        }

        public bool UpdateAfterMatch()
        {
            using var db = new FootballLeague();

            var match = db.Matches.FirstOrDefault(c => c.IdMatch == this.IdMatch);

            if (match.GoalsHomeTeam > match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Failures += 1;
            }
            else if (match.GoalsHomeTeam == match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (match.GoalsHomeTeam < match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Failures += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            return SaveChange(db);
        }

        static bool SaveChange(FootballLeague db)
        {
            int result = db.SaveChanges();
            return (result == 1);
        }
    }
}
