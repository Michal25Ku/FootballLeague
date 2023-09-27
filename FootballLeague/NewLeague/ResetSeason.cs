using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.NewLeague
{
    public class ResetSeason : IResetLeague
    {
        public void ResetDatabase()
        {
            using var db = new FootballLeagueContext();
            db.Goals.RemoveRange(db.Goals);
            db.Matches.RemoveRange(db.Matches);
            db.Players.RemoveRange(db.Players);
            db.Clubs.RemoveRange(db.Clubs);

            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AddConnectionsInDatabase()
        {
            using var db = new FootballLeagueContext();

            var clubsWithPlayers = db.Clubs.Include(c => c.Players).ToList();

            foreach (var player in db.Players)
            {
                var club = clubsWithPlayers.FirstOrDefault(c => c.IdClub == player.ClubId);
                club?.Players.Add(player);
            }

            int result = db.SaveChanges();
            return result == 1;
        }
    }
}
