using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FootballLeagueWPFAplication.VievModel
{
    public partial class MainVievModel
    {
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
    }
}
