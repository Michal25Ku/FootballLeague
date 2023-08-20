using FootballLeagueLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueWPFAplication.VievModel
{
    public partial class MainVievModel
    {
        private List<Tuple<int, Club, int>> _tableStatistic;
        public List<Tuple<int, Club, int>> TableStatistic
        {
            get { return _tableStatistic; }
            set
            {
                _tableStatistic = value;
                OnPropertyChanged();
            }
        }
    }
}
