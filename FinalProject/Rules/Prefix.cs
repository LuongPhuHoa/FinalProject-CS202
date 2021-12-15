using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class Prefix : IRenameRules
    {
        public string name => "Add Prefix";

        public StringProcessor Processor => _addPrefix;

        public string? GetPrefix { get; set; }

        public string _addPrefix(string origin)
        {
            string res = GetPrefix + origin;
            return res;
        }

    }
}
