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

        public PrefixSurfixDialog(PrefixSurfix arg)
        {
            InitializeComponent();
            current = arg.Type;
            if (current == "prefix") prefixBtn.IsChecked = true;
            else if (current == "surfix") surfixBtn.IsChecked = true;
            contentBox.Text = arg.Content;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (prefixBtn.IsChecked == true)
            {
                current = "prefix";
                
                this.DialogResult = true;
            }
            else if (surfixBtn.IsChecked == true)
            {
                current = "surfix";
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Chose your option!");
            }
            Content = contentBox.Text;
            this.DialogResult = true;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
