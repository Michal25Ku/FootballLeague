using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities
{
    public partial class Player
    {
        public int IdPlayer { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public int ShirtNumber { get; set; }
        public string Position { get; set; }
        public int GoalsScored { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }

        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }

}