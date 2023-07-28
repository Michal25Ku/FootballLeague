using FootballLeagueLib.Model;
using FootballLeagueLib.PlayMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FootballLeagueLib.DataForWPF
{
    public class Season
    {
        public static int actualMatch = 0;
        public int RoundCount => (Db.Clubs.Count() - 1) * 2;
        public int ClubCount => Db.Clubs.Count();
        FootballLeague Db { get; }
        public List<MatchTracking> Matches { get; private set; }
        public List<Club> Clubs { get; private set; }

        public Season()
        {
            Db = new FootballLeague();
            Matches = new List<MatchTracking>();
            Clubs = Db.Clubs.ToList();

            GenerateAllMatches();
        }

        public void GenerateAllMatches()
        {
            Stack<Club> clubsQueue = new Stack<Club>();
            List<Tuple<int, int>> coupleClubs = new List<Tuple<int, int>>();

            for (int round = 0; round < RoundCount; round++)
            {
                for(int club = 0; club < ClubCount; club++)
                {
                    clubsQueue.Push(Clubs[club]);
                }

                foreach (var c in clubsQueue)
                    Console.WriteLine(c.ClubName);

                Console.WriteLine("-----------");

                Clubs.RemoveRange(0, Clubs.Count);

                for (int i = 0; i < ClubCount; i += 2)
                {
                    int homeTeamId = clubsQueue.Pop().IdClub;
                    int awayTeamId = clubsQueue.Pop().IdClub;

                    Tuple<int, int> couple = new Tuple<int, int>(homeTeamId, awayTeamId);

                    if(!coupleClubs.Contains(couple))
                    {
                        Matches.Add(new MatchTracking(1000, new Model.Match(homeTeamId, awayTeamId, DateTime.Now)));
                    }
                    else
                    {
                        Matches.Add(new MatchTracking(1000, new Model.Match(awayTeamId, homeTeamId, DateTime.Now)));
                    }

                    coupleClubs.Add(couple);
                }

                if (round % 2 == 0)
                {
                    for (int i = Matches.Count - ClubCount / 2; i < Matches.Count; i++)
                    {
                        Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item2 == c.IdClub));
                    }

                    for (int i = Matches.Count - ClubCount/2; i < Matches.Count; i++)
                    {
                        Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item1 == c.IdClub));
                    }
                }
                else
                {
                    for (int i = Matches.Count - ClubCount / 2; i < Matches.Count; i++)
                    {
                        if(i % 2 == 0)
                        {
                            Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item1 == c.IdClub));
                        }
                        else
                        {
                            Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item2 == c.IdClub));
                        }
                    }

                    for (int i = Matches.Count - 1; i >= Matches.Count - ClubCount / 2; i--)
                    {
                        if (i % 2 != 0)
                        {
                            Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item1 == c.IdClub));
                        }
                        else
                        {
                            Clubs.Add(Db.Clubs.FirstOrDefault(c => coupleClubs[i].Item2 == c.IdClub));
                        }
                    }
                }
            }

            

            foreach (var p in Matches)
            {
                Console.WriteLine(p.PlayedMatch.HomeTeam + "-" + p.PlayedMatch.AwayTeam);
            }
        }

        public void StartMatch()
        {
            Matches[actualMatch].StartMatch();
            actualMatch++;
        }
    }
}
