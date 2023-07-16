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

        [Required]
        [StringLength(50)]
        public string ClubName { get; set; }

        [Required]
        [StringLength(50)]
        public string StadiumName { get; set; }

        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalBalance { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Failures { get; set; }
        public int Points { get; set; }
        #endregion

        public virtual ICollection<Player> Players { get; set; }

        [NotMapped]
        public virtual ICollection<Match> MatchesGuest { get; set; }
        [NotMapped]
        public virtual ICollection<Match> MatchesHost { get; set; }

        public Club()
        {
            GoalsScored = 0;
            GoalsConceded = 0;
            GoalBalance = 0;
            Wins = 0;
            Draws = 0;
            Failures = 0;
            Points = 0;

            this.Players = new HashSet<Player>();
            this.MatchesGuest = new HashSet<Match>();
            this.MatchesHost = new HashSet<Match>();
        }

    }
}
