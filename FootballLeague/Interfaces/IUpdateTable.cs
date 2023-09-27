using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IUpdateTable
    {
        /// <summary>
        /// Creates a new List<Tuple<int, Club, int>> which includes: Item1 - rank club in the table, Item2 - club, Item3 - number of matches played by the club.
        /// Sorts this list descending by points, then by goal balance, then by goals scored.
        /// </summary>
        /// <returns>Tuple list where item1 - rank, item2 - club, item2 - number of played matches</returns>
        List<Tuple<int, Club, int>> UpdateTable();
    }
}
