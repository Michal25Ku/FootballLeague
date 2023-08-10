using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IEndMatch<Match>
    {
        bool UpdateAfterMatchIsOver(Match match);
    }
}
