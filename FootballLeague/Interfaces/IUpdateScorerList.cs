using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IUpdateScorerList
    {
        /// <summary>
        /// Checks which team scored a goal and added data about minute and player's first name and last name who scored a goal to scorer list as StringBuilder
        /// </summary>
        /// <param name="minute">minute of match when goal is scoed</param>
        /// <param name="player">player who scored goal</param>
        /// <param name="isHomeTeamShotGoal">if true, home team scor goal, false away team score goal</param>
        void UpdateScorerInMatch(int minute, Player player, bool isHomeTeamShotGoal);
    }
}
