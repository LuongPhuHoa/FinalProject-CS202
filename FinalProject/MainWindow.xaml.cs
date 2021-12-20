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
using FinalProject.BrowseInit;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FolderClass> folderList;
        ObservableCollection<FileClass> fileList;
        ObservableCollection<IRenameRules> methodList;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            methodList = new ObservableCollection<IRenameRules>() {
                //new CaseHandling(),
                //new PrefixSurfixHandling(),
                new ReplaceAction(),
                new SpaceClean(),
            };
            actionCombobox.ItemsSource = methodList;
        }

        private void AddFolderButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFolderDialog = new();
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
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new();
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
            var method = (IRenameRules)ActionListBox.SelectedItem;
            method.ShowEditDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ActionListBox.ItemsSource = null;
            ActionListBox.Items.Clear();
            FileTab.ItemsSource = null;
            FileTab.Items.Clear();
            FolderTab.ItemsSource = null;
            FolderTab.Items.Clear();
            // clear source collections
            if (fileList != null) fileList.Clear();
            if (folderList != null) folderList.Clear();
        }

        private void AddMethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (actionCombobox.SelectedItem == null)
            {
                MessageBox.Show("No method selected to add ?");
                return;
            }
            var methodSelected = (IRenameRules)actionCombobox.SelectedItem;
            var instance = methodSelected.Clone();

            ActionListBox.Items.Add(instance);
        }

        private void StartBatchButtonButton_Click(object sender, RoutedEventArgs e)
        {

            folderList = new ObservableCollection<FolderClass>();
            fileList = new ObservableCollection<FileClass>();
            // Add item from fileTab/folderTab to fileList/folderList
            foreach (FileClass file in FileTab.Items) fileList.Add(file);
            foreach (FolderClass folder in FolderTab.Items) folderList.Add(folder);

            //No method is selected!
            if (methodList.Count == 0)
            {
                MessageBox.Show("Method box is empty!");
                return;
            }
            // No file or folder in fileTab/folderTab
            if (fileList.Count == 0 && folderList.Count == 0)
            {
                MessageBox.Show("No file/folder in List!");
                return;
            }


            // Batching file
            for (int i = 0; i < fileList.Count; i++)
            {
                string result = fileList[i].FileName;
                string path = fileList[i].FilePath;
                foreach (IRenameRules rule in actionCombobox.Items)
                {
                    result = rule.Processor.Invoke(result);
                    ObservableCollection<FileClass> temp = new(fileList);
                    temp.Remove(temp[i]);
                    fileList[i].FileRename = result;
                    foreach (FileClass file in temp)
                    {
                        if (result == file.FileName)
                        {
                            fileList[i].FileError = "dupplicate file name!";
                        }
                    }
                    if (result == " ")
                    {
                        fileList[i].FileError = "Error.";
                        break;
                    }
                    else if (fileList[i].FileError != "Duplicate file name!" && fileList[i].FileError != "Error"!)
                    {
                        fileList[i].FileError = "Ok.";
                    }
                }
            }
        }
    }
}
