using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface ICreateAndUpdateScorerList
    {
        List<Tuple<int, Player>> CreateScorerList(Match match, int idClub);
    }
}
