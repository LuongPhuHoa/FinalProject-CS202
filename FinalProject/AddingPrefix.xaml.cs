using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FinalProject.Rules;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for AddingPrefixSurfix.xaml
    /// </summary>
    public partial class AddingPrefixSurfix : Window
    {
        public AddingPrefixSurfix()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newPre = new Prefix();
            newPre.GetPrefix = PrefixBlock.Text;
            this.Close();
        }
    }
}
