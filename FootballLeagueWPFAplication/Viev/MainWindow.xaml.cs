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
using FootballLeagueLib.DataForWPF;
using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using FootballLeagueWPFAplication.VievModel;

namespace FootballLeagueWPFAplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResetDatabase(); 
            DataContext = new MainVievModel();
        }

        void ResetDatabase()
        {
            using var db = new FootballLeagueContext();
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
                club.Players.Clear();
                club.MatchesAwayTeam.Clear();
                club.MatchesHomeTeam.Clear();
            }

            foreach (var player in db.Players)
            {
                player.GoalsScored = 0;
                player.Goals.Clear();
            }

            db.SaveChanges();
        }

        private void ClubStatisticListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainVievModel viewModel)
            {
                viewModel.ShowClubMatchesCommand.Execute(null);
            }
        }
    }
}
