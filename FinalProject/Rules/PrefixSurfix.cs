using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class PrefixSurfix : StringArgs, INotifyPropertyChanged
    {
        private string _type; //prefix, surfix

        public string Details => $"Adding {_type}";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ParseArgs()
        {
            return _type;
        }
        public int GetType()
        {
            if (_type == "prefix")
                return 1;
            else if (_type == "surfix")
                return 2;
            return 3;
        }

        public string Type
        {
            get => _type; set
            {
                _type = value;
                NotifyChange("Type");
                NotifyChange("Details");
            }
        }

        private void NotifyChange(string newEvent)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newEvent));
            }
        }

        public class Handling : IRenameRules
        {
            public string name => "Adding prefix/surfix";

            public StringArgs? Args { get; set; }

            public StringProcessor Processor => _transform;

            private string _transform(string origin)
            {
                return origin;
            }

            public IRenameRules Clone()
            {
                return new Handling()
                {
                    Args = new PrefixSurfix()
                };
            }

            public void ShowEditDialog()
            {
                throw new NotImplementedException();
            }
        }
    }
}
