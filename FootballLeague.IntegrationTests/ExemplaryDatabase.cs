using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.IntegrationTests
{
    public class ExemplaryDatabase
    {
        public ExemplaryDatabase() 
        {
            ResetDatabase();
            CreateTestDatabase();
        }

        private void ResetDatabase()
        {
            using var db = new FootballLeagueContext();
            db.Goals.RemoveRange(db.Goals.Select(g => g));
            db.Matches.RemoveRange(db.Matches.Select(m => m));
            db.Players.RemoveRange(db.Players.Select(p => p));
            db.Clubs.RemoveRange(db.Clubs.Select(c => c));

            db.SaveChanges();
        }

        public void CreateTestDatabase()
        {
            using var db = new FootballLeagueContext();
            Random rand = new Random();

            // Create 4 clubs
            for (int i = 1; i <= 4; i++)
            {
                var club = new Club
                {
                    ClubName = "Club" + i,
                    StadiumName = "Stadium" + i
                };

                db.Clubs.Add(club);
            }
            db.SaveChanges();

            // For each clubs add 11 players
            foreach (var c in db.Clubs.Select(c => c).Where(c => c.ClubName.Contains("Club")).ToList())
            {
                for(int i = 1; i <= 11; i++)
                {
                    var player = new Player
                    {
                        FirstName = c.ClubName + "name" + i,
                        LastName = c.ClubName + "lastname" + i,
                        Pesel = $"{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}",
                        ShirtNumber = i,
                        Position = "position" + i,
                        ClubId = c.IdClub
                    };

                    db.Players.Add(player);
                }
                db.SaveChanges();
            }
        }
    }
}
