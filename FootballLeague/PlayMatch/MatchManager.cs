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
    public delegate void MatchResultChangedHandler(int minuteOfMatch, Player player, bool isHomeTeamShotGoal);
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
        List<int> _playerWeightNumbers;
        // TODO 
        // need to create separate maybe a progress bar in the menu. we could change this value if we want the match to be played faster or slower
        public int SimulationTime { get; }
        public int MinuteInMatch { get; private set; }

        public Match PlayedMatch { get; private set; }
        public MatchScoredGoal MatchScoreGoal { get; }
        public MatchPlayers MatchPlayers { get; }

        public List<Player> HomeTeamPlayers { get; private set; }
        public List<Player> AwayTeamPlayers { get; private set; }

        public MatchManager(Match playedMatch)
        {
            MatchScoreGoal = new MatchScoredGoal();
            MatchPlayers = new MatchPlayers();
            PlayedMatch = playedMatch;

            HomeTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.HomeTeamId);
            AwayTeamPlayers = MatchPlayers.GetPlayersFromTeam(playedMatch.AwayTeamId);
            _playerWeightNumbers = GenerateWeightedNumbers();

            SimulationTime = 0;
            MinuteInMatch = 0;
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
                        int playerShoot = _playerWeightNumbers[rand.Next(_playerWeightNumbers.Count - 1)];
                        MatchScoreGoal.ScoreGoal(i, PlayedMatch.HomeTeamId, HomeTeamPlayers[playerShoot - 1].IdPlayer, this);
                        MatchResultChanged?.Invoke(i, HomeTeamPlayers[playerShoot - 1], true);
                    }
                    else
                    {
                        int playerShoot = _playerWeightNumbers[rand.Next(_playerWeightNumbers.Count - 1)];
                        MatchScoreGoal.ScoreGoal(i, PlayedMatch.AwayTeamId, AwayTeamPlayers[playerShoot - 1].IdPlayer, this);
                        MatchResultChanged?.Invoke(i, AwayTeamPlayers[playerShoot - 1], false);
                    }
                }

                MinuteInMatch = i;
                db.SaveChanges();
                
                await Task.Run(() => Thread.Sleep(5000 / 100));
            }

            await MatchEnd.UpdateAfterMatchIsOver(this);
            MatchEndChanged?.Invoke();
        }

        List<int> GenerateWeightedNumbers()
        {
            List<int> weightedNumbers = new List<int>();

            for (int i = 1; i <= 11; i++)
            {
                int weight = i;
                for (int j = 0; j < weight / 2; j++)
                {
                    weightedNumbers.Add(i);
                }
            }
            return weightedNumbers;
        }
    }
}
