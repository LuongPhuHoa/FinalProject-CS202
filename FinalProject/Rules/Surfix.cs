using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class Surfix : IRenameRules
    {
        public string name => "Add Surfix";

        public StringProcessor Processor => _addSurfix;

        public string? GetSurfix { get; set; }

        public string _addSurfix(string origin)
        {
            string res = GetSurfix + origin;
            return res;
        }
    }
}
