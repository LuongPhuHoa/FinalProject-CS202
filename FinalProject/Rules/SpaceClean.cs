using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FinalProject.Rules
{
    public class SpaceClean : IRenameRules
    {
        public string name => "Cleaning spaces";

        public StringArgs? Args { get; set; }

        public StringProcessor Processor => _transform;

        private string _transform(string origin)
        {
            string result = Regex.Replace(origin, @"\s", "");
            return result;
        }
        public IRenameRules Clone()
        {
            return new SpaceClean();
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
