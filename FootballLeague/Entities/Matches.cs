using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities;

public partial class Matches
{
    public int IdMatch { get; set; }

    public string HomeTeam { get; set; }

    public string AwayTeam { get; set; }

    public string MatchName { get; set; }

    public DateTime? MatchDate { get; set; }

    public int? GoalsHomeTeam { get; set; }

    public int? GoalsAwayTeam { get; set; }

    public string Result { get; set; }

    public int IdHomeTeam { get; set; }

    public int IdAwayTeam { get; set; }

    public bool IsPlayed { get; set; }

    public int Round { get; set; }

    public virtual ICollection<Goals> Goals { get; set; } = new List<Goals>();

    public virtual Clubs IdAwayTeamNavigation { get; set; }

    public virtual Clubs IdHomeTeamNavigation { get; set; }
}
