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
        List<(int, Club, int)> CreateTable(IList<Club> clubs);

        void UpdateTable (ref IList<Club> clubs);
    }
}
