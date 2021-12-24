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
            int dotIndex = origin.LastIndexOf(".");
            string ext = origin.Substring(dotIndex);
            string name = origin.Substring(0, dotIndex);
            string result = Regex.Replace(name, @"\s", "") + ext;
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
