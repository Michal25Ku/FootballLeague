using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class MatchTracking
    {
        public const int MATCH_TIME = 90;
        public int SimulationSpeedMultiplier { get; set; }
        public PlayedMatch Match { get; }
        public int TimeInMatch { get; private set; }
        
        public MatchTracking(int simulationSpeedMultiplier, PlayedMatch match)
        {
            SimulationSpeedMultiplier = simulationSpeedMultiplier;
            Match = match;
            TimeInMatch = 0;
        }

        public void StartMatch()
        {
            var rand = new Random();

            for(int i = 1; i <= MATCH_TIME; i++)
            {
                Console.WriteLine($"Minuta:{i}");

                if(rand.Next(100) <= 5)
                {
                    int teamShoot = rand.Next(2);
                    if(teamShoot == 0)
                    {
                        int playerShoot = rand.Next();
                        Match.ShootGoal(i, Match.IdHomeTeam, 1);
                        Console.WriteLine($"Drużyna {Match.HomeTeamName} strzeliła gola");
                    }
                    else
                    {
                        Match.ShootGoal(i, Match.IdAwayTeam, 2);
                        Console.WriteLine($"Drużyna {Match.AwayTeamName} strzeliła gola");

                    }
                }

                Thread.Sleep(5000 / SimulationSpeedMultiplier);
            }

            Match.UpdateAfterMatch();
        }
    }
}
