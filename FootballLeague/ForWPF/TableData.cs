using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class TableData : IUpdateTable
    {
        public List<Tuple<int, Club, int>> Table { get; private set; }

        /// <summary>
        /// Initialize List<Tuple<int, Club, int>> Table
        /// </summary>
        public TableData() 
        {
            Table = UpdateTable();
        }

        public List<Tuple<int, Club, int>> UpdateTable()
        {
            using var db = new FootballLeagueContext();
            Table = new List<Tuple<int, Club, int>>();
            var query = db.Clubs.ToList().OrderByDescending(c => c.Points).ThenByDescending(c => c.GoalBalance).ThenByDescending(c => c.GoalsScored);

            int rank = 1;

            foreach(var c in query)
            {
                int matchCount = db.Matches.Select(m => m).Where(m => m.IsPlayed).Where(club => club.HomeTeamId == c.IdClub || club.AwayTeamId == c.IdClub).Count();
                Table.Add(new Tuple<int, Club, int>(rank, c, matchCount));
                rank++;
            }

            return Table;
        }
    }
}
