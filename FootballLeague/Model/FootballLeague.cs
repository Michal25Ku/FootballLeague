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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>(eb =>
            {
                eb.Property(c => c.ClubName).HasMaxLength(50).IsRequired();
                eb.Property(c => c.StadiumName).HasMaxLength(50).IsRequired();
                eb.Property(c => c.GoalsScored).HasDefaultValue(0);
                eb.Property(c => c.GoalsConceded).HasDefaultValue(0);
                eb.Property(c => c.GoalBalance).HasDefaultValue(0);
                eb.Property(c => c.Wins).HasDefaultValue(0);
                eb.Property(c => c.Draws).HasDefaultValue(0);
                eb.Property(c => c.Failures).HasDefaultValue(0);
                eb.Property(c => c.Points).HasDefaultValue(0);
            });

            modelBuilder.Entity<Player>(eb =>
            {
                eb.Property(p => p.FirstName).HasMaxLength(30).IsRequired();
                eb.Property(p => p.LastName).HasMaxLength(30).IsRequired();
                eb.Property(p => p.Position).HasMaxLength(30).IsRequired();
                eb.Property(p => p.GoalsScored).HasDefaultValue(0);
            });

            modelBuilder.Entity<Match>(eb =>
            {
                eb.Property(m => m.HomeTeam).HasMaxLength(30).IsRequired();
                eb.Property(m => m.AwayTeam).HasMaxLength(30).IsRequired();
                eb.Property(m => m.MatchName).HasMaxLength(60).IsRequired();
                eb.Property(m => m.MatchDate).HasDefaultValueSql("getdate()");
                eb.Property(m => m.GoalsHomeTeam).HasDefaultValue(0);
                eb.Property(m => m.GoalsAwayTeam).HasDefaultValue(0);
                eb.Property(m => m.Result).HasMaxLength(20).HasDefaultValue("0 - 0");
                eb.Property(m => m.IsPlayed).HasDefaultValue(false);
                eb.Property(m => m.Round).HasDefaultValue(0);
            });
        }
    }
}
