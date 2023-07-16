using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueLib
{
    public class MatchTracking
    {
        public const int MATCH_TIME = 90;
        public int SimulationSpeedMultiplier { get; set; }
        public PlayedMatch Match { get; }
        public int TimeInMatch { get; private set; }

        public List<Player> PlayersHomeTeam { get; private set; }
        public List<Player> PlayersAwayTeam { get; private set; }
        
        public MatchTracking(int simulationSpeedMultiplier, PlayedMatch match)
        {
            SimulationSpeedMultiplier = simulationSpeedMultiplier;
            Match = match;
            TimeInMatch = 0;
            PlayersHomeTeam = new List<Player>();
            PlayersAwayTeam = new List<Player>();
        }

        public void StartMatch()
        {
            var rand = new Random();
            PlayersHomeTeam = PlayersWhoPlay(Match.IdHomeTeam);
            PlayersAwayTeam = PlayersWhoPlay(Match.IdAwayTeam);

            for (int i = 1; i <= MATCH_TIME; i++)
            {
                Console.WriteLine($"Minuta:{i}");

                if(rand.Next(100) <= 5)
                {
                    int teamShoot = rand.Next(2);
                    if(teamShoot == 0)
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        Match.ShootGoal(i, Match.IdHomeTeam, playerShoot);
                        Console.WriteLine($"Drużyna {Match.HomeTeamName} strzeliła gola");
                    }
                    else
                    {
                        int playerShoot = rand.Next(PlayersHomeTeam.Count);
                        Match.ShootGoal(i, Match.IdAwayTeam, playerShoot);
                        Console.WriteLine($"Drużyna {Match.AwayTeamName} strzeliła gola");
                    }
                }

                Thread.Sleep(5000 / SimulationSpeedMultiplier);
            }

            Match.UpdateAfterMatch();
        }

        List<Player> PlayersWhoPlay(int idClub)
        {
            Random rand = new Random();
            using var db = new FootballLeague();
            List<Player> players = db.Players.Where(p => p.IdClub == idClub).ToList();

            while(players.Count > 11)
            {
                if (!(players[rand.Next(players.Count)] is null))
                    players.Remove(players[rand.Next(players.Count)]);
            }

            return players;
        }
    }
}
