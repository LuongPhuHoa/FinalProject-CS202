using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class ReplaceActionArguments : IStringArgs, INotifyPropertyChanged
    {
        private string _needle;
        private string _hammer;

        public string Needle
        {
            get => _needle; set
            {
                _needle = value;
                NotifyChanged("Needle");
                NotifyChanged("_needle");
                NotifyChanged("Details");
            }
        }

        public string Hammer
        {
            get => _hammer; set
            {
                _hammer = value;
                NotifyChanged("Hammer");
                NotifyChanged("_hammer");
                NotifyChanged("Details");
            }
        }

        public string Details => $"Replace {Needle} with {Hammer}";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
