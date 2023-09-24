using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchPlayers
    {
        public List<Player> GetPlayersFromTeam(int idClub)
        {
            using var db = new FootballLeagueContext();
            IQueryable<Player> players = db.Players.Where(p => p.ClubId == idClub);
            return players.ToList();
        }
    }
}
