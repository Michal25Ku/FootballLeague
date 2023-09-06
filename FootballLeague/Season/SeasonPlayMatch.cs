using FootballLeagueLib.Entities;
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
        public MatchManager MatchManager { get; private set; }
        public void PlayMatch(Match match)
        { 
            if (!match.IsPlayed)
            {
                MatchManager = new MatchManager(match);
                MatchManager.StartMatch();
            }
        }
    }
}
