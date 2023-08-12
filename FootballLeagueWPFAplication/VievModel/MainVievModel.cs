using FootballLeagueLib.Model;
using FootballLeagueLib.Table;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueWPFAplication.VievModel
{
    public class MainVievModel : INotifyPropertyChanged
    {
        private TableData _tableData;

        private List<(int, Club, int)> _tableStatistic;

        public MainVievModel() 
        {
            _tableData = new TableData();
            TableStatistic = _tableData.Table;
        }

        public List<(int, Club, int)> TableStatistic
        {
            get { return _tableStatistic; }
            set
            {
                _tableStatistic = value;
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
