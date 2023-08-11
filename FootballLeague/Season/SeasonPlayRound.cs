using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Model;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonPlayRound : IPlayRound
    {
        private SeasonManager _seasonManager;

        public SeasonPlayRound(SeasonManager seasonManager)
        {
            _seasonManager = seasonManager;
        }

        public void PlayRound(int round)
        {
            foreach (var m in _seasonManager.Rounds[round])
            {
                if(!m.IsPlayed)
                {
                    MatchManager match = new MatchManager(m);
                    match.StartMatch();
                }
            }
        }
    }
}
