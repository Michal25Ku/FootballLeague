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
            for(int i = 1; i <= MATCH_TIME; i++)
            {
                Console.WriteLine($"Minuta:{i}");

                Thread.Sleep(5000 / SimulationSpeedMultiplier);
            }

            Match.UpdateAfterMatch();
        }
    }
}
