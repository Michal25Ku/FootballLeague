using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class TopScorer : IGetTopScorerList
    {
        public List<Tuple<int, Player, Club>> TopScorers()
        {
            using var db = new FootballLeagueContext();
            List<Tuple<int, Player, Club>> topScorer = new List<Tuple<int, Player, Club>>();
            int i = 1;

            var players = db.Players.OrderByDescending(p => p.GoalsScored).ToList();
            foreach (var player in players) 
            {
                if(i <= 5 && player.GoalsScored > 0)
                {
                    topScorer.Add(new Tuple<int, Player, Club>(i, player, db.Clubs.FirstOrDefault(c => c.IdClub == player.ClubId)));
                    i++;
                }
            }

            return topScorer;
        }
    }
}
