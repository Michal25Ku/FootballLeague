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
        public int SimulationSpeedMultiplier { get; set; }

        public Match PlayedMatch { get; }
        private readonly MatchScoredGoal MatchScoredGoal;
        private readonly IEndMatch<Match> EndMatch;

        public List<Player> PlayersHomeTeam { get; private set; }
        public List<Player> PlayersAwayTeam { get; private set; }

        public MatchManager(int simulationSpeedMultiplier, Match playedMatch)
        {
            SimulationSpeedMultiplier = simulationSpeedMultiplier;
            PlayedMatch = playedMatch;
            TimeInMatch = 0;
            PlayersHomeTeam = PlayersWhoPlay(playedMatch.IdHomeTeam);
            PlayersAwayTeam = PlayersWhoPlay(playedMatch.IdAwayTeam);
            MatchScoredGoal = new MatchScoredGoal(this);
        }

        List<Player> PlayersWhoPlay(int idClub)
        {
            Random rand = new Random();
            using var db = new FootballLeague();
            List<Player> players = db.Players.Where(p => p.IdClub == idClub).ToList();

            while (players.Count > 11)
            {
                if (!(players[rand.Next(players.Count)] is null))
                    players.Remove(players[rand.Next(players.Count)]);
            }

            return players;
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
