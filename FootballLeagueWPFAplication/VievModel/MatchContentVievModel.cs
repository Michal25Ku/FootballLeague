using FootballLeagueLib.Entities;
using FootballLeagueLib.PlayMatch;
using FootballLeagueLib.Season;
using FootballLeagueLib.Table;
using FootballLeagueWPFAplication.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeagueWPFAplication.VievModel
{
    public class MatchContentVievModel : INotifyPropertyChanged
    {
        private ScorerListForMatch _scoredListForMatch;

        public MatchContentVievModel(Match match)
        {
            _scoredListForMatch = new ScorerListForMatch();
            SeasonPlayMatch = new SeasonPlayMatch(match);
            MatchData = match;
            TimeVisibility = Visibility.Collapsed;

            SeasonPlayMatch.MatchManager.MatchResultChanged += OnMatchChanged;
            SeasonPlayMatch.MatchManager.MatchTimeChanged += OnTimeChanged;
            SeasonPlayMatch.MatchManager.MatchEndChanged += OnEndChanged;

            PlayMatchCommand = new RelayCommand(PlayMatch);
        }

        public void OnMatchChanged()
        {
            using var db = new FootballLeagueContext();
            MatchData = db.Matches.FirstOrDefault(m => m.IdMatch == MatchData.IdMatch);
        }

        public void OnTimeChanged()
        {
            TimeInMatchBar = SeasonPlayMatch.MatchManager.TimeInMatch;
        }

        public void OnEndChanged()
        {
            TimeInMatchBar = SeasonPlayMatch.MatchManager.TimeInMatch;
            TimeVisibility = Visibility.Collapsed;
        }

        private Visibility _timeVisibility;
        public Visibility TimeVisibility
        {
            get { return _timeVisibility; }
            set
            {
                _timeVisibility = value;
                OnPropertyChanged();
            }
        }

        private SeasonPlayMatch _seasonPlayMatch;
        public SeasonPlayMatch SeasonPlayMatch
        {
            get { return _seasonPlayMatch; }
            set 
            {
                _seasonPlayMatch = value;
                OnPropertyChanged();
            }
        }

        private Match _matchData;
        public Match MatchData
        {
            get { return _matchData; }
            set
            {
                _matchData = value;

                HomeTeamScorerList = string.Join("\n", _scoredListForMatch.CreateScorerList(value, value.HomeTeamId).Select(s => $"{s.Item2.LastName} '{s.Item1}"));
                AwayTeamScorerList = string.Join("\n", _scoredListForMatch.CreateScorerList(value, value.AwayTeamId).Select(s => $"{s.Item2.LastName} '{s.Item1}"));
                OnPropertyChanged();
            }
        }

        private string _homeTeamScorerList;
        public string HomeTeamScorerList
        {
            get { return _homeTeamScorerList; }
            set
            {
                _homeTeamScorerList = value;
                OnPropertyChanged();
            }
        }

        private string _awayTeamScorerList;
        public string AwayTeamScorerList
        {
            get { return _awayTeamScorerList; }
            set
            {
                _awayTeamScorerList = value;
                OnPropertyChanged();
            }
        }

        private int _timeInMatchBar;
        public int TimeInMatchBar
        {
            get { return _timeInMatchBar; }
            set
            {
                _timeInMatchBar = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayMatchCommand { get; set; }
        private void PlayMatch(object obj)
        {
            TimeVisibility = Visibility.Visible;
            using var db = new FootballLeagueContext();
            _seasonPlayMatch.PlayMatch();

            MatchData = db.Matches.FirstOrDefault(m => m.IdMatch == MatchData.IdMatch);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
