using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.PlayMatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class ScorerListForMatch : IUpdateScorerList
    {
        public StringBuilder ScorerListForHomeTeam { get; private set; }
        public StringBuilder ScorerListForAwayTeam { get; private set; }

        /// <summary>
        /// Initialize ScorerListForHomeTeam and ScorerListForAwayTeam parameters
        /// </summary>
        public ScorerListForMatch()
        {
            ScorerListForHomeTeam = new StringBuilder();
            ScorerListForAwayTeam = new StringBuilder();
        }

        public void UpdateScorerInMatch(int minute, Player player, bool isHomeTeamShotGoal)
        {
            if(isHomeTeamShotGoal)
            {
                ScorerListForHomeTeam.AppendLine($"{minute} {player.FirstName} {player.LastName}");
                ScorerListForAwayTeam.AppendLine();
            }
            else 
            {
                ScorerListForHomeTeam.AppendLine();
                ScorerListForAwayTeam.AppendLine($"{minute} {player.FirstName} {player.LastName}");
            }
        }
    }
}