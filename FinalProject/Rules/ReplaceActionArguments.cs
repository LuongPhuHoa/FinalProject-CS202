using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class ReplaceActionArguments : StringArgs, INotifyPropertyChanged
    {
        public string _needle;
        public string _hammer;

        public string Needle
        {
            get => _needle; set
            {
                _needle = value;
                NotifyChanged("Needle");
                NotifyChanged("Details");
            }
        }

        public string Hammer
        {
            get => _hammer; set
            {
                _hammer = value;
                NotifyChanged("Hammer");
                NotifyChanged("Details");
            }
        }

        public string Details => $"Replace {Needle} with {Hammer}";

        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string ParseArgs()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
