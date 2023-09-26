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
        /// Create a Tuples list which includes a minute of the match as Item1 and the player who scored the goal as Item2
        /// </summary>
        /// <param name="match">match where goals are scored</param>
        /// <param name="idClub">the club who scored goal</param>
        /// <returns>return Tuple list where item1 - minute of match, item2 - Player</returns>
        List<Tuple<int, Player>> CreateScorerList(Match match, int idClub);
    }
}
