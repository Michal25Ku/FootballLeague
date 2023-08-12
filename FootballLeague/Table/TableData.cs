using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class TableData : ICreateUpdateTable
    {
        public List<(int, Club, int)> Table { get; }

        public TableData() 
        {
            using var db = new FootballLeague();

            Table = CreateTable(db.Clubs.ToList());
        }

        public List<(int, Club, int)> CreateTable(IList<Club> clubs)
        {
            List<(int, Club, int)> table = new List<(int, Club, int)>();
            var query = clubs.OrderBy(c => c.ClubName);

            for(int i = 0; i < clubs.Count; i++)
            {
                table.Add((i + 1, clubs[i], 0));
            }

            return table;
        }

        public void UpdateTable(ref IList<Club> clubs)
        {
            var query = clubs.OrderBy(c => c.Points).ThenBy(c => c.GoalBalance).ThenBy(c => c.GoalsConceded);
        }
    }
}
