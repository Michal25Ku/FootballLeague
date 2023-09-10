using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FootballLeagueLib.Interfaces;
using System.Text.RegularExpressions;
using Match = FootballLeagueLib.Entities.Match;
using FootballLeagueLib.Table;
using FootballLeagueLib.Season;

namespace FootballLeagueLib.PlayMatch
{
    public delegate void MatchResultChangedHandler();
    public delegate void MatchTimeChangedHandler();
    public delegate void MatchEndChangedHandler();
    public delegate void MatchStartChangedHandler();
    public class MatchManager
    {
        #region events
        public event MatchResultChangedHandler MatchResultChanged;
        public event MatchTimeChangedHandler MatchTimeChanged;
        public event MatchEndChangedHandler MatchEndChanged;
        public event MatchStartChangedHandler MatchStartChanged;
        #endregion

        public const int MATCH_TIME = 90;
        public int TimeInMatch { get; private set; }
        public Match PlayedMatch { get; private set; }

        // TODO 
        // need to create separate maybe a progress bar in the menu. we could change this value if we want the match to be played faster or slower
        public int SimulationTime { get; }

        private readonly MatchScoredGoal ScoredGoalManager;
        private readonly MatchEnd EndingMatch;

        public List<Player> HomeTeamPlayers { get; private set; }
        public List<Player> AwayTeamPlayers { get; private set; }

        public MatchManager(Match playedMatch)
        {
            ScoredGoalManager = new MatchScoredGoal(this);
            EndingMatch = new MatchEnd();

            PlayedMatch = playedMatch;

            HomeTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.HomeTeamId);
            AwayTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.AwayTeamId);

            SimulationTime = 0;
            TimeInMatch = 0;
        }

        public async Task StartMatch()
        {
            MatchStartChanged?.Invoke();
            var rand = new Random();
            using var db = new FootballLeagueContext();

            db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch).IsPlayed = true;
            db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch).Result = PlayedMatch.GoalsHomeTeam + " - " + PlayedMatch.GoalsAwayTeam;

            for (int i = 1; i <= MATCH_TIME + 1; i++)
            {
                MatchTimeChanged?.Invoke();
                if (rand.Next(100) <= 1)
                {
                    int teamShoot = rand.Next(2);
                    if (teamShoot == 0)
                    {
                        int playerShoot = rand.Next(HomeTeamPlayers.Count);
                        await ScoredGoalManager.ScoreGoal(i, PlayedMatch.HomeTeamId, HomeTeamPlayers[playerShoot].IdPlayer);
                        MatchResultChanged?.Invoke();
                    }
                    else
                    {
                        int playerShoot = rand.Next(AwayTeamPlayers.Count);
                        await ScoredGoalManager.ScoreGoal(i, PlayedMatch.AwayTeamId, AwayTeamPlayers[playerShoot].IdPlayer);
                        MatchResultChanged?.Invoke();
                    }
                }

                TimeInMatch = i;
                db.SaveChanges();
                PlayedMatch = db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch);

                // 10 seconds
                await Task.Run(() => Thread.Sleep(5000 / 100));
            }

            EndingMatch.UpdateAfterMatchIsOver(PlayedMatch);
            MatchEndChanged?.Invoke();
        }
    }
}
