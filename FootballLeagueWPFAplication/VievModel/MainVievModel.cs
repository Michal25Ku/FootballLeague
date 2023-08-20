using FootballLeagueLib.DataForWPF;
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

        private SeasonManager _seasonManager;

        public MainVievModel()
        {
            //using var db = new FootballLeagueContext();
            _seasonManager = new SeasonManager();

            _tableData = new TableData();
            TableStatistic = _tableData.Table;

            _matchesData = new MatchesData();
            MatchesContent = new List<MatchContentVievModel>();

            foreach (var m in _matchesData.MatchesList)
            {
                MatchesContent.Add(new MatchContentVievModel(m));
            }

            PlayRoundCommand = new RelayCommand(PlayRound);
            ShowMatchesCommand = new RelayCommand(ShowMatches);
            ShowTableCommand = new RelayCommand(ShowTable);

            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;

            //var clubs = db.Clubs.ToList();

            //for (int i = 0; i < clubs.Count(); i++)
            //{
            //    var c = db.Clubs.FirstOrDefault(c => c.IdClub == clubs[i].IdClub);

            //    foreach (var p in db.Players)
            //    {
            //        if (c.IdClub == p.ClubId)
            //        {
            //            c.Players.Add(p);
            //        }
            //    }
            //}

            //db.SaveChanges();
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
