using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Rules;

namespace FinalProject
{
    public delegate string StringProcessor(string origin);
    public interface IRenameRules
    {
        string Name { get; }
        StringProcessor Processor { get; }
        IStringArgs Args { get; set; }
        IRenameRules Clone();
        void ShowEditDialog();
    }
}
