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
        static public List<MatchManager> AllMatchesManager { get; private set; } = new List<MatchManager>();

        public SeasonManager()
        {
            ActualRound = 1;
            using var db = new FootballLeagueContext();
            generator = new SeasonAllMatchesGenerator();
            Rounds = generator.SortMatchesIntoRound(generator.GenerateMatches(db.Clubs.ToList()));
            foreach(var m in db.Matches)
            {
                AllMatchesManager.Add(new MatchManager(m));
            }
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
                var matchManager = AllMatchesManager.FirstOrDefault(mm => mm.PlayedMatch.IdMatch == m.IdMatch);
                if (!matchManager.PlayedMatch.IsPlayed)
                {
                    matchManager.StartMatch();
                }
            }

            ActualRound++;
        }
    }
}
