using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string CaseType;
        public int Choice;

        public CaseDialog(CaseArgs args)
        {
            InitializeComponent();

            if (CaseType == "upper") upperBtn.IsChecked = true;
            else if (CaseType == "lower") lowerBtn.IsChecked = true;
            else pascalBtn.IsChecked = true;

            CaseType = args.CaseType;   
            Choice = args.Choice;
        }
     
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (upperBtn.IsChecked == true)
            {
                CaseType = "upper";
                Choice = 0;
                this.DialogResult = true;
            }
            else if (lowerBtn.IsChecked == true)
            {
                CaseType = "lower";
                Choice = 1;
                this.DialogResult = true;
            }
            else if (pascalBtn.IsChecked == true)
            {
                CaseType = "pascal";
                Choice = 2;
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
