using FootballLeagueLib.Entities;
using FootballLeagueLib.Interfaces;
using FootballLeagueLib.PlayMatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Table
{
    public class ScorerListForMatch : IUpdateScorerList
    {
        public StringBuilder ScorerListForHomeTeam { get; private set; }
        public StringBuilder ScorerListForAwayTeam { get; private set; }

        /// <summary>
        /// Initialize ScorerListForHomeTeam and ScorerListForAwayTeam parameters
        /// </summary>
        public ScorerListForMatch()
        {
            ScorerListForHomeTeam = new StringBuilder();
            ScorerListForAwayTeam = new StringBuilder();
        }

        public void UpdateListForHomeTeam(Match match, int idClub)
        {
            List<Tuple<int, Player>> scorer = new List<Tuple<int, Player>>();
            using var db = new FootballLeagueContext();
            var goals = db.Goals.Where(g => g.MatchId == match.IdMatch).AsNoTracking().ToList();

            foreach (var g in goals)
            {
                if(g.ClubId == idClub)
                {
                    scorer.Add(new Tuple<int, Player> (g.MinuteOfTheMatch, db.Players.FirstOrDefault(p => (p.IdPlayer == g.PlayerId) && (p.ClubId == idClub))));
                }
            }

            return scorer;
        }

        public void UpdateListForAwayTeam(Match match, int idClub)
        {
            List<Tuple<int, Player>> scorer = new List<Tuple<int, Player>>();
            using var db = new FootballLeagueContext();
            var goals = db.Goals.Where(g => g.MatchId == match.IdMatch).AsNoTracking().ToList();

            foreach (var g in goals)
            {
                if (g.ClubId == idClub)
                {
                    scorer.Add(new Tuple<int, Player>(g.MinuteOfTheMatch, db.Players.FirstOrDefault(p => (p.IdPlayer == g.PlayerId) && (p.ClubId == idClub))));
                }
            }

            return scorer;
        }
    }
}
