﻿using FootballLeagueLib.Season;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootballLeagueWPFAplication.VievModel
{
    public partial class MainVievModel
    {
        public ICommand PlayRoundCommand { get; set; }
        public ICommand ShowMatchesCommand { get; set; }
        public ICommand ShowTableCommand { get; set; }
        public ICommand ShowStatisticCommand { get; set; }
        public ICommand ShowClubMatchesCommand { get; set; }

        private void PlayRound(object obj)
        {
            _seasonManager.PlayRound();

            TableStatistic = _tableData.UpdateTable();
        }

        private void ShowMatches(object obj)
        {
            MatchesContent = new List<MatchContentVievModel>();
            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.Add(new MatchContentVievModel(m));
            }

            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;
            StatisticVisibility = Visibility.Collapsed;
        }

        private void ShowTable(object obj)
        {
            TableStatistic = _tableData.UpdateTable();

            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;
            StatisticVisibility = Visibility.Collapsed;
        }

        private void ShowStatistic(object obj)
        {
            TopScorerList = _topScorer.TopScorers();

            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Collapsed;
            StatisticVisibility = Visibility.Visible;
        }

        private void ShowClubMatches(object obj)
        {
            if (SelectedClubStatistic != null)
            {
                MatchesContent = new List<MatchContentVievModel>();
                foreach (var m in _matchesData.UpdateMatchesForOneClub(SelectedClubStatistic.Item2))
                {
                    MatchesContent.Add(new MatchContentVievModel(m));
                }
            }

            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;
            StatisticVisibility = Visibility.Collapsed;
        }
    }
}
