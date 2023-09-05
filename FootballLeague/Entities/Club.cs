using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities
{
    public class Club
    {
        public int IdClub { get; set; }

        public string ClubName { get; set; }
        public string StadiumName { get; set; }
        public int? GoalsScored { get; set; }
        public int? GoalsConceded { get; set; }
        public int? GoalBalance { get; set; }
        public int? Wins { get; set; }
        public int? Draws { get; set; }
        public int? Failures { get; set; }
        public int? Points { get; set; }

        public ICollection<Match> MatchesHomeTeam { get; set; } = new List<Match>();
        public ICollection<Match> MatchesAwayTeam { get; set; } = new List<Match>();

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
