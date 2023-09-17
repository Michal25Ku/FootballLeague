using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Interfaces
{
    public interface IScoreGoal
    {
        bool ScoreGoal(int minuteOfMatch, int idClub, int idPlayer, MatchManager matchManager);

        bool UpdateMatchAfterGoal(Goal goal, MatchManager matchManager);

        bool UpdatePlayerCount(int idPlayer);

        bool UpdateClubGoals(int idClub, MatchManager matchManager);
    }
}
