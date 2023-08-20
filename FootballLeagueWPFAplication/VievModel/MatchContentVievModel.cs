using FootballLeagueLib.DataForWPF;
using FootballLeagueLib.Entities;
using FootballLeagueLib.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueWPFAplication.VievModel
{
    public class MatchContentVievModel : INotifyPropertyChanged
    {
        private ScorerListForMatch _scoredListForMatch;

        public MatchContentVievModel(Match match)
        {
            _scoredListForMatch = new ScorerListForMatch();
            MatchData = match;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
