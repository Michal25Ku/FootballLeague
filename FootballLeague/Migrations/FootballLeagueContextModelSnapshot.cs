﻿// <auto-generated />
using System;
using FootballLeagueLib.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FootballLeagueLib.Migrations
{
    [DbContext(typeof(FootballLeagueContext))]
    partial class FootballLeagueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Polish_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FootballLeagueLib.Entities.Club", b =>
                {
                    b.Property<int>("IdClub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClub"));

                    b.Property<string>("ClubName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Draws")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("Failures")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("GoalBalance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("GoalsConceded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("GoalsScored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("Points")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("StadiumName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Wins")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("IdClub");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Goal", b =>
                {
                    b.Property<int>("IdGoal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGoal"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("MinuteOfTheMatch")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("IdGoal");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Match", b =>
                {
                    b.Property<int>("IdMatch")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMatch"));

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<string>("AwayTeamName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("GoalsAwayTeam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("GoalsHomeTeam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<string>("HomeTeamName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsPlayed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("MatchDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("MatchName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Result")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue(" - ");

                    b.Property<int?>("Round")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("IdMatch");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Player", b =>
                {
                    b.Property<int>("IdPlayer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPlayer"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("GoalsScored")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)")
                        .HasColumnName("PESEL");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("ShirtNumber")
                        .HasColumnType("int");

                    b.HasKey("IdPlayer");

                    b.HasIndex("ClubId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Goal", b =>
                {
                    b.HasOne("FootballLeagueLib.Entities.Match", "Match")
                        .WithMany("Goals")
                        .HasForeignKey("MatchId")
                        .IsRequired()
                        .HasConstraintName("Shoot");

                    b.HasOne("FootballLeagueLib.Entities.Player", "Player")
                        .WithMany("Goals")
                        .HasForeignKey("PlayerId")
                        .IsRequired()
                        .HasConstraintName("Shoot by");

                    b.Navigation("Match");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Match", b =>
                {
                    b.HasOne("FootballLeagueLib.Entities.Club", "AwayTeam")
                        .WithMany("MatchesAwayTeam")
                        .HasForeignKey("AwayTeamId")
                        .IsRequired()
                        .HasConstraintName("PlayAwayTeam");

                    b.HasOne("FootballLeagueLib.Entities.Club", "HomeTeam")
                        .WithMany("MatchesHomeTeam")
                        .HasForeignKey("HomeTeamId")
                        .IsRequired()
                        .HasConstraintName("PlayHomeTeam");

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Player", b =>
                {
                    b.HasOne("FootballLeagueLib.Entities.Club", "Club")
                        .WithMany("Players")
                        .HasForeignKey("ClubId")
                        .IsRequired()
                        .HasConstraintName("PlayFor");

                    b.Navigation("Club");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Club", b =>
                {
                    b.Navigation("MatchesAwayTeam");

                    b.Navigation("MatchesHomeTeam");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Match", b =>
                {
                    b.Navigation("Goals");
                });

            modelBuilder.Entity("FootballLeagueLib.Entities.Player", b =>
                {
                    b.Navigation("Goals");
                });
#pragma warning restore 612, 618
        }
    }
}
