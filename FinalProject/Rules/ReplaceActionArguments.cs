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
        public string Needle { get; set; }

        public string Hammer { get; set; }  

        public string Details => $"Replace {Needle} with {Hammer}";

        private void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
