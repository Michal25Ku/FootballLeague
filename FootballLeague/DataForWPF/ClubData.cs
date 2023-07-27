using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.DataForWPF
{
    public class ClubData
    {
        public string ClubName { get; set; }
        public int MatchCount { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Failures { get; set; }
        public int GoalScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsBalance { get; set; }
        public int Point { get; set; }
    }
}
