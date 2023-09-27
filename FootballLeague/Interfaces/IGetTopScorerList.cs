using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IGetTopScorerList
    {
        /// <summary>
        /// Creates and returns 5 top scorers in the league as a tuple list where Item1 - rank player, Item2 - player, Item3 - the club where the player is played
        /// Tuple list sorted descending by goals scored by player
        /// </summary>
        /// <returns></returns>
        List<Tuple<int, Player, Club>> TopScorers();
    }
}
