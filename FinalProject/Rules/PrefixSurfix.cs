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
        public string _type; //prefix, surfix
        public string _content;

        public string Type
        {
            get => _type; set
            {
                _type = value;
                NotifyChange("Type");
                NotifyChange("Details");
            }
        }

        public string Content
        {
            get => _content;

            set
            {
                Content = value;
                NotifyChange("Content");
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

        public string Details => $"Adding {Type} : {Content}";

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
    }
    public class PrefixSurfixHandling : IRenameRules
    {
        public string name => "Adding prefix/surfix";

        public StringArgs? Args { get; set; }

        public StringProcessor Processor => _transform;

        private string _transform(string origin)
        {
            var option = Args as PrefixSurfix;
            var content = option.Content;
            int _caseType = option.GetType();
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
            var screen = new PrefixSurfixDialog(Args as PrefixSurfix);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = Args as PrefixSurfix;
                caseArgs.Content = screen.content;
                caseArgs.Type = screen.current;
            }
        }
    }
}
