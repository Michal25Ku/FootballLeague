using FootballLeagueLib.Season;
using System;
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
        public ICommand ShowStatisticCommand { get; set; }
        public ICommand ShowClubMatchesCommand { get; set; }

        private void PlayRound(object obj)
        {
            _seasonManager.PlayRound();

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
            StatisticVisibility = Visibility.Collapsed;

            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.FirstOrDefault(x => x.MatchData.IdMatch == m.IdMatch).MatchData = m;
            }
        }

        private void ShowTable(object obj)
        {
            TableVisibility = Visibility.Visible;
            MatchesVisibility = Visibility.Collapsed;
            StatisticVisibility = Visibility.Collapsed;

            TableStatistic = _tableData.UpdateTable();
        }

        private void ShowStatistic(object obj)
        {
            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Collapsed;
            StatisticVisibility = Visibility.Visible;

            TopScorerList = _topScorer.TopScorers();
        }

        private void ShowClubMatches(object obj)
        {
            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;
            StatisticVisibility = Visibility.Collapsed;

            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.FirstOrDefault(x => x.MatchData.IdMatch == m.IdMatch).MatchData = m;
            }
        }
    }
}
