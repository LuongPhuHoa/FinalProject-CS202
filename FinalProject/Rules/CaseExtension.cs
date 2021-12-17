using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class CaseArgs : StringArgs, INotifyPropertyChanged
    {
        private string _caseType; //  upper, lower , pascal 

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Details => $"Casing filename to {_caseType}";

        public string ParseArgs()
        {
            return _caseType;
        }
        public int GetCaseType()
        {
            if (_caseType == "upper")
                return 1;
            else if (_caseType == "lower")
                return 2;
            return 3;
        }
        public string CaseType
        {
            get => _caseType; set
            {
                _caseType = value;
                NotifyChange("CaseType");
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
    }

    public class CaseHandling : IRenameRules
    {
        public string name => "Case Handling";

        public StringArgs? Args { get; set; }
        private string _transform(string origin)
        {
            var caseArgs = Args as CaseArgs;
            int _caseType = caseArgs.GetCaseType();
            if (_caseType == 1)
            {
                return origin.ToUpper();
            }
            else if (_caseType == 2)
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

        StringProcessor IRenameRules.Processor => _transform;

        public IRenameRules Clone()
        {
            return new CaseHandling()
            {
                Args = new CaseArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new CaseDialog(Args as CaseArgs);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = Args as CaseArgs;

                caseArgs.CaseType = screen.currentCase;
            }
        }
    }
}
