using FootballLeagueLib.Entities;
using FootballLeagueLib.Season;
using FootballLeagueWPFAplication.Viev;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Match = FootballLeagueLib.Entities.Match;

namespace FootballLeagueWPFAplication.VievModel
{
    public partial class MainVievModel
    {
        public ICommand PlayRoundCommand { get; set; }
        public ICommand ShowMatchesCommand { get; set; }
        public ICommand ShowTableCommand { get; set; }
        public ICommand ShowStatisticCommand { get; set; }
        public ICommand ShowClubMatchesCommand { get; set; }
        public ICommand CreateNewLeagueCommand { get; set; }

        private void PlayRound(object obj)
        {
            if(!MatchContentVievModel.IsPlayNow && MatchesContent.Count > 0)
            {
                SeasonManager.PlayRound();

                TableStatistic = _tableData.UpdateTable();
            }
        }

        private void ShowMatches(object obj)
        {
            MatchesContent.ForEach(m => m.ShowMatch());
            List<Match> updateList = _matchesData.UpdateMatchesList();
            for (int i = 0; i < MatchesContent.Count; i++)
            {
                MatchesContent[i].MatchData = updateList[i];
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
                foreach (var match in _matchesData.UpdateMatchesForOneClub(SelectedClubStatistic.Item2))
                {
                    MatchesContent.FirstOrDefault(m => m.MatchData.IdMatch == match.IdMatch).HideMatch();
                }
            }

            TableVisibility = Visibility.Collapsed;
            MatchesVisibility = Visibility.Visible;
            StatisticVisibility = Visibility.Collapsed;
        }

        private void CreateNewLeague(object obj)
        {
            NewSeasonRuleVievModel newSeasonRuleViewModel = new NewSeasonRuleVievModel();
            newSeasonRuleViewModel.NewSeasonCreate += CreateAllMatches;
            _newSeasonRulesWindow = new NewSeasonRulesWindow();
            _newSeasonRulesWindow.DataContext = newSeasonRuleViewModel;
            _newSeasonRulesWindow.ShowDialog();
        }

        void CreateAllMatches()
        {
            _newSeasonRulesWindow.Close();
            TableStatistic = _tableData.UpdateTable();
            MatchesContent = new List<MatchContentVievModel>();
            foreach (var m in _matchesData.UpdateMatchesList())
            {
                MatchesContent.Add(new MatchContentVievModel(m));
            }
        }
    }
}
