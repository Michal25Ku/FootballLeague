using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities;

public partial class Clubs
{
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

    public virtual ICollection<Matches> MatchesIdAwayTeamNavigation { get; set; } = new List<Matches>();

    public virtual ICollection<Matches> MatchesIdHomeTeamNavigation { get; set; } = new List<Matches>();

    public virtual ICollection<Players> Players { get; set; } = new List<Players>();
}
