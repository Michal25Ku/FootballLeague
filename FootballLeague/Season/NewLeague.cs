using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Season
{
    public class NewLeague
    {
        public void CreateNewLeague()
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
                for (int i = 1; i <= 11; i++)
                {
                    int randomFN = new Random().Next(0, 19);
                    int randomLN = new Random().Next(0, 19);
                    RandomFirstName firstName = (RandomFirstName)randomFN;
                    RandomLastName lastName = (RandomLastName)randomFN;

                    var player = new Player
                    {
                        FirstName = firstName.ToString(),
                        LastName = lastName.ToString(),
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
