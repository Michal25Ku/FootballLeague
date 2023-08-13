using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface ICreateUpdateTable
    {
        List<Tuple<int, Club, int>> CreateTable(IList<Club> clubs);

        List<Tuple<int, Club, int>> UpdateTable();
    }
}
