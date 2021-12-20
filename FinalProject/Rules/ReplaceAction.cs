using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Rules;

namespace FinalProject.Rules
{
    public class ReplaceAction : IRenameRules
    {
        public string name => "Replace action";

        public StringProcessor Processor => Transfer;

        public StringArgs Args { get; set; }

        public IRenameRules Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceActionArguments()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new ReplaceActionDialog((ReplaceActionArguments)Args);

            if (screen.ShowDialog() == true)
            {
                var myArgs = (ReplaceActionArguments)Args;
                myArgs.Needle = screen.Needle;
                myArgs.Hammer = screen.Hammer;
            }
        }

        public string Transfer(string origin)
        {
            var myArgs = (ReplaceActionArguments)Args;
            string needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            
            string res = origin.Replace(needle, hammer);
            
            return res;
        }

        
    }
}
