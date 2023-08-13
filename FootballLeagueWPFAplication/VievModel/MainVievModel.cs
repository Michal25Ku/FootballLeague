using FootballLeagueLib.Model;
using FootballLeagueLib.Season;
using FootballLeagueLib.Table;
using FootballLeagueWPFAplication.Commands;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeagueWPFAplication.VievModel
{
    public class MainVievModel : INotifyPropertyChanged
    {
        private TableData _tableData;
        private List<Tuple<int, Club, int>> _tableStatistic;

        private SeasonManager _seasonManager;
        private SeasonPlayMatch _seasonPlayMatch;
        private List<Match> _matchList;

        public MainVievModel() 
        {
            using var db = new FootballLeague();
            _tableData = new TableData();
            TableStatistic = _tableData.Table;

            _seasonManager = new SeasonManager();

            _matchList = db.Matches.ToList();
            _seasonPlayMatch = new SeasonPlayMatch(_seasonManager);

            PlayMatchCommand = new RelayCommand(PlayMatch);
        }

        public List<Tuple<int, Club, int>> TableStatistic
        {
            get { return _tableStatistic; }
            set
            {
                _tableStatistic = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayMatchCommand { get; set; }

        private void PlayMatch(object obj)
        {
            _seasonPlayMatch.PlayMatch(_matchList.FirstOrDefault(m => !m.IsPlayed));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
