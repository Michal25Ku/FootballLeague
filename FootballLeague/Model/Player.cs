using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class Player
    {
        #region Mapowanie kolumn
        [Key]
        [Column("IdPlayer")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlayer { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PESEL { get; set; }
        public int ShirtNumber { get; set; }
        public string Position { get; set; }
        public int? GoalsScored { get; set; }
        #endregion

        public virtual ICollection<Goal> Goals { get; set; }

        #region Foreign key
        public int IdClub { get; set; }
        public virtual Club Club { get; set; }
        #endregion

        public Player()
        {
            this.Goals = new HashSet<Goal>();
        }
    }
}
