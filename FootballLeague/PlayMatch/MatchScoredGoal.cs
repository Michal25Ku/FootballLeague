using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchScoredGoal
    {
        private readonly MatchManager _matchManage;

        public MatchScoredGoal(MatchManager matchManager)
        {
            _matchManage = matchManager;
        }

        public bool ScoreGoal(int minuteOfMatch, int idClub, int idPlayer)
        {
            // Goal can be scored between 1 and 90 minutes
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                return false;
            if (idClub != _matchManage.PlayedMatch.HomeTeamId && idClub != _matchManage.PlayedMatch.AwayTeamId)
                return false;

            using var db = new FootballLeagueContext();

            // Is player playing in this match
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != _matchManage.PlayedMatch.HomeTeamId && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != _matchManage.PlayedMatch.AwayTeamId)
                return false;

            // Is player playing for the team that scored the goal
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != idClub)
                return false;

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                ClubId = idClub,
                PlayerId = idPlayer,
                MatchId = _matchManage.PlayedMatch.IdMatch,
                Player = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer),
                Match = db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch)
            };

            db.Goals.Add(newGoal);

            // when a player scores a goal, the match, player, and club data are updated
            UpdateMatchAfterGoal(newGoal);
            UpdatePlayerCount(idPlayer);
            UpdateClubGoals(idClub);

            return SaveChange(db);
        }

        bool UpdateMatchAfterGoal(Goal goal)
        {
            using var db = new FootballLeagueContext();

            var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch);

            if (matchToUpdate is null)
                return false;

            if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch).HomeTeamId)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == _matchManage.PlayedMatch.IdMatch).AwayTeamId)
            {
                matchToUpdate.GoalsAwayTeam += 1;
            }

            matchToUpdate.Result = matchToUpdate.GoalsHomeTeam + " - " + matchToUpdate.GoalsAwayTeam;

            return SaveChange(db);
        }

        bool UpdatePlayerCount(int idPlayer)
        {
            using var db = new FootballLeagueContext();

            var playerToUpdate = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer);

            if (playerToUpdate is null)
                return false;

            playerToUpdate.GoalsScored += 1;

            return SaveChange(db);
        }

        bool UpdateClubGoals(int idClub)
        {
            using var db = new FootballLeagueContext();

            if (idClub == _matchManage.PlayedMatch.HomeTeamId)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }
            else if (idClub == _matchManage.PlayedMatch.AwayTeamId)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == _matchManage.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }

            return SaveChange(db);
        }

        static bool SaveChange(FootballLeagueContext db)
        {
            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
