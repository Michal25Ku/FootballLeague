using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.IntegrationTests
{
    public class MatchScoredGoalTest
    {
        private ExemplaryDatabase _exemplaryDatabase = new ExemplaryDatabase();
        private Match _testMatch;
        private Club _testClub1Home;
        private Club _testClub2Away;

        private MatchManager AddTestMatchManager()
        {
            using var db = new FootballLeagueContext();
            _testClub1Home = db.Clubs.FirstOrDefault(c => c.ClubName == "Club1");
            _testClub2Away = db.Clubs.FirstOrDefault(c => c.ClubName == "Club2");

            _testMatch = new Match
            {
                HomeTeamName = _testClub1Home.ClubName,
                AwayTeamName = _testClub2Away.ClubName,
                MatchName = _testClub1Home.ClubName + " - " + _testClub2Away.ClubName,
                HomeTeamId = _testClub1Home.IdClub,
                AwayTeamId = _testClub2Away.IdClub
            };
            db.Matches.Add(_testMatch);
            db.SaveChanges();

            return new MatchManager(_testMatch);
        }

        [Test, Isolated]
        public void ScoreGoalMethod_AddGoal()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            testMatchManager.MatchScoreGoal.ScoreGoal(10, testMatchManager.PlayedMatch.HomeTeamId, testMatchManager.HomeTeamPlayers[0].IdPlayer, testMatchManager);

            var goal = db.Goals.FirstOrDefault(g => g.MatchId == testMatchManager.PlayedMatch.IdMatch);
            Assert.That(goal.MinuteOfTheMatch, Is.EqualTo(10));
            Assert.That(goal.ClubId, Is.EqualTo(_testClub1Home.IdClub));
            Assert.That(goal.PlayerId, Is.EqualTo(testMatchManager.HomeTeamPlayers[0].IdPlayer));
            Assert.That(goal.MatchId, Is.EqualTo(_testMatch.IdMatch));
        }

        [Test, Isolated]
        public void ScoreGoalMethod_CorrectData_ShouldReturnTrue()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            bool ScoreGoalBool = testMatchManager.MatchScoreGoal.ScoreGoal(10, testMatchManager.PlayedMatch.HomeTeamId, testMatchManager.HomeTeamPlayers[0].IdPlayer, testMatchManager);

            Assert.IsTrue(ScoreGoalBool);
        }

        [Test, Isolated]
        public void UpdateMatchAfterGoal_UpdateMatchResult_ShouldSetGoalsHomeTeamTo_1_GoalsAwayTeamTo_0()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            var newGoal = new Goal
            {
                MinuteOfTheMatch = 10,
                ClubId = _testClub1Home.IdClub,
                PlayerId = testMatchManager.HomeTeamPlayers[0].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.HomeTeamPlayers[0],
                Match = _testMatch
            };

            testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);

            _testMatch = db.Matches.FirstOrDefault(m => m.IdMatch == _testMatch.IdMatch);
            Assert.That(_testMatch.GoalsHomeTeam, Is.EqualTo(1));
            Assert.That(_testMatch.GoalsAwayTeam, Is.EqualTo(0));
            Assert.That(_testMatch.Result, Is.EqualTo("1 - 0"));
        }

        [Test, Isolated]
        public void UpdateMatchAfterGoal_UpdateMatchResult_ShouldSetGoalsAwayTeamTo_1_GoalsHomeTeamTo_0()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            var newGoal = new Goal
            {
                MinuteOfTheMatch = 10,
                ClubId = _testClub2Away.IdClub,
                PlayerId = testMatchManager.AwayTeamPlayers[0].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.AwayTeamPlayers[0],
                Match = _testMatch
            };
            testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);

            _testMatch = db.Matches.FirstOrDefault(m => m.IdMatch == _testMatch.IdMatch);
            Assert.That(_testMatch.GoalsHomeTeam, Is.EqualTo(0));
            Assert.That(_testMatch.GoalsAwayTeam, Is.EqualTo(1));
            Assert.That(_testMatch.Result, Is.EqualTo("0 - 1"));
        }

        [Test, Isolated]
        public void UpdateMatchAfterGoal_UpdateMatchResult_ShouldSetGoalsHomeTeamTo_2_GoalsAwayTeamTo_1()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            var newGoal = new Goal
            {
                MinuteOfTheMatch = 10,
                ClubId = _testClub1Home.IdClub,
                PlayerId = testMatchManager.HomeTeamPlayers[0].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.HomeTeamPlayers[0],
                Match = _testMatch
            };
            testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);
            newGoal = new Goal
            {
                MinuteOfTheMatch = 12,
                ClubId = _testClub2Away.IdClub,
                PlayerId = testMatchManager.AwayTeamPlayers[0].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.AwayTeamPlayers[0],
                Match = _testMatch
            };
            testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);
            newGoal = new Goal
            {
                MinuteOfTheMatch = 13,
                ClubId = _testClub1Home.IdClub,
                PlayerId = testMatchManager.HomeTeamPlayers[2].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.HomeTeamPlayers[0],
                Match = _testMatch
            };
            testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);

            _testMatch = db.Matches.FirstOrDefault(m => m.IdMatch == _testMatch.IdMatch);
            Assert.That(_testMatch.GoalsHomeTeam, Is.EqualTo(2));
            Assert.That(_testMatch.GoalsAwayTeam, Is.EqualTo(1));
            Assert.That(_testMatch.Result, Is.EqualTo("2 - 1"));
        }

        [Test, Isolated]
        public void UpdateMatchAfterGoal_CorrectData_ShouldRetunTrue()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            var newGoal = new Goal
            {
                MinuteOfTheMatch = 10,
                ClubId = _testClub1Home.IdClub,
                PlayerId = testMatchManager.HomeTeamPlayers[0].IdPlayer,
                MatchId = _testMatch.IdMatch,
                Player = testMatchManager.HomeTeamPlayers[0],
                Match = _testMatch
            };
            bool UpdateMatchAfterGoal = testMatchManager.MatchScoreGoal.UpdateMatchAfterGoal(newGoal, testMatchManager);

            Assert.IsTrue(UpdateMatchAfterGoal);
        }

        [Test, Isolated]
        public void UpdatePlayerCount_UpdateGoalsScoredInPlayers_ShouldSetGoalsScoreTo_1()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[0].IdPlayer);

            Player player = db.Players.FirstOrDefault(p => p.IdPlayer == testMatchManager.HomeTeamPlayers[0].IdPlayer);
            Assert.That(player.GoalsScored, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void UpdatePlayerCount_UpdateGoalsScoredInPlayers_ShouldSetGoalsScoreTo_3_ScoredManyGoals()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[0].IdPlayer);
            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[0].IdPlayer);
            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[0].IdPlayer);

            Player player = db.Players.FirstOrDefault(p => p.IdPlayer == testMatchManager.HomeTeamPlayers[0].IdPlayer);
            Assert.That(player.GoalsScored, Is.EqualTo(3));
        }

        [Test, Isolated]
        public void UpdatePlayerCount_UpdateGoalsScoredInPlayers_ManyPlayersScoredGoal()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();
            
            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[1].IdPlayer);
            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[1].IdPlayer);
            testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.AwayTeamPlayers[0].IdPlayer);

            Assert.That(db.Players.FirstOrDefault(p => p.IdPlayer == testMatchManager.HomeTeamPlayers[1].IdPlayer).GoalsScored, Is.EqualTo(2));
            Assert.That(db.Players.FirstOrDefault(p => p.IdPlayer == testMatchManager.AwayTeamPlayers[0].IdPlayer).GoalsScored, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void UpdatePlayerCount_CorrectData_ShouldReturnTrue()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            bool UpdatePlayerCountBool = testMatchManager.MatchScoreGoal.UpdatePlayerCount(testMatchManager.HomeTeamPlayers[1].IdPlayer);

            Assert.IsTrue(UpdatePlayerCountBool);
        }

        [Test, Isolated]
        public void UpdateClubGoals_UpdateGoalsInClub_Should_AddGoalsScoredAndGoalBalanceInScorerClub_And_AddGoalsConcededSubstractGoalBalanceInEnemyClub()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.HomeTeamId, testMatchManager);

            _testClub1Home = db.Clubs.FirstOrDefault(c => c.IdClub == testMatchManager.PlayedMatch.HomeTeamId);
            Assert.That(_testClub1Home.GoalsScored, Is.EqualTo(1));
            Assert.That(_testClub1Home.GoalsConceded, Is.EqualTo(0));
            Assert.That(_testClub1Home.GoalBalance, Is.EqualTo(1));

            _testClub2Away = db.Clubs.FirstOrDefault(c => c.IdClub == testMatchManager.PlayedMatch.AwayTeamId);
            Assert.That(_testClub2Away.GoalsScored, Is.EqualTo(0));
            Assert.That(_testClub2Away.GoalsConceded, Is.EqualTo(1));
            Assert.That(_testClub2Away.GoalBalance, Is.EqualTo(-1));
        }

        [Test, Isolated]
        public void UpdateClubGoals_UpdateGoalsInClub_Should_AddGoalsScoredAndGoalBalanceInScorerClub_And_AddGoalsConcededSubstractGoalBalanceInEnemyClub_Many_Goals()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.HomeTeamId, testMatchManager);
            testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.AwayTeamId, testMatchManager);
            testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.HomeTeamId, testMatchManager);
            testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.HomeTeamId, testMatchManager);

            _testClub1Home = db.Clubs.FirstOrDefault(c => c.IdClub == testMatchManager.PlayedMatch.HomeTeamId);
            Assert.That(_testClub1Home.GoalsScored, Is.EqualTo(3));
            Assert.That(_testClub1Home.GoalsConceded, Is.EqualTo(1));
            Assert.That(_testClub1Home.GoalBalance, Is.EqualTo(2));

            _testClub2Away = db.Clubs.FirstOrDefault(c => c.IdClub == testMatchManager.PlayedMatch.AwayTeamId);
            Assert.That(_testClub2Away.GoalsScored, Is.EqualTo(1));
            Assert.That(_testClub2Away.GoalsConceded, Is.EqualTo(3));
            Assert.That(_testClub2Away.GoalBalance, Is.EqualTo(-2));
        }

        [Test, Isolated]
        public void UpdateClubGoals_CorrectData_ShouldReturnTrue()
        {
            using var db = new FootballLeagueContext();
            MatchManager testMatchManager = AddTestMatchManager();

            bool UpdateClubGoalsBool = testMatchManager.MatchScoreGoal.UpdateClubGoals(testMatchManager.PlayedMatch.HomeTeamId, testMatchManager);

            Assert.IsTrue(UpdateClubGoalsBool);
        }
    }
}
