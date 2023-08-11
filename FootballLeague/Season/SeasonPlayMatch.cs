using FootballLeagueLib.Interfaces;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonPlayMatch : IPlayMatch
    {
        private SeasonManager _seasonManager;

        public SeasonPlayMatch(SeasonManager seasonManager)
        {
            _seasonManager = seasonManager;
        }

        public void PlayMatch(Model.Match match)
        {
            if (!match.IsPlayed)
            {
                MatchManager matchManager = new MatchManager(match);
                matchManager.StartMatch();
            }
        }
    }
}
