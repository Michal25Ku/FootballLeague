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
        /// <summary>
        /// Creates IQueryable<Player> which filtres Player who plays in a club defined by idClub in the parameter
        /// </summary>
        /// <param name="idClub">club id from Club object</param>
        /// <returns>List of players who play in club defined by idClub in parameter</returns>
        public List<Player> GetPlayersFromTeam(int idClub)
        {
            using var db = new FootballLeagueContext();
            IQueryable<Player> players = db.Players.Where(p => p.ClubId == idClub);
            return players.ToList();
        }
    }
}
