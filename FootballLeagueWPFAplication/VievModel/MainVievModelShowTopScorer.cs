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
        private List<Tuple<int, Player, Club>> _topScorerList;
        public List<Tuple<int, Player, Club>> TopScorerList
        {
            get { return _topScorerList; }
            set
            {
                _topScorerList = value;
                OnPropertyChanged();
            }
        }
    }
}
