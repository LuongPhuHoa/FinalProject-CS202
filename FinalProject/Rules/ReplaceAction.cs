﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Rules
{
    public class ReplaceAction : IRenameRules
    {
        public string name => "Replace action";

        public StringProcessor Processor => _replace;

        public StringArgs Args { get; set; }

        private string _replace(string origin)
        {
            var myArgs = Args as ReplaceActionArguments;
            var needle = myArgs.Needle;
            var hammer = myArgs.Hammer;

            string res = origin.Replace(needle, hammer);

            return res;
        }
        public void ShowEditDialog()
        {
            var screen = new ReplaceActionDialog(Args as ReplaceActionArguments);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as ReplaceActionArguments;
                myArgs.Needle = screen.Needle;
                myArgs.Hammer = screen.Hammer;
            }
        }

        IRenameRules IRenameRules.Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceActionArguments()
            };
        }
    }
}
