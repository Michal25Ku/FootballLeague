using FootballLeagueLib.Interfaces;
using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.PlayMatch
{
    public class MatchPlayers : IGetPlayers
    {
        public List<Player> AwayTeamPlayers(int idClub)
        {
            Random rand = new Random();
            using var db = new FootballLeague();
            List<Player> players = db.Players.Where(p => p.IdClub == idClub).ToList();

            while (players.Count > 11)
            {
                if (!(players[rand.Next(players.Count)] is null))
                    players.Remove(players[rand.Next(players.Count)]);
            }

            return players;
        }

        public List<Player> HomeTeamPlayers(int idClub)
        {
            Random rand = new Random();
            using var db = new FootballLeague();
            List<Player> players = db.Players.Where(p => p.IdClub == idClub).ToList();

            while (players.Count > 11)
            {
                if (!(players[rand.Next(players.Count)] is null))
                    players.Remove(players[rand.Next(players.Count)]);
            }

            return players;
        }
    }
}
