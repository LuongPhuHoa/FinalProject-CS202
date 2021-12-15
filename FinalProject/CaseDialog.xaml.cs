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
    /// Interaction logic for CaseDialog.xaml
    /// </summary>
    public partial class CaseDialog : Window
    {
        public string oldCase;
        public string currentCase;
        public CaseDialog(CaseArgs args)
        {
            InitializeComponent();
            currentCase = args.CaseType;
            if (currentCase == "upper") upperBtn.IsChecked = true;
            else if (currentCase == "lower") lowerBtn.IsChecked = true;
            else pascalBtn.IsChecked = true;

        }
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (upperBtn.IsChecked == true)
            {
                currentCase = "upper";
                this.DialogResult = true;
            }
            else if (lowerBtn.IsChecked == true)
            {
                currentCase = "lower";
                this.DialogResult = true;
            }
            else if (pascalBtn.IsChecked == true)
            {
                currentCase = "pascal";
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Chose your case option!");
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
