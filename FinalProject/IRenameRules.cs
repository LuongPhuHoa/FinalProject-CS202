using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public delegate string StringProcessor(string origin);
    public interface StringArgs {
        string Details { get; }
        string ParseArgs();
    }
    public interface IRenameRules
    {
        string name { get; }
        StringProcessor Processor { get; }
        IRenameRules Clone();
        void ShowEditDialog();
    }
}
