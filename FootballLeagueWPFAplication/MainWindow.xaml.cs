using System;
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
using FootballLeagueLib;
using FootballLeagueLib.Model;

namespace FootballLeagueWPFAplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ClubData> ClubList { get; set; }
        public Season season { get; }
        private string _matchRoundBtnContent;
        public string MatchRoundBtnContent { get; set; }
        public int MatchRound { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ResetDatabase();

            UpdateClubStatistic();

            MatchRound = 1;
            MatchRoundBtnContent = $"Play {MatchRound} round";
            season = new Season();
        }

        void ResetDatabase()
        {
            using var db = new FootballLeague();

            db.Goals.RemoveRange();
            db.Matches.RemoveRange();
            
            foreach(var club in db.Clubs)
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
            PlayedMatch nowyMecz = new PlayedMatch(1, 2);
            PlayedMatch nowyMecz1 = new PlayedMatch(1, 2);

            MatchTracking rozegrajMecz = new MatchTracking(50, nowyMecz1);
            rozegrajMecz.StartMatch();

            UpdateClubStatistic();
        }

        private void UpdateClubStatistic()
        {
            using var db = new FootballLeague();

            ClubList = db.Clubs.Select(c => new ClubData
            {
                ClubName = c.ClubName,
                MatchCount = db.Matches.Count(m => m.IdHomeTeam == c.IdClub || m.IdAwayTeam == c.IdClub) / 2,
                Wins = c.Wins,
                Draws = c.Draws,
                Failures = c.Failures,
                GoalScored = c.GoalsScored,
                GoalsConceded = c.GoalsConceded,
                GoalsBalance = c.GoalBalance,
                Point = c.Points
            }).ToList();

            lvEntries.ItemsSource = ClubList;

            ICollectionView view = CollectionViewSource.GetDefaultView(lvEntries.ItemsSource);
            view.Refresh();
        }

        private void PlayMatchBtn_Click(object sender, RoutedEventArgs e)
        {
            season.StartMatch();
            UpdateClubStatistic();
        }
    }
}
