using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class PrefixSurfixArg: IStringArgs, INotifyPropertyChanged
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public string ParseArgs()
        {
            return $"{Type} {Content}";
        }

        private void NotifyChanged(string newEvent)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newEvent));
            }
        }
        public PrefixSurfixArg() { }
        public PrefixSurfixArg(string details)
        {
            string[] word = details.Split(' ');
            Type = word[2];
            Content = word[3];
        }
    }


    public class PrefixSurfixHandling : IRenameRules
    {
        public string Name => "Adding prefix/surfix";

        public IStringArgs Args { get; set; }

        public StringProcessor Processor => Transform;

        public string Transform(string origin)
        {
            var option = (PrefixSurfixArg)Args;
            var content = option.Content;
            int _caseType = option.Choice;
            int dotIndex = origin.LastIndexOf(".");
            string ext = origin.Substring(dotIndex);
            string name = origin.Substring(0, dotIndex);
            string result = "";
            if (_caseType == 0)
            {
                result = result + content + origin;
                return result;
            }
            else if (_caseType == 1)
            {
                result = result + name + content + ext;
                return result;
            }
            else return origin;
        }

        public IRenameRules Clone()
        {
            return new PrefixSurfixHandling()
            {
                Args = new PrefixSurfixArg()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new PrefixSurfixDialog((PrefixSurfixArg)Args);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = (PrefixSurfixArg)Args;
                caseArgs.Content = screen.content;
                caseArgs.Type = screen.current;
                caseArgs.Choice = screen.choice;
            }
        }
    }
}
