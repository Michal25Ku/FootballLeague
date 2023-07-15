using System;
using System.Collections.Generic;

namespace FootballLeagueLib.Entities;

public partial class Goals
{
    public int IdGoal { get; set; }

    public int MinuteOfTheMatch { get; set; }

    public int IdPlayer { get; set; }

    public int IdClub { get; set; }

    public int IdMatch { get; set; }

    public virtual Players Id { get; set; }

    public virtual Matches IdMatchNavigation { get; set; }
}
