using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchPlayers
    {
        public List<Player> GetPlayersFromTeam(int idClub)
        {
            Random rand = new Random(); // simulation, which players will be playing
            using var db = new FootballLeagueContext();
            List<Player> players = db.Players.Where(p => p.ClubId == idClub).ToList();

            while (players.Count > 11)
            {
                if (players[rand.Next(players.Count)] is not null)
                    players.Remove(players[rand.Next(players.Count)]);
            }

            return players;
        }
    }
}
