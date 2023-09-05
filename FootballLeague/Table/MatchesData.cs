using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;

namespace FootballLeagueLib.Table
{
    public class MatchesData : IUpdateMatchesList, IUpdateListMatchesForOneTeam
    {
        public List<Match> MatchesList { get; private set; }

        public MatchesData()
        {
            using var db = new FootballLeagueContext();

            MatchesList = UpdateMatchesList();
        }

        public List<Match> UpdateMatchesList()
        {
            using var db = new FootballLeagueContext();
            MatchesList = db.Matches.OrderBy(m => m.Round).ToList();

            return MatchesList;
        }

        public List<Match> UpdateMatchesForOneClub(Club club)
        {
            using var db = new FootballLeagueContext();
            MatchesList = db.Matches.Select(m => m).Where(m => m.HomeTeamId == club.IdClub || m.AwayTeamId == club.IdClub).OrderBy(m => m.Round).ToList();

            return MatchesList;
        }
    }
}
