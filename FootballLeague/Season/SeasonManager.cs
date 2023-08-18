using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonManager : IPlayMatch, IPlayRound
    {
        public int ActualRound { get; private set; }
        public Dictionary<int, IList<Match>> Rounds { get; }

        private SeasonAllMatchesGenerator generator;

        public SeasonManager()
        {
            ActualRound = 1;
            using var db = new FootballLeagueContext();
            generator = new SeasonAllMatchesGenerator();
            Rounds = generator.SortMatchesIntoRound(generator.GenerateMatches(db.Clubs.ToList()));
        }

        public void PlayMatch(Match match)
        {
            if (!match.IsPlayed)
            {
                MatchManager matchManager = new MatchManager(match);
                matchManager.StartMatch();
            }
        }

        public void PlayRound(int round)
        {
            foreach (var m in Rounds[round])
            {
                if (!m.IsPlayed)
                {
                    MatchManager match = new MatchManager(m);
                    match.StartMatch();
                }
            }

            ActualRound++;
        }
    }
}
