﻿using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface ICreateAndUpdateScorerList
    {
        Dictionary<int, Player> CreateScorerList(MatchManager matchManager, int idClub);
    }
}
