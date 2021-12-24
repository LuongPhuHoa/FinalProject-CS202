using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    class ReplaceAction : IRenameRules
    {
        public string Name => "Replace action";

        public StringProcessor Processor => Transform;

        public IStringArgs Args { get; set; }

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
                myArgs.Needle += screen.Needle;
                myArgs.Hammer += screen.Hammer;
            }
        }

        private string Transform(string origin)
        {
            var myArgs = (ReplaceActionArguments)Args;
            var needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            
            string res = origin.Replace(needle, hammer);
            
            return res;
        }

        
    }
}
