using System;
using System.Collections.Generic;
using FootballLeagueLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueLib;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clubs> Clubs { get; set; }

    public virtual DbSet<Goals> Goals { get; set; }

    public virtual DbSet<Matches> Matches { get; set; }

    public virtual DbSet<Players> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Polish_CI_AS");

        modelBuilder.Entity<Clubs>(entity =>
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

        modelBuilder.Entity<Goals>(entity =>
        {
            entity.HasKey(e => e.IdGoal);

            entity.HasOne(d => d.IdMatchNavigation).WithMany(p => p.Goals)
                .HasForeignKey(d => d.IdMatch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shoot");

            entity.HasOne(d => d.Id).WithMany(p => p.Goals)
                .HasForeignKey(d => new { d.IdPlayer, d.IdClub })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shoot by");
        });

        modelBuilder.Entity<Matches>(entity =>
        {
            entity.HasKey(e => e.IdMatch);

            entity.Property(e => e.AwayTeam).HasMaxLength(30);
            entity.Property(e => e.GoalsAwayTeam).HasDefaultValueSql("((0))");
            entity.Property(e => e.GoalsHomeTeam).HasDefaultValueSql("((0))");
            entity.Property(e => e.HomeTeam).HasMaxLength(30);
            entity.Property(e => e.MatchDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.MatchName).HasMaxLength(50);
            entity.Property(e => e.Result)
                .HasMaxLength(20)
                .HasDefaultValueSql("('0-0')");

            entity.HasOne(d => d.IdAwayTeamNavigation).WithMany(p => p.MatchesIdAwayTeamNavigation)
                .HasForeignKey(d => d.IdAwayTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlayAwayTeam");

            entity.HasOne(d => d.IdHomeTeamNavigation).WithMany(p => p.MatchesIdHomeTeamNavigation)
                .HasForeignKey(d => d.IdHomeTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlayHomeTeam");
        });

        modelBuilder.Entity<Players>(entity =>
        {
            entity.HasKey(e => new { e.IdPlayer, e.IdClub });

            entity.Property(e => e.IdPlayer).ValueGeneratedOnAdd();
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

            entity.HasOne(d => d.IdClubNavigation).WithMany(p => p.Players)
                .HasForeignKey(d => d.IdClub)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlayFor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
