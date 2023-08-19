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
        public Dictionary<int, Player> CreateScorerList(MatchManager matchManager, int idClub)
        {
            Dictionary<int, Player> scorer = new Dictionary<int, Player>();
            using var db = new FootballLeagueContext();
            var goals = db.Goals.Select(g => g).Where(g => g.MatchId == matchManager.PlayedMatch.IdMatch).ToList();

            foreach (var g in goals)
            {
                scorer.Add
                (
                    g.MinuteOfTheMatch,
                    db.Players.FirstOrDefault(p => (p.IdPlayer == g.PlayerId) && (p.ClubId == idClub))
                );
            }

            return scorer;
        }
    }
}
