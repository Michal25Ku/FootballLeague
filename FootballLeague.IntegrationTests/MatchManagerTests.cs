using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.IntegrationTests
{
    class MatchManagerTests
    {
        private ExemplaryDatabase _exemplaryDatabase = new ExemplaryDatabase();
        private Match _testMatch;
        private Club _testClub1;
        private Club _testClub2;

        private MatchManager AddTestMatchManager()
        {
            using var db = new FootballLeagueContext();
            _testClub1 = db.Clubs.FirstOrDefault(c => c.ClubName == "Club1");
            _testClub2 = db.Clubs.FirstOrDefault(c => c.ClubName == "Club2");

            _testMatch = new Match
            {
                HomeTeamName = _testClub1.ClubName,
                AwayTeamName = _testClub2.ClubName,
                MatchName = _testClub1.ClubName + " - " + _testClub2.ClubName,
                HomeTeamId = _testClub1.IdClub,
                AwayTeamId = _testClub2.IdClub
            };
            db.Matches.Add(_testMatch);
            db.SaveChanges();

            return new MatchManager(_testMatch);
        }

        [Test, Isolated]
        public void Add_MatchManager_Test_Contructor()
        {
            using var db = new FootballLeagueContext();

            MatchManager testMatchManager = AddTestMatchManager();

            var playedMatch = testMatchManager.PlayedMatch;
            var simulationTime = testMatchManager.SimulationTime;
            var minuteOfMatch = testMatchManager.MinuteInMatch;

            Assert.That(playedMatch.IdMatch, Is.EqualTo(_testMatch.IdMatch));
            Assert.That(simulationTime, Is.EqualTo(0));
            Assert.That(minuteOfMatch, Is.EqualTo(0));
        }
    }
}
