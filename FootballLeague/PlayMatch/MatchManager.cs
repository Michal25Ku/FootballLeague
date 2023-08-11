using FootballLeagueLib.Model;
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

        public Match PlayedMatch { get; }
        private readonly MatchScoredGoal MatchScoredGoal;
        private readonly IEndMatch<Match> EndMatch;
        private readonly IGetPlayers MatchPlayers;

        public List<Player> PlayersHomeTeam { get; private set; }
        public List<Player> PlayersAwayTeam { get; private set; }

        public MatchManager(Match playedMatch)
        {
            PlayedMatch = playedMatch;
            TimeInMatch = 0;
            PlayersHomeTeam = MatchPlayers.HomeTeamPlayers(playedMatch.IdHomeTeam);
            PlayersAwayTeam = MatchPlayers.AwayTeamPlayers(playedMatch.IdAwayTeam);
            MatchScoredGoal = new MatchScoredGoal(this);
        }

        public void StartMatch()
        {
            var rand = new Random();

            for (int i = 1; i <= MATCH_TIME; i++)
            {
                //Console.WriteLine($"Minuta:{i}");

                if (rand.Next(100) <= 1)
                {
                    int teamShoot = rand.Next(2);
                    if (teamShoot == 0)
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        MatchScoredGoal.ScoreGoal(i, PlayedMatch.IdHomeTeam, PlayersHomeTeam[playerShoot].IdPlayer);
                        //Console.WriteLine($"Drużyna {PlayedMatch.HomeTeam} strzeliła gola");
                    }
                    else
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        MatchScoredGoal.ScoreGoal(i, PlayedMatch.IdAwayTeam, PlayersAwayTeam[playerShoot].IdPlayer);
                        //Console.WriteLine($"Drużyna {PlayedMatch.AwayTeam} strzeliła gola");
                    }
                }

                //Thread.Sleep(5000 / SimulationSpeedMultiplier);
            }

            EndMatch.UpdateAfterMatchIsOver(PlayedMatch);
        }
    }
}
