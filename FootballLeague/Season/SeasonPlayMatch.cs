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

        public SeasonPlayMatch(Match match)
        {
            MatchManager = new MatchManager(match);
        }

        public void PlayMatch()
        { 
            if (!MatchManager.PlayedMatch.IsPlayed)
            {
                MatchManager.StartMatch();
            }
        }
    }
}
