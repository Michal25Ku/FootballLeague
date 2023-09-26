using EFCore.BulkExtensions;
using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.NewLeague
{
    public class NewLeague
    {
        ResetSeason _resetSeason = new ResetSeason();
        public NewLeague() 
        {
            _resetSeason.ResetDatabase();
        }

        public void CreateNewLeague(SeasonRules seasonRules)
        {
            using var db = new FootballLeagueContext();
            Random rand = new Random();
            var clubs = new List<Club>();
            var players = new List<Player>();

            RandomClubNames[] listClubName = (RandomClubNames[])Enum.GetValues(typeof(RandomClubNames));
            
            for (int i = 1; i <= seasonRules.NumberOfClubs; i++)
            {
                clubs.Add(new Club
                {
                    ClubName = listClubName[i - 1].ToString(),
                    StadiumName = listClubName[i - 1].ToString() + " Stadium"
                });
            }
            db.BulkInsert(clubs);

            // For each clubs add 11 players
            foreach (var c in db.Clubs.ToList())
            {
                PlayerPosition[] listPosition = (PlayerPosition[])Enum.GetValues(typeof(PlayerPosition));
                
                for (int i = 1; i <= 11; i++)
                {
                    int randomFN = rand.Next(0, 19);
                    int randomLN = rand.Next(0, 19);
                    RandomFirstName firstName = (RandomFirstName)randomFN;
                    RandomLastName lastName = (RandomLastName)randomFN;

                    players.Add(new Player
                    {
                        FirstName = firstName.ToString(),
                        LastName = lastName.ToString(),
                        Pesel = $"{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}{rand.Next(0, 9)}",
                        ShirtNumber = i,
                        Position = listPosition[i - 1].ToString(),
                        ClubId = c.IdClub
                    });
                    
                }
                db.BulkInsert(players);
                players = new List<Player>();
            }

            _resetSeason.AddConnectionsInDatabase();
        }
    }
}
