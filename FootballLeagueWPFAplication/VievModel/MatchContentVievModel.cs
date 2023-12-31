﻿using FootballLeagueLib.Entities;
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
        public static bool IsPlayNow { get; private set; } = false;
        private ScorerListForMatch _scoredListForMatch;

        public MatchContentVievModel(Match match)
        {
            _scoredListForMatch = new ScorerListForMatch();
            MatchManager = SeasonManager.AllMatchesManager.FirstOrDefault(m => m.PlayedMatch.IdMatch == match.IdMatch);
            MatchData = match;
            MatchResult = match.Result;

            TimeVisibility = Visibility.Collapsed;
            PlayButtonVisibility = Visibility.Visible;
            MatchVisibility = Visibility.Visible;

            MatchManager.MatchResultChanged += OnMatchChanged;
            MatchManager.MatchTimeChanged += OnTimeChanged;
            MatchManager.MatchEndChanged += OnEndChanged;
            MatchManager.MatchStartChanged += OnStartChanged;

            PlayMatchCommand = new RelayCommand(PlayMatch);
        }

        public void OnMatchChanged(int minuteOfMatch, Player player, bool isHomeTeamShotGoal)
        {
            using var db = new FootballLeagueContext();
            _scoredListForMatch.UpdateScorerInMatch(minuteOfMatch, player, isHomeTeamShotGoal);
            HomeTeamScorerList = _scoredListForMatch.ScorerListForHomeTeam;
            AwayTeamScorerList = _scoredListForMatch.ScorerListForAwayTeam;

            MatchResult = db.Matches.FirstOrDefault(m => m.IdMatch == MatchData.IdMatch).Result;
        }

        public void OnTimeChanged()
        {
            TimeInMatchBar = MatchManager.MinuteInMatch;
        }

        public void OnEndChanged()
        {
            TimeVisibility = Visibility.Collapsed;
            IsPlayNow = false;
        }

        public void OnStartChanged()
        {
            MatchResult = "0 - 0";
            TimeVisibility = Visibility.Visible;
            PlayButtonVisibility = Visibility.Collapsed;
            IsPlayNow = true;
        }

        public void HideMatch() => MatchVisibility = Visibility.Collapsed;
        public void ShowMatch() => MatchVisibility = Visibility.Visible;

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

        private Visibility _playButtonVisibility;
        public Visibility PlayButtonVisibility
        {
            get { return _playButtonVisibility; }
            set
            {
                _playButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _matchVisibility;
        public Visibility MatchVisibility
        {
            get { return _matchVisibility; }
            set
            {
                _matchVisibility = value;
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
                OnPropertyChanged();
            }
        }

        private string _matchResult;
        public string MatchResult
        {
            get { return _matchResult; }
            set
            {
                _matchResult = value;
                OnPropertyChanged();
            }
        }

        private StringBuilder _homeTeamScorerList;
        public StringBuilder HomeTeamScorerList
        {
            get { return _homeTeamScorerList; }
            set
            {
                _homeTeamScorerList = value;
                OnPropertyChanged();
            }
        }

        private StringBuilder _awayTeamScorerList;
        public StringBuilder AwayTeamScorerList
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
            if(!MatchManager.PlayedMatch.IsPlayed && IsPlayNow == false)
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
