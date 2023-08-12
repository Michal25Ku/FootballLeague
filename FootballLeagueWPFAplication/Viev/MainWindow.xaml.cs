﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FootballLeagueLib.DataForWPF;
using FootballLeagueLib.Model;
using FootballLeagueLib.Season;
using FootballLeagueWPFAplication.VievModel;

namespace FootballLeagueWPFAplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ClubData> ClubList { get; set; }
        public List<Tuple<string, string, string>> MatchList { get; set; }
        public SeasonPlayRound season { get; }
        public SeasonManager SeasonManager { get; }
        public string MatchRoundBtnContent { get; set; }
        public int MatchRound { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ResetDatabase();
            DataContext = new MainVievModel();

            //UpdateClubStatistic();

            MatchRound = 1;
            MatchRoundBtnContent = $"Play {MatchRound} round";
            SeasonManager = new SeasonManager();
            //season = new SeasonPlayRound();
        }

        void ResetDatabase()
        {
            using var db = new FootballLeague();

            db.Goals.RemoveRange(db.Goals.Select(g => g));
            db.Matches.RemoveRange(db.Matches.Select(m => m));

            foreach (var club in db.Clubs)
            {
                club.Wins = 0;
                club.Draws = 0;
                club.Failures = 0;
                club.GoalsScored = 0;
                club.GoalsConceded = 0;
                club.GoalBalance = 0;
                club.Points = 0;
            }

            foreach(var player in db.Players)
            {
                player.GoalsScored = 0;
            }

            db.SaveChanges();
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            //if (clubHeader.Visibility == Visibility.Collapsed && lvEntriesTable.Visibility == Visibility.Collapsed)
            //{
            //    clubHeader.Visibility = Visibility.Visible;
            //    lvEntriesTable.Visibility = Visibility.Visible;

            //    lvEntriesMatches.Visibility = Visibility.Collapsed;
            //}
        }

        //private void UpdateClubStatistic()
        //{
        //    using var db = new FootballLeague();

        //    ClubList = db.Clubs.Select(c => new ClubData
        //    {
        //        ClubName = c.ClubName,
        //        MatchCount = db.Matches.Count(m => m.IdHomeTeam == c.IdClub || m.IdAwayTeam == c.IdClub) / 2,
        //        Wins = c.Wins,
        //        Draws = c.Draws,
        //        Failures = c.Failures,
        //        GoalScored = c.GoalsScored,
        //        GoalsConceded = c.GoalsConceded,
        //        GoalsBalance = c.GoalBalance,
        //        Point = c.Points
        //    }).ToList();

        //    lvEntriesTable.ItemsSource = ClubList;

        //    ICollectionView view = CollectionViewSource.GetDefaultView(lvEntriesTable.ItemsSource);
        //    view.Refresh();
        //}

        //private void UpdateMatches()
        //{
        //    using var db = new FootballLeague();

        //    MatchList = db.Matches.Select(m => new Tuple<string,string,string>(m.HomeTeam, m.Result, m.AwayTeam)).ToList();

        //    lvEntriesMatches.ItemsSource = MatchList;

        //    ICollectionView view = CollectionViewSource.GetDefaultView(lvEntriesMatches.ItemsSource);
        //    view.Refresh();
        //}

        private void PlayMatchBtn_Click(object sender, RoutedEventArgs e)
        {
            //season.StartMatch();
            //UpdateMatches();
            //UpdateClubStatistic();
        }

        private void MatchesBtn_Click(object sender, RoutedEventArgs e)
        {
            //if (clubHeader.Visibility == Visibility.Visible && lvEntriesTable.Visibility == Visibility.Visible)
            //{
            //    clubHeader.Visibility = Visibility.Collapsed;
            //    lvEntriesTable.Visibility = Visibility.Collapsed;

            //    lvEntriesMatches.Visibility = Visibility.Visible;
            //    UpdateMatches();
            //}
        }
    }
}