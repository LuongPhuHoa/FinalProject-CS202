using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    class AddCounterArg : IStringArgs
    {
        public string Details => "Adding counter in name";

        public string ParseArgs()
        {
            return "";
        }
    }
    class AddCounter : IRenameRules
    {
        public string Name => "Adding Counter";

        public StringProcessor Processor => Transform;

        public IStringArgs Args { get; set; }

        public IRenameRules Clone()
        {
            return new AddCounter()
            {
                Args = new AddCounterArg()
            };
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        public int FileCount = MainWindow.fileCount;
        public string Transform(string origin)
        {

            FileCount++;
            if (origin.Contains("."))
            {
                int dotIndex = origin.LastIndexOf(".");
                string ext = origin.Substring(dotIndex);
                string name = origin.Substring(0, dotIndex);
                return $"{name} ({FileCount}){ext}";
            }
            else
            return $"{origin} ({FileCount})";
        }
    }
}
