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
    public class SpaceArg : IStringArgs
    {
        public string Details => "Cleaning spaces in name";

        public string ParseArgs()
        {
            return "";
        }
    }
    public class SpaceClean : IRenameRules
    {
        public string Name => "Cleaning spaces";

        public IStringArgs Args { get; set; }

        public StringProcessor Processor => Transform;

        public string Transform(string origin)
        {
            string result;
            if(origin.Contains('.') )
            {
                int dotIndex = origin.LastIndexOf(".");
                string ext = origin.Substring(dotIndex);
                string name = origin.Substring(0, dotIndex);
                result = Regex.Replace(name, @"\s", "") + ext;
            }
            else
            {
                result = Regex.Replace(origin, @"\s", "");
            }
            return result;
        }
        public IRenameRules Clone()
        {
            return new SpaceClean()
            {
                Args = new SpaceArg()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }
    }
}
