using FootballLeagueLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public static class MatchScoredGoal
    {
        public static async Task<bool> ScoreGoal(int minuteOfMatch, int idClub, int idPlayer, MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();
            #region Is the goal scored correctly and all conditions are fulfilled
            // Goal can be scored between 1 and 90 minutes
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                return false;
            if (idClub != matchManager.PlayedMatch.HomeTeamId && idClub != matchManager.PlayedMatch.AwayTeamId)
                return false;

            // Is player playing in this match
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != matchManager.PlayedMatch.HomeTeamId && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).ClubId != matchManager.PlayedMatch.AwayTeamId)
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

            // when a player scores a goal, the match, player, and club data are updated
            await Task.Run(() => UpdateMatchAfterGoal(newGoal, matchManager));
            await Task.Run(() => UpdatePlayerCount(idPlayer));
            await Task.Run(() => UpdateClubGoals(idClub, matchManager));

            return await Task.Run(() => SaveChange(db));
        }

        static async Task<bool> UpdateMatchAfterGoal(Goal goal, MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();

            var matchToUpdate = await db.Matches.FirstOrDefaultAsync(m => m.IdMatch == matchManager.PlayedMatch.IdMatch);

            if (matchToUpdate is null)
                return false;

            if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == matchManager.PlayedMatch.IdMatch).HomeTeamId)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (goal.ClubId == db.Matches.FirstOrDefault(m => m.IdMatch == matchManager.PlayedMatch.IdMatch).AwayTeamId)
            {
                matchToUpdate.GoalsAwayTeam += 1;
            }

            matchToUpdate.Result = matchToUpdate.GoalsHomeTeam + " - " + matchToUpdate.GoalsAwayTeam;

            return await SaveChange(db);
        }

        static async Task<bool> UpdatePlayerCount(int idPlayer)
        {
            using var db = new FootballLeagueContext();

            var playerToUpdate = await db.Players.FirstOrDefaultAsync(p => p.IdPlayer == idPlayer);

            if (playerToUpdate is null)
                return false;

            playerToUpdate.GoalsScored += 1;

            return await SaveChange(db);
        }

        static async Task<bool> UpdateClubGoals(int idClub, MatchManager matchManager)
        {
            using var db = new FootballLeagueContext();

            if (idClub == matchManager.PlayedMatch.HomeTeamId)
            {
                var clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == matchManager.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == matchManager.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }
            else if (idClub == matchManager.PlayedMatch.AwayTeamId)
            {
                var clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == matchManager.PlayedMatch.AwayTeamId);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = await db.Clubs.FirstOrDefaultAsync(c => c.IdClub == matchManager.PlayedMatch.HomeTeamId);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }

            return await SaveChange(db);
        }

        static async Task<bool> SaveChange(FootballLeagueContext db)
        {
            int result = await db.SaveChangesAsync();
            return result == 1;
        }
    }
}
