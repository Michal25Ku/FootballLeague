using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;

namespace FootballLeagueLib.Entities
{
    public class FootballLeagueContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _modulePath = "FootballLeague";
            string _currentDirectory = Path.Combine(Environment.CurrentDirectory);

            for (int i = 0; i < 10; i++)
            {
                string filePath = Path.Combine(_currentDirectory, _modulePath);

                if (Directory.Exists(filePath)) break;

                _currentDirectory = Directory.GetParent(_currentDirectory)?.FullName;

                if (_currentDirectory == null) break;
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{_currentDirectory}\\appsettings.json", true, true)
                .AddEnvironmentVariables();

            string connectionStringName = "FootballLeagueConnectionString";

            var config = builder.Build().GetConnectionString(connectionStringName);
            string modifiedConnectionString = config.Replace("{_currentDirectory}", _currentDirectory);
            optionsBuilder
                .LogTo(Console.WriteLine, new[]
                { DbLoggerCategory.Database.Command.Name},
                LogLevel.Information)
                .EnableSensitiveDataLogging()
                .UseSqlServer(modifiedConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Polish_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.IdClub);

                entity.Property(e => e.ClubName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Draws).HasDefaultValueSql("((0))");
                entity.Property(e => e.Failures).HasDefaultValueSql("((0))");
                entity.Property(e => e.GoalBalance).HasDefaultValueSql("((0))");
                entity.Property(e => e.GoalsConceded).HasDefaultValueSql("((0))");
                entity.Property(e => e.GoalsScored).HasDefaultValueSql("((0))");
                entity.Property(e => e.Points).HasDefaultValueSql("((0))");
                entity.Property(e => e.StadiumName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Wins).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.IdGoal);

                entity.HasOne(d => d.Match).WithMany(p => p.Goals)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Shoot");

                entity.HasOne(d => d.Player).WithMany(p => p.Goals)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Shoot by");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.IdMatch);

                entity.Property(e => e.AwayTeamName).HasMaxLength(30);
                entity.Property(e => e.GoalsAwayTeam).HasDefaultValue(0);
                entity.Property(e => e.GoalsHomeTeam).HasDefaultValue(0);
                entity.Property(e => e.HomeTeamName).HasMaxLength(30);
                entity.Property(e => e.MatchDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("date");
                entity.Property(e => e.MatchName).HasMaxLength(60);
                entity.Property(e => e.Result)
                    .HasMaxLength(20)
                    .HasDefaultValue(" - ");
                entity.Property(e => e.IsPlayed).HasDefaultValue(false);
                entity.Property(e => e.Round).HasDefaultValue(0);

                entity.HasOne(d => d.AwayTeam).WithMany(p => p.MatchesAwayTeam)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlayAwayTeam");

                entity.HasOne(d => d.HomeTeam).WithMany(p => p.MatchesHomeTeam)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlayHomeTeam");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.IdPlayer);

                //entity.Property(e => e.IdPlayer).ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(e => e.GoalsScored).HasDefaultValueSql("((0))");
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("PESEL");
                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Club).WithMany(p => p.Players)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlayFor");
            });
        }
    }
}
