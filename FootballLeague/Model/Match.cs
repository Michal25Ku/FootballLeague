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

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string MatchName { get; set; }
        public DateTime? MatchDate { get; set; }
        public int? GoalsHomeTeam { get; set; }
        public int? GoalsAwayTeam { get; set; }
        public string Result { get; set; }
        public bool IsPlayed { get; set; }
        public int? Round { get; set; }
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

        public Match(int idHomeTeam, int idAwayTeam, DateTime matchDate)
        {
            using var db = new FootballLeague();

            if (idHomeTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idHomeTeam).IdClub || idAwayTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idAwayTeam).IdClub)
                throw new ArgumentException("Podana drużyna nie istnieje!");

            HomeTeam = db.Clubs.Where(c => c.IdClub == idHomeTeam).Select(c => c.ClubName).FirstOrDefault();
            AwayTeam = db.Clubs.Where(c => c.IdClub == idAwayTeam).Select(c => c.ClubName).FirstOrDefault();
            MatchDate = matchDate;
            IdHomeTeam = idHomeTeam;
            IdAwayTeam = idAwayTeam;
            MatchName = HomeTeam + " - " + AwayTeam;

            this.Goals = new HashSet<Goal>();

            db.SaveChanges();
        }

        public Match(int idHomeTeam, int idAwayTeam) : this(idHomeTeam, idAwayTeam, DateTime.Now) { }
    }
}
