using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class Goal
    {
        #region mapowanie kolumn
        [Key]
        [Column("IdGoal")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGoal { get; set; }

        public int MinuteOfTheMatch { get; set; }
        #endregion

        #region Foreign key
        [ForeignKey("Player")]
        public int IdPlayer { get; set; }
        public virtual Player Player { get; set; }

        [ForeignKey("Club")]
        public int IdClub { get; set; }
        public virtual Club Club { get; set; }

        [ForeignKey("Match")]
        public int IdMatch { get; set; }
        public virtual Match Match { get; set; }
        #endregion

        public Goal() { }
    }
}
