using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class CaseArgs : IStringArgs, INotifyPropertyChanged
    {
        private int _choice;
        private string _caseType;

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
        public string CaseType
        {
            get => _caseType; 
            set 
            { 
                _caseType = value;
                NotifyChanged("CaseType");
                NotifyChanged("_caseType");
                NotifyChanged("Details");
            }

        }

        public string Details => $"Casing filename to {CaseType}";

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged(string newEvent)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(newEvent));
            }
        }

        public string ParseArgs()
        {
            return CaseType;
        }
        public CaseArgs() { }
        public CaseArgs(string details)
        {
            string[] word = details.Split(' ');
            CaseType = word[2];
        }
    }

    class CaseHandling : IRenameRules
    {
        public string Name => "Case Handling";
        public StringProcessor Processor => Transform;
        public IStringArgs Args { get; set; }
        public IRenameRules Clone()
        {
            return new CaseHandling()
            {
                Args = new CaseArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new CaseDialog((CaseArgs)Args);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = (CaseArgs)Args;
                caseArgs.CaseType = screen.CaseType;
                caseArgs.Choice = screen.Choice;
            }
        }

        public string Transform(string origin)
        {
            var caseArgs = (CaseArgs)Args;
            var _caseType = caseArgs.Choice;
            if (_caseType == 0)
            {
                return origin.ToUpper();
            }
            else if (_caseType == 1)
            {
                return origin.ToLower();

            }
            else
            {
                var yourString = origin.ToLower().Replace("_", " ");
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                yourString = info.ToTitleCase(yourString).Replace(" ", string.Empty);
                return yourString;
            }
        }

        
    }
}
