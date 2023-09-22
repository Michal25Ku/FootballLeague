using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeagueWPFAplication.VievModel
{
    class NewSeasonRuleVievModel : INotifyPropertyChanged
    {
        public NewSeasonRuleVievModel()
        {

        }

        private string _numericInput;
        public string NumericInput
        {
            get { return _numericInput; }
            set
            {
                if (IsNumeric(value))
                {
                    _numericInput = value;
                    OnPropertyChanged(nameof(NumericInput));
                }
            }
        }

        private bool IsNumeric(string text) => int.TryParse(text, out var value);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
