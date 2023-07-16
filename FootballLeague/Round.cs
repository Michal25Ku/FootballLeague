using FootballLeagueLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib
{
    public class Round
    {
        Random rand = new Random();
        public static int actualRound = 1;
        public int RoundCount => Db.Clubs.Count() * 2 - 2;
        FootballLeague Db { get; }
        public List<PlayedMatch> PlayedMatches { get; private set; }
        public List<Club> Clubs { get; private set; }

        public Round()
        {
            Db = new FootballLeague();
            PlayedMatches = new List<PlayedMatch>();
            Clubs = Db.Clubs.ToList();

            if(actualRound < RoundCount - 1)
            {
                for (int i = 0; i < RoundCount / 2; i++)
                {
                    for (int j = RoundCount - 1; j >= RoundCount / 2; j--)
                    {
                        PlayedMatches.Add(new PlayedMatch(Clubs[i].IdClub, Clubs[j].IdClub, DateTime.Now));
                    }
                }
                actualRound++;
            }
            else
            {
                for (int i = 0; i < RoundCount / 2; i++)
                {
                    for (int j = RoundCount - 1; j >= RoundCount / 2; j--)
                    {
                        PlayedMatches.Add(new PlayedMatch(Clubs[j].IdClub, Clubs[i].IdClub, DateTime.Now));
                    }
                }
                actualRound++;
            }
        }


    }
}
