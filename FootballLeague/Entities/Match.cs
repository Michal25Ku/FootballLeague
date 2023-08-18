using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities
{
    public class Match
    {
        public int IdMatch { get; set; }

        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string MatchName { get; set; }
        public DateTime? MatchDate { get; set; }
        public int? GoalsHomeTeam { get; set; }
        public int? GoalsAwayTeam { get; set; }
        public string Result { get; set; }
        public bool IsPlayed { get; set; }
        public int? Round { get; set; }

        public ICollection<Goal> Goals { get; set; } = new List<Goal>();

        public int HomeTeamId { get; set; }
        public Club HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Club AwayTeam { get; set; }
    }
}
