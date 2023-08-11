using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class SeasonManager
    {
        public static int actualRound = 1;
        public Dictionary<int, IList<Model.Match>> Rounds { get; }

        private SeasonAllMatchesGenerator generator;

        public SeasonManager()
        {
            using var db = new Model.FootballLeague();
            generator = new SeasonAllMatchesGenerator();
            Rounds = generator.SortMatchesIntoRound(generator.GenerateMatches(db.Clubs.ToList()));
        }
    }
}
