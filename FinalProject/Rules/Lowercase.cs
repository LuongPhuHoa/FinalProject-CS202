using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class Lowercase : IRenameRules
    {
        public string name => "Lowercase";

        public StringProcessor Processor => _lowercase;

        public string _lowercase(string origin)
        {
            string res = origin.ToLower();
            return res;
        }
    }
}
