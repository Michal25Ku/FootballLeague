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
    public class MainVievModel : INotifyPropertyChanged
    {
        private TableData _tableData;
        private MatchesData _matchesData;

        private SeasonManager _seasonManager;

        public MainVievModel()
        {
            using var db = new FootballLeagueContext();
            _seasonManager = new SeasonManager();

            _tableData = new TableData();
            TableStatistic = _tableData.Table;

            _matchesData = new MatchesData();

            PlayRoundCommand = new RelayCommand(PlayRound);
            ShowMatchesCommand = new RelayCommand(ShowMatches);
            ShowTableCommand = new RelayCommand(ShowTable);

            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;

            var clubs = db.Clubs.ToList();

            for (int i = 0; i < clubs.Count(); i++)
            {
                var c = db.Clubs.FirstOrDefault(c => c.IdClub == clubs[i].IdClub);

                foreach (var p in db.Players)
                {
                    if (c.IdClub == p.ClubId)
                    {
                        c.Players.Add(p);
                    }
                }
            }

            db.SaveChanges();
        }

        private Visibility _tableVisibility;
        public Visibility TableVisibility
        {
            get { return _tableVisibility; }
            set 
            {
                _tableVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _matchesVisibility;
        public Visibility MatchesVisibility
        {
            get { return _matchesVisibility; }
            set
            {
                _matchesVisibility = value;
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

        private List<Match> _matchesContent;
        public List<Match> MatchesContent
        {
            get { return _matchesContent; }
            set
            {
                _matchesContent = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayRoundCommand { get; set; }
        public ICommand ShowMatchesCommand { get; set; }
        public ICommand ShowTableCommand { get; set; }

        private void PlayRound(object obj)
        {
            _seasonManager.PlayRound(_seasonManager.ActualRound);

            TableStatistic = _tableData.UpdateTable();
            MatchesContent = _matchesData.UpdateMatchesList();
        }

        private void ShowMatches(object obj)
        {
            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;

            MatchesContent = _matchesData.MatchesList;
        }

        private void ShowTable(object obj)
        {
            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
