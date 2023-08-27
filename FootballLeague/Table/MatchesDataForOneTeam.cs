using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class MatchesDataForOneTeam : ICreateListMatchesForOneTeam
    {
        public List<Match> MatchesList { get; private set; }

        public MatchesDataForOneTeam(int clubId)
        {
            using var db = new FootballLeagueContext();

            MatchesList = CreateMatchesList(db.Matches.ToList(), clubId);
        }

        public List<Match> CreateMatchesList(List<Match> matches, int clubId)
        {
            MatchesList = matches.Select(m => m).Where(m => (m.HomeTeamId == clubId || m.AwayTeamId == clubId)).OrderBy(m => m.Round).ToList();

            return MatchesList;
        }
    }
}
