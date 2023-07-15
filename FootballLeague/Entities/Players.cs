using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities;

public partial class Players
{
    public int IdPlayer { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Pesel { get; set; }

    public int ShirtNumber { get; set; }

    public string Position { get; set; }

    public int? GoalsScored { get; set; }

    public int IdClub { get; set; }

    public virtual ICollection<Goals> Goals { get; set; } = new List<Goals>();

    public virtual Clubs IdClubNavigation { get; set; }
}
