using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchScoredGoal
    {
        private MatchManager _matchManage;

        public MatchScoredGoal(MatchManager matchManager)
        {
            _matchManage = matchManager;
        }

        public bool ScoreGoal(int minuteOfMatch, int idClub, int idPlayer)
        {
            // Goal can be scored between 1 and 90 minutes
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                return false;
            if (idClub != _matchManage.PlayedMatch.IdHomeTeam && idClub != _matchManage.PlayedMatch.IdAwayTeam)
                return false;

            using var db = new FootballLeague();

            // Is player playing in this match
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != _matchManage.PlayedMatch.IdHomeTeam && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != _matchManage.PlayedMatch.IdAwayTeam)
                return false;

            // Is player playing for the team that scored the goal
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != idClub)
                return false;

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                IdPlayer = idPlayer,
                IdClub = idClub,
                IdMatch = _matchManage.PlayedMatch.IdMatch
            };

            db.Goals.Add(newGoal);

            // when a player scores a goal, the match, player and club data are updated
            UpdateMatchAfterGoal(idClub);
            UpdatePlayerCount(idPlayer);
            UpdateClubGoals(idClub);

            return SaveChange(db);
        }

        bool UpdateMatchAfterGoal(int idClub)
        {
            using var db = new FootballLeague();

            var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch);

            if (matchToUpdate is null)
                return false;

            if (idClub == db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch).IdHomeTeam)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (idClub == db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch).IdAwayTeam)
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

            if (idClub == _matchManage.PlayedMatch.IdHomeTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.IdHomeTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.IdAwayTeam);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }
            else if (idClub == _matchManage.PlayedMatch.IdAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.IdAwayTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.IdHomeTeam);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }

            return SaveChange(db);
        }

        static bool SaveChange(FootballLeague db)
        {
            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
