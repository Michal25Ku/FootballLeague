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
            MatchManager = SeasonManager.AllMatchesManager.FirstOrDefault(m => m.PlayedMatch.IdMatch == match.IdMatch);
            MatchData = match;
            TimeVisibility = Visibility.Collapsed;

            MatchManager.MatchResultChanged += OnMatchChanged;
            MatchManager.MatchTimeChanged += OnTimeChanged;
            MatchManager.MatchEndChanged += OnEndChanged;
            MatchManager.MatchStartChanged += OnStartChanged;

            PlayMatchCommand = new RelayCommand(PlayMatch);
        }

        public void OnMatchChanged()
        {
            using var db = new FootballLeagueContext();
            MatchData = db.Matches.FirstOrDefault(m => m.IdMatch == MatchData.IdMatch);
        }

        public void OnTimeChanged()
        {
            TimeInMatchBar = MatchManager.MinuteInMatch;
        }

        public void OnEndChanged()
        {
            TimeVisibility = Visibility.Collapsed;
        }

        public void OnStartChanged()
        {
            TimeVisibility = Visibility.Visible;
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

        private MatchManager _matchManager;
        public MatchManager MatchManager
        {
            get { return _matchManager; }
            set 
            {
                _matchManager = value;
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
            if(!MatchManager.PlayedMatch.IsPlayed)
            {
                using var db = new FootballLeagueContext();
                MatchManager.StartMatch();
                MatchData = db.Matches.FirstOrDefault(m => m.IdMatch == MatchData.IdMatch);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
