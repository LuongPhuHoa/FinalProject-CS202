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
        public string Type { get; set; }
        public int Choice { get; set; }
        public string Content { get; set; }

        private void NotifyChange(string newEvent)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newEvent));
            }
        }

        public string Details => $"Adding {Type} : {Content}";

        public event PropertyChangedEventHandler? PropertyChanged;
    }


    public class PrefixSurfixHandling : IRenameRules
    {
        public string name => "Adding prefix/surfix";

        public StringArgs Args { get; set; }

        public StringProcessor Processor => Transform;

        private string Transform(string origin)
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
