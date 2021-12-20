using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class PrefixSurfix : IStringArgs, INotifyPropertyChanged
    {
        private int _choice;
        private string _type;
        private string _content;
        public string Type
        {
            get => _type; 
            set 
            { 
                _type = value;
                NotifyChanged("Type");
                NotifyChanged("_type");
                NotifyChanged("Details");
            }
        }
        public int Choice
        {
            get => _choice;
            set
            {
                _choice = value;
                NotifyChanged("Choice");
                NotifyChanged("_choice");
                NotifyChanged("Details");
            }
        }
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                NotifyChanged("Content");
                NotifyChanged("_content");
                NotifyChanged("Details");
            }
        }
        public string Details => $"Adding {Type} : {Content}";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChanged(string newEvent)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newEvent));
            }
        }
    }


    class PrefixSurfixHandling : IRenameRules
    {
        public string Name => "Adding prefix/surfix";

        public IStringArgs Args { get; set; }

        public StringProcessor Processor => Transform;

        public string Transform(string origin)
        {
            var option = (PrefixSurfix)Args;
            var content = option.Content;
            int _caseType = option.Choice;
            string result = "";
            if (_caseType == 1)
            {
                result = result + content + origin;
                return result;
            }
            else if (_caseType == 2)
            {
                result = result + origin + content;
                return result;
            }
            else return origin;
        }

        public IRenameRules Clone()
        {
            return new PrefixSurfixHandling()
            {
                Args = new PrefixSurfix()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new PrefixSurfixDialog((PrefixSurfix)Args);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = (PrefixSurfix)Args;
                caseArgs.Content = screen.content;
                caseArgs.Type = screen.current;
                caseArgs.Choice = screen.choice;
            }
        }
    }
}
