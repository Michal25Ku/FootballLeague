using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class ScorerListForMatch : ICreateAndUpdateScorerList
    {
        public List<Tuple<int, Player>> CreateScorerList(Match match, int idClub)
        {
            List<Tuple<int, Player>> scorer = new List<Tuple<int, Player>>();
            using var db = new FootballLeagueContext();
            var goals = db.Goals.Select(g => g).Where(g => g.MatchId == match.IdMatch).ToList();

            foreach (var g in goals)
            {
                if(g.ClubId == idClub)
                {
                    scorer.Add(new Tuple<int, Player> (g.MinuteOfTheMatch, db.Players.FirstOrDefault(p => (p.IdPlayer == g.PlayerId) && (p.ClubId == idClub))));
                }
            }

            return scorer;
        }
    }
}
