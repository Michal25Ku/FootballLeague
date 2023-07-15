using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class Match
    {
        #region mapowanie kolumn
        [Key]
        [Column("IdMatch")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMatch { get; set; }

        [Required]
        [StringLength(30)]
        public string HomeTeam { get; set; } = "";

        [Required]
        [StringLength(30)]
        public string AwayTeam { get; set; } = "";

        [Required]
        [StringLength(50)]
        public string MatchName { get; set; } = "";

        public DateTime MatchDate { get; set; }

        public int GoalsHomeTeam { get; set; } = 0;
        public int GoalsAwayTeam { get; set; } = 0;

        [Required]
        [StringLength(20)]
        public string Result { get; set; } = "0-0";
        #endregion

        public virtual ICollection<Goal> Goals { get; set; }

        #region Foreign key
        [ForeignKey("ClubHost")]
        [Column("IdHomeTeam")]
        public int IdHomeTeam { get; set; }
        public virtual Club ClubHost { get; set; }

        [ForeignKey("ClubGuest")]
        [Column("IdAwayTeam")]
        public int IdAwayTeam { get; set; }
        public virtual Club ClubGuest { get; set; }
        #endregion

        public Match()
        {
            HomeTeam = string.Empty;
            AwayTeam = string.Empty;
            MatchName = string.Empty;
            MatchDate = DateTime.Now;
            GoalsHomeTeam = 0;
            GoalsAwayTeam = 0;
            Result = "0-0";

            this.Goals = new HashSet<Goal>();
        }
    }
}
