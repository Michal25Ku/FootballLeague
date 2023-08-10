using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IGeneratorAndSorterMatches
    {
        IList<Model.Match> GenerateMatches(IList<Model.Club> clubList);
        Dictionary<int, IList<Model.Match>> SortMatchesIntoRound(IList<Model.Match> AllMatchesList);
    }

}
