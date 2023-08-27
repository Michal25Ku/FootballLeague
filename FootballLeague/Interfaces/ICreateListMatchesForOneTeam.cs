using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface ICreateListMatchesForOneTeam
    {
        List<Match> CreateMatchesList(List<Match> matches, int clubId);
    }
}
