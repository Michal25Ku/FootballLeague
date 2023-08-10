using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonManager
    {
        public static int actualRound = 1;
        Dictionary<int, List<Model.Match>> Rounds { get; set; }

        public SeasonManager()
        {
            //Rounds = new Dictionary<int, List<Model.Match>>;
        }
    }
}
