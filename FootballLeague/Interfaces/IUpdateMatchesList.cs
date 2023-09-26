using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IUpdateMatchesList
    {
        /// <summary>
        /// Takes Match list from database and sorts by round
        /// </summary>
        /// <returns>Match List</returns>
        List<Match> UpdateMatchesList();
    }
}
