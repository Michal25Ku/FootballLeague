﻿using FootballLeagueLib.Model;
using FootballLeagueLib.Season;

namespace TestConsoleAplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new FootballLeague();

            db.Goals.RemoveRange();
            db.Matches.RemoveRange();
            db.SaveChanges();

            Season seaon1 = new Season();
            //rozegrajMecz.StartMatch();

        }
    }
}