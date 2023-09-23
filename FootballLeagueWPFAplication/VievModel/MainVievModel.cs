using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using FootballLeagueLib.Table;
using FootballLeagueWPFAplication.Commands;
using FootballLeagueWPFAplication.Viev;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeagueWPFAplication.VievModel
{
    public partial class MainVievModel : INotifyPropertyChanged
    {
        private TableData _tableData;
        private MatchesData _matchesData;
        private TopScorer _topScorer;
        private NewSeasonRulesWindow _newSeasonRulesWindow;

        public static SeasonManager SeasonManager { get; set; }
        public MainVievModel()
        {
            MatchesContent = new List<MatchContentVievModel>();

            _tableData = new TableData();
            TableStatistic = _tableData.Table;
            _matchesData = new MatchesData();
            _topScorer = new TopScorer();

            PlayRoundCommand = new RelayCommand(PlayRound);
            ShowMatchesCommand = new RelayCommand(ShowMatches);
            ShowTableCommand = new RelayCommand(ShowTable);
            ShowStatisticCommand = new RelayCommand(ShowStatistic);
            ShowClubMatchesCommand = new RelayCommand(ShowClubMatches);
            CreateNewLeagueCommand = new RelayCommand(CreateNewLeague);

            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;
            StatisticVisibility = Visibility.Collapsed;
        }

        private List<MatchContentVievModel> _matchesContent;
        public List<MatchContentVievModel> MatchesContent
        {
            get { return _matchesContent; }
            set
            {
                _matchesContent = value;
                OnPropertyChanged();
            }
        }

        private Tuple<int, Club, int> _selectedClubStatistic;
        public Tuple<int, Club, int> SelectedClubStatistic
        {
            get { return _selectedClubStatistic; }
            set
            {
                _selectedClubStatistic = value;
                OnPropertyChanged();
            }
        }

        private List<Tuple<int, Club, int>> _tableStatistic;
        public List<Tuple<int, Club, int>> TableStatistic
        {
            get { return _tableStatistic; }
            set
            {
                _tableStatistic = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
