using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchScoredGoal : IScoreGoal
    {
        public bool ScoreGoal(int minuteOfMatch, int idClub, int idPlayer, MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();
            #region Is the goal scored correctly and all conditions are fulfilled
            // Goal can be scored between 1 and 90 minutes
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                return false;
            if (idClub != matchManager.PlayedMatch.HomeTeamId && idClub != matchManager.PlayedMatch.AwayTeamId)
                return false;

            // Is player playing in this match
            if (!matchManager.HomeTeamPlayers.Select(p => p.IdPlayer).Any() && !matchManager.AwayTeamPlayers.Select(p => p.IdPlayer).Any())
                return false;
            // Is player playing for the team that scored the goal
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != idClub)
                return false;
            #endregion

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                ClubId = idClub,
                PlayerId = idPlayer,
                MatchId = matchManager.PlayedMatch.IdMatch,
                Player = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer),
                Match = db.Matches.FirstOrDefault(m => m.IdMatch == matchManager.PlayedMatch.IdMatch)
            };

            db.Goals.Add(newGoal);
            db.SaveChanges();
            // when a player scores a goal, the match, player, and club data are updated
            bool pass = UpdateMatchAfterGoal(newGoal, matchManager.PlayedMatch.IdMatch);
            pass = UpdatePlayerCount(idPlayer);
            pass = UpdateClubGoals(idClub, matchManager);

            return pass;
        }

        public bool UpdateMatchAfterGoal(Goal goal, int matchId)
        {
            using var db = new FootballLeagueContext();

            var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == matchId);

            if (matchToUpdate is null)
                return false;

            if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == matchId).HomeTeamId)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == matchId).AwayTeamId)
            {
                matchToUpdate.GoalsAwayTeam += 1;
            }

            matchToUpdate.Result = matchToUpdate.GoalsHomeTeam + " - " + matchToUpdate.GoalsAwayTeam;

            return SaveChange(db);
        }

        public bool UpdatePlayerCount(int idPlayer)
        {
            using var db = new FootballLeagueContext();

            var playerToUpdate = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer);

            if (playerToUpdate is null)
                return false;

            playerToUpdate.GoalsScored += 1;

            return SaveChange(db);
        }

        public bool UpdateClubGoals(int idClub, MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();
            bool pass = false;

            if (idClub == matchManager.PlayedMatch.HomeTeamId)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == matchManager.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;
                pass = SaveChange(db);

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == matchManager.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
                pass = SaveChange(db);
            }
            else if (idClub == matchManager.PlayedMatch.AwayTeamId)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == matchManager.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;
                pass = SaveChange(db);

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == matchManager.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
                pass = SaveChange(db);
            }

            return pass;
        }

        bool SaveChange(FootballLeagueContext db)
        {
            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
