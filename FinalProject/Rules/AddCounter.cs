using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    class AddCounter : IRenameRules
    {
        public string Name => "Adding Counter";

        public StringProcessor Processor => Transform;

        public IStringArgs Args { get; set; }

        public IRenameRules Clone()
        {
            return new AddCounter();
        }

        public void ShowEditDialog()
        {
            throw new NotImplementedException();
        }

        static public int FileCount = MainWindow.fileCount;
        public string Transform(string origin)
        {
            int dotIndex = origin.LastIndexOf(".");
            string ext = origin.Substring(dotIndex);
            string name = origin.Substring(0, dotIndex);
            return $"{name} ({FileCount}){ext}";
        }
    }
}
