using FootballLeagueLib.Season;
using FootballLeagueWPFAplication.Commands;
using FootballLeagueWPFAplication.Viev;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootballLeagueWPFAplication.VievModel
{
    public delegate void NewSeason();

    class NewSeasonRuleVievModel : INotifyPropertyChanged
    {
        public event NewSeason NewSeasonCreate;

        public NewSeasonRuleVievModel()
        {
            CreateNewLeagueCommand = new RelayCommand(CreateNewLeague);
        }

        private string _clubsNumber;
        public string ClubsNumber
        {
            get { return _clubsNumber; }
            set
            {
                if (IsNumeric(value))
                {
                    _clubsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _relegatedClubsNumner;
        public string RelegatedClubsNumner
        {
            get { return _relegatedClubsNumner; }
            set
            {
                if (IsNumeric(value))
                {
                    _relegatedClubsNumner = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewLeagueCommand { get; set; }

        public void CreateNewLeague(object obj)
        {
            if(IsRuleCorrect())
            {
                NewLeague newLeague = new NewLeague();
                newLeague.CreateNewLeague(new SeasonRules(int.Parse(ClubsNumber), int.Parse(RelegatedClubsNumner)));
                MainVievModel.SeasonManager = new SeasonManager();
                NewSeasonCreate.Invoke();
                NewSeasonRulesWindow newSeasonRulesWindow = new NewSeasonRulesWindow();
                newSeasonRulesWindow.Close();
            }
        }

        private bool IsRuleCorrect()
        {
            if (ClubsNumber is null || RelegatedClubsNumner is null)
            {
                ErrorMessage = "Not entered any data!";
                return false;
            }

            if (int.Parse(ClubsNumber) <= int.Parse(RelegatedClubsNumner))
            {
                ErrorMessage = "Number of relegated clubs is more than number of clubs!";
                return false;
            }

            if (int.Parse(ClubsNumber) < 3 || int.Parse(RelegatedClubsNumner) <= 0)
            {
                ErrorMessage = "This league is nonsense!";
                return false;
            }

            if (int.Parse(ClubsNumber) > 20)
            {
                ErrorMessage = "This league is too large!";
                return false;
            }

            return true;
        }

        private bool IsNumeric(string text) => string.IsNullOrEmpty(text) || text.All(char.IsDigit);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
