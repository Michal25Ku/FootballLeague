using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities
{
    public class Goal
    {
        public int IdGoal { get; set; }

        public int MinuteOfTheMatch { get; set; }

        public int ClubId { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}
