using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class Club
    {
        #region mapowanie kolumn
        [Key]
        [Column("IdClub")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        #endregion

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Match> MatchesGuest { get; set; }
        public virtual ICollection<Match> MatchesHost { get; set; }

        public Club()
        {
            this.Players = new HashSet<Player>();
            this.MatchesGuest = new HashSet<Match>();
            this.MatchesHost = new HashSet<Match>();
        }

    }
}
