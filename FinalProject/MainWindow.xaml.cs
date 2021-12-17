using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using Path = System.IO.Path;
using System.Collections.ObjectModel;
using FinalProject.Rules;
using FinalProject.Class;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FolderClass> folderList = null;
        ObservableCollection<FileClass> fileList = null;
        ObservableCollection<IRenameRules> methodList = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadPreset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFolderButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string[] path = Directory.GetDirectories(openFolderDialog.SelectedPath);
                foreach (var dir in path)
                {
                    FolderTab.Items.Add(new FolderClass()
                    {
                        FolderName = new DirectoryInfo(dir).Name,
                        FolderPath = dir
                    });
                }
            }
        }

        private void AddFileButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] path = Directory.GetFiles(openFileDialog.SelectedPath);
                foreach (string item in path)
                {
                    FileTab.Items.Add(new FileClass()
                    {
                        FileName = Path.GetFileName(item),
                        FilePath = item
                    });
                }
            }
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            var method = ActionListBox.SelectedItem as IRenameRules;
            method.ShowEditDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartBatchButtonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddMethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (actionCombobox.SelectedItem == null)
            {
                MessageBox.Show("No method selected to add ?");
                return;
            }
            var methodSelected = actionCombobox.SelectedItem as IRenameRules;
            var instance = methodSelected.Clone();

            ActionListBox.Items.Add(instance);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            methodList = new ObservableCollection<IRenameRules>() {
                new CaseHandling(),
                new PrefixSurfixHandling(),
                new ReplaceAction(),
            };
            actionCombobox.ItemsSource = methodList;
        }
    }
}
