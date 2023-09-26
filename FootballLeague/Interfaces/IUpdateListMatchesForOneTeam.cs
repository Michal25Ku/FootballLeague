using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IUpdateListMatchesForOneTeam
    {
        /// <summary>
        /// Takes Match list from database which doesn't include matches where HomeTeam or AwayTeam is a club specified in the parameter 
        /// </summary>
        /// <param name="club">club which doesn't include in a match list</param>
        /// <returns>Match list</returns>
        List<Match> UpdateMatchesForOneClub(Club club);
    }
}
