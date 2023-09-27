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

        /// <summary>
        /// On initialize class, resets data in the database
        /// </summary>
        public NewLeague() 
        {
            _resetSeason.ResetDatabase();
        }

        /// <summary>
        /// When the season rules are set, creates a new season.
        /// Creates as much as it is set in the "SeasonRules" parameter. Each club sets its name by a random value from the RandomClubNames enum, 
        /// and sets the stadium name as "'club name' stadium".
        /// Each club creates 11 players. Each player sets the first and last names by random values from the RandomFirstName and RandomLastName enums.
        /// Pesel has 11 random numbers from 0 to 9.
        /// The player's position set value from the PlayerPosition enum depends on the shirt number.
        /// Each club and player is added to the database
        /// </summary>
        /// <param name="seasonRules">SeasonRule object which have number of clubs and number of relegated club</param>
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
