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
    public class SeasonManager : IPlayRound
    {
        public static int ActualRound { get; private set; }
        public Dictionary<int, IList<Match>> Rounds { get; }

        private SeasonAllMatchesGenerator generator;

        public SeasonManager()
        {
            ActualRound = 1;
            using var db = new FootballLeagueContext();
            generator = new SeasonAllMatchesGenerator();
            Rounds = generator.SortMatchesIntoRound(generator.GenerateMatches(db.Clubs.ToList()));
        }

        public void PlayRound()
        {
            using var db = new FootballLeagueContext();
            int round = 1, i = 0;
            var matches = db.Matches.OrderBy(m => m.Round).ToList();

            while(matches[i].IsPlayed)
            {
                round = (int)matches[i].Round;
                i++;
            }

            Rounds[round] = db.Matches.Select(m => m).Where(m => m.Round == round).ToList();

            while(Rounds[round].All(m => m.IsPlayed))
            {
                ActualRound++;
                round++;
            }

            Rounds[round] = db.Matches.Select(m => m).Where(m => m.Round == round).ToList();

            foreach(var m in Rounds[round])
            {
                if (!m.IsPlayed)
                {
                    SeasonPlayMatch _seasonPlayMatch = new SeasonPlayMatch(m);
                    _seasonPlayMatch.PlayMatch();
                }
            }

            ActualRound++;
        }
    }
}
