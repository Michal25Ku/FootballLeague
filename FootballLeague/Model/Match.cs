using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class Match : IEquatable<Match>
    {
        #region mapowanie kolumn
        [Key]
        [Column("IdMatch")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMatch { get; set; }

        [Required]
        [StringLength(30)]
        public string HomeTeam { get; set; }

        [Required]
        [StringLength(30)]
        public string AwayTeam { get; set; }

        [Required]
        [StringLength(50)]
        public string MatchName { get; set; }

        public DateTime MatchDate { get; set; }

        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }

        [Required]
        [StringLength(20)]
        public string Result { get; set; }

        public bool IsPlayed { get; set; }
        public int Round { get; set; }
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

        public Match(int idHomeTeam, int idAwayTeam, DateTime matchDate = default)
        {
            using var db = new FootballLeague();

            if (matchDate == default)
                matchDate = DateTime.Now;

            if (idHomeTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idHomeTeam).IdClub || idAwayTeam != db.Clubs.FirstOrDefault(c => c.IdClub == idAwayTeam).IdClub)
                throw new ArgumentException("Podana drużyna nie istnieje!");

            HomeTeam = db.Clubs.Where(c => c.IdClub == idHomeTeam).Select(c => c.ClubName).FirstOrDefault();
            AwayTeam = db.Clubs.Where(c => c.IdClub == idAwayTeam).Select(c => c.ClubName).FirstOrDefault();
            MatchName = HomeTeam + " - " + AwayTeam;
            MatchDate = matchDate;
            GoalsHomeTeam = 0;
            GoalsAwayTeam = 0;
            Result = "0-0";
            IsPlayed = false;
            IdHomeTeam = idHomeTeam;
            IdAwayTeam = idAwayTeam;
            Round = 0;

            this.Goals = new HashSet<Goal>();

            db.Matches.Add(this);
            SaveChange(db);
        }

        public bool ShootGoal(int minuteOfMatch, int idClub, int idPlayer)
        {
            // Goal can be scored between 1 and 90 minutes
            if (minuteOfMatch < 1 || minuteOfMatch > 90)
                return false;
            if (idClub != IdHomeTeam && idClub != IdAwayTeam)
                return false;

            using var db = new FootballLeague();

            // Is player playing in this match
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdHomeTeam && db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != IdAwayTeam)
                return false;

            // Is player playing for the team that scored the goal
            if (db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer).IdClub != idClub)
                return false;

            var newGoal = new Goal
            {
                MinuteOfTheMatch = minuteOfMatch,
                IdPlayer = idPlayer,
                IdClub = idClub,
                IdMatch = IdMatch
            };

            db.Goals.Add(newGoal);

            // when a player scores a goal, the match, player and club data are updated
            UpdateMatchAfterGoal(idClub);
            UpdatePlayerCount(idPlayer);
            UpdateClubGoals(idClub);

            return SaveChange(db);
        }

        bool UpdateMatchAfterGoal(int idClub)
        {
            using var db = new FootballLeague();

            var matchToUpdate = db.Matches.FirstOrDefault(m => m.IdMatch == IdMatch);

            if (matchToUpdate is null)
                return false;

            if (idClub == db.Matches.FirstOrDefault(m => m.IdMatch == IdMatch).IdHomeTeam)
            {
                matchToUpdate.GoalsHomeTeam += 1;
            }
            else if (idClub == db.Matches.FirstOrDefault(m => m.IdMatch == IdMatch).IdAwayTeam)
            {
                matchToUpdate.GoalsAwayTeam += 1;
            }

            matchToUpdate.Result = matchToUpdate.GoalsHomeTeam + " - " + matchToUpdate.GoalsAwayTeam;

            return SaveChange(db);
        }

        bool UpdatePlayerCount(int idPlayer)
        {
            using var db = new FootballLeague();

            var playerToUpdate = db.Players.FirstOrDefault(p => p.IdPlayer == idPlayer);

            if (playerToUpdate is null)
                return false;

            playerToUpdate.GoalsScored += 1;

            return SaveChange(db);
        }

        bool UpdateClubGoals(int idClub)
        {
            using var db = new FootballLeague();

            if (idClub == IdHomeTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }
            else if (idClub == IdAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.GoalsScored += 1;
                clubToUpdate.GoalBalance += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.GoalsConceded += 1;
                clubToUpdate.GoalBalance -= 1;
            }

            return SaveChange(db);
        }

        public bool UpdateAfterMatchIsOver()
        {
            using var db = new FootballLeague();

            var match = db.Matches.FirstOrDefault(c => c.IdMatch == IdMatch);

            if (match.GoalsHomeTeam > match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Failures += 1;
            }
            else if (match.GoalsHomeTeam == match.GoalsAwayTeam) // draw
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Draws += 1;
                clubToUpdate.Points += 1;
            }
            else if (match.GoalsHomeTeam < match.GoalsAwayTeam)
            {
                var clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdHomeTeam);
                clubToUpdate.Failures += 1;

                clubToUpdate = db.Clubs.FirstOrDefault(c => c.IdClub == IdAwayTeam);
                clubToUpdate.Wins += 1;
                clubToUpdate.Points += 3;
            }

            match.IsPlayed = true;

            return SaveChange(db);
        }

        static bool SaveChange(FootballLeague db)
        {
            int result = db.SaveChanges();
            return result == 1;
        }

        public override bool Equals(object obj)
        {
            if(obj is null)
                return false;

            if(obj is Match)
                return base.Equals(obj);
            
            return false;
        }

        public bool Equals(Match other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (this.IdHomeTeam == other.IdHomeTeam && this.IdAwayTeam == other.IdAwayTeam)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Match left, Match right)
            => left.Equals(right);

        public static bool operator !=(Match left, Match right)
            => !(left == right);
    }
}
