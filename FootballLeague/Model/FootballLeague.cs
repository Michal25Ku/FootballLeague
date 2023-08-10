using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueLib.Model
{
    public class FootballLeague : DbContext
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "FootballLeague.mdf");
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mkuci\source\repos\FootballLeague\FootballLeague\FootballLeague.mdf;Integrated Security=True");
        }
    }
}
