using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class Uppercase : IRenameRules
    {
        public string name => "Uppercase";

        public StringProcessor Processor => _uppercase;

        /*public string Details => throw new NotImplementedException();

        public IRenameRules Clone()
        {
            throw new NotImplementedException();
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }*/

        public string _uppercase(string origin)
        {
            string res = origin.ToUpper();
            return res;
        }
    }
}
