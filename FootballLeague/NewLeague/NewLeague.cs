using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.NewLeague
{
    public class NewLeague
    {
        ResetSeason _resetSeason = new ResetSeason();
        public void CreateNewLeague(SeasonRules seasonRules)
        {
            _resetSeason.ResetDatabase();
            using var db = new FootballLeagueContext();
            Random rand = new Random();

            RandomClubNames[] listClubName = (RandomClubNames[])Enum.GetValues(typeof(RandomClubNames));
            // Create 4 clubs
            for (int i = 1; i <= seasonRules.NumberOfClubs; i++)
            {
                var club = new Club
                {
                    ClubName = listClubName[i - 1].ToString(),
                    StadiumName = listClubName[i - 1].ToString() + " Stadium"
                };

                db.Clubs.Add(club);
            }
            db.SaveChanges();

            // For each clubs add 11 players
            foreach (var c in db.Clubs.Select(c => c).ToList())
            {
                PlayerPosition[] listPosition = (PlayerPosition[])Enum.GetValues(typeof(PlayerPosition));

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
                        Position = listPosition[i - 1].ToString(),
                        ClubId = c.IdClub
                    };

                    db.Players.Add(player);
                }
                db.SaveChanges();
            }
        }
    }
}
