using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using NUnit.Framework;

namespace FootballLeague.IntegrationTests
{
    public class ClubTest
    {
        private Club _club;

        private void _addNewTestClub()
        {
            using var db = new FootballLeagueContext();
            _club = new Club
            {
                ClubName = "ClubTest",
                StadiumName = "Stadium1"
            };

            db.Clubs.Add(_club);
            db.SaveChanges();
        }

        #region Tested club when added
        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClubToDatabase()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubCount = db.Clubs.Count(x => x.ClubName == _club.ClubName);
            Assert.That(clubCount, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetGoalsScoredTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubGoalsScored = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.GoalsScored;
            Assert.That(clubGoalsScored, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetGoalsConcededTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubGoalsConceded = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.GoalsConceded;
            Assert.That(clubGoalsConceded, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetGoalBalanceTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubGoalBalance = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.GoalBalance;
            Assert.That(clubGoalBalance, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetWinsTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubWins = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.Wins;
            Assert.That(clubWins, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetDrawsTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubDraws = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.Draws;
            Assert.That(clubDraws, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetFailuresTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubFailures = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.Failures;
            Assert.That(clubFailures, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Add_PassValidClub_ShouldAddClub_And_SetPointsTo_0()
        {
            using var db = new FootballLeagueContext();
            _addNewTestClub();

            var clubPoints = db.Clubs.FirstOrDefault(x => x.ClubName == _club.ClubName)?.Points;
            Assert.That(clubPoints, Is.EqualTo(0));
        }
        #endregion
    }
}