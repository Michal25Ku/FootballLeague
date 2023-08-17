using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FootballLeagueLib.Interfaces;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchManager
    {
        public const int MATCH_TIME = 90;

        public int TimeInMatch { get; private set; }

        public Match PlayedMatch { get; private set; }

        private readonly IGetPlayers MatchPlayers;
        private readonly MatchScoredGoal MatchScoredGoal;
        private readonly IEndMatch<Match> EndMatch;

        public List<Player> PlayersHomeTeam { get; private set; }
        public List<Player> PlayersAwayTeam { get; private set; }

        public MatchManager(Match playedMatch)
        {
            MatchPlayers = new MatchPlayers();
            MatchScoredGoal = new MatchScoredGoal(this);
            EndMatch = new MatchEnd();

            PlayedMatch = playedMatch;

            PlayersHomeTeam = MatchPlayers.HomeTeamPlayers(playedMatch.HomeTeamId);
            PlayersAwayTeam = MatchPlayers.AwayTeamPlayers(playedMatch.AwayTeamId);
            
            TimeInMatch = 0;
        }

        public void StartMatch()
        {
            var rand = new Random();

            for (int i = 1; i <= MATCH_TIME; i++)
            {
                if (rand.Next(100) <= 1)
                {
                    int teamShoot = rand.Next(2);
                    if (teamShoot == 0)
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        MatchScoredGoal.ScoreGoal(i, PlayedMatch.HomeTeamId, PlayersHomeTeam[playerShoot].IdPlayer);
                    }
                    else
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        MatchScoredGoal.ScoreGoal(i, PlayedMatch.AwayTeamId, PlayersAwayTeam[playerShoot].IdPlayer);
                    }
                }

                using var db = new FootballLeagueContext();
                PlayedMatch = db.Matches.FirstOrDefault(m => m.IdMatch == PlayedMatch.IdMatch);

                //Thread.Sleep(5000 / SimulationSpeedMultiplier);
            }

            EndMatch.UpdateAfterMatchIsOver(PlayedMatch);
        }
    }
}
