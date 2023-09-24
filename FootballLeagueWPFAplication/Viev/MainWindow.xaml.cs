using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
using FootballLeagueLib.Entities;
using FootballLeagueLib.NewLeague;
using FootballLeagueWPFAplication.VievModel;

namespace FootballLeagueWPFAplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ResetSeason _resetSeason;

        public MainWindow()
        {
            InitializeComponent();
            _resetSeason = new ResetSeason(); 
            DataContext = new MainVievModel();
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
