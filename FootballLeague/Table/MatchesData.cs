using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;

namespace FootballLeagueLib.DataForWPF
{
    public class MatchesData : ICreateAndUpdateDataMatch
    {
        public List<Match> MatchesList { get; private set; }

        public MatchesData() 
        {
            using var db = new FootballLeagueContext();

            MatchesList = CreateMatchesList(db.Matches.ToList());
        }

        public List<Match> CreateMatchesList(List<Match> matches)
        {
            MatchesList = matches.OrderBy(m => m.Round).ToList();

            return MatchesList;
        }

        public List<Match> UpdateMatchesList()
        {
            using var db = new FootballLeagueContext();
            MatchesList = db.Matches.OrderBy(m => m.Round).ToList();

            return MatchesList;
        }
    }
}
