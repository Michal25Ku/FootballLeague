using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonRules
    {
        public SeasonRules(int numberOfClubs, int numberOfRelagatedClubs) 
        {
            NumberOfClubs = numberOfClubs;
            NumberOfRelagatedClubs = numberOfRelagatedClubs;
        }

        public int NumberOfClubs { get; }
        public int NumberOfRelagatedClubs { get; }
    }
}
