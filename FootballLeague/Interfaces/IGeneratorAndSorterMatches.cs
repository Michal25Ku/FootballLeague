using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballLeagueLib.Entities;

namespace FootballLeagueLib.Interfaces
{
    public interface IGeneratorAndSorterMatches
    {
        IList<Match> GenerateMatches(IList<Club> clubList);
        Dictionary<int, IList<Match>> SortMatchesIntoRound(IList<Match> AllMatchesList);
    }

}
