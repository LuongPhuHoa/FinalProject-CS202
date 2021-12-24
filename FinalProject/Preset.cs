using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Preset
    {
        public string Name { get; set; }
        public ObservableCollection<IRenameRules> presetItems = null;
    }
}
