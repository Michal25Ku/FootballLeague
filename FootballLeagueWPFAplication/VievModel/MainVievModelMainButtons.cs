﻿using System;
using System.Collections.Generic;
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

        private void PlayRound(object obj)
        {
            _seasonManager.PlayRound(_seasonManager.ActualRound);

            TableStatistic = _tableData.UpdateTable();

            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.FirstOrDefault(x => x.MatchData.IdMatch == m.IdMatch).MatchData = m;
            }
        }

        private void ShowMatches(object obj)
        {
            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;

            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.FirstOrDefault(x => x.MatchData.IdMatch == m.IdMatch).MatchData = m;
            }
        }

        private void ShowTable(object obj)
        {
            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;
        }
    }
}
