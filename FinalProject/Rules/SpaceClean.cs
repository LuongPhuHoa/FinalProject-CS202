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
        public string Name => "Cleaning spaces";

        public IStringArgs Args { get; set; }

        public StringProcessor Processor => Transform;

        public string Transform(string origin)
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
