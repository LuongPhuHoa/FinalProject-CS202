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
    /// Interaction logic for PrefixSurfixDialog.xaml
    /// </summary>
    public partial class PrefixSurfixDialog : Window
    {
        public string current;
        public string content;
        public int choice;

        public PrefixSurfixDialog(PrefixSurfix arg)
        {
            InitializeComponent();
            current = arg.Type;
            choice = arg.Choice;
            content = arg.Content;
            if (current == "prefix") prefixBtn.IsChecked = true;
            else if (current == "surfix") surfixBtn.IsChecked = true;
            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (prefixBtn.IsChecked == true)
            {
                current = "prefix";
                choice = 0;
                content = contentBox.Text;
                this.DialogResult = true;
            }
            else if (surfixBtn.IsChecked == true)
            {
                current = "surfix";
                choice = 1;
                content = contentBox.Text;
                this.DialogResult = true;
            }
            else
            {
                choice = 2;
                MessageBox.Show("Chose your option!");
            }
            Content = contentBox.Text;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
