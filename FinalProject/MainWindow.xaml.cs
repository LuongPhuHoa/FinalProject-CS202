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
using FinalProject.Dialogs;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<FolderClass> folderList;
        ObservableCollection<FileClass> fileList;
        List<IRenameRules> methodList;
        ObservableCollection<Preset> presetList;
        string currentPresetFile = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            methodList = new List<IRenameRules>() {
                new CaseHandling(),
                new PrefixSurfixHandling(),
                new ReplaceAction(),
                new SpaceClean(),
                new AddCounter(),
            };
            actionCombobox.ItemsSource = methodList;
            presetList = new ObservableCollection<Preset>();
        }

        private void AddFolderButtons_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFolderDialog = new();

            if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] path = Directory.GetDirectories(openFolderDialog.SelectedPath, "*", System.IO.SearchOption.AllDirectories);
                foreach (var dir in path)
                {
                    FolderTab.Items.Add(new FolderClass()
                    {
                        FolderName = dir.Substring(openFolderDialog.SelectedPath.Length + 1),
                        FolderPath = dir
                    });
                }
            }
        }

        static public int fileCount = 0;
        private void AddFileButtons_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    FileTab.Items.Add(new FileClass()
                    {
                        FileName = System.IO.Path.GetFileName(file),
                        FilePath = file
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
            if (ActionListBox.Items.Count == 0)
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
                foreach (IRenameRules rule in ActionListBox.Items)
                {
                    result = rule.Processor.Invoke(result);
                    ObservableCollection<FileClass> temp = new(fileList);
                    temp.Remove(temp[i]);
                    foreach (FileClass f in temp)
                    {
                        if (result == f.FileName)
                        {
                            fileList[i].FileError = "Duplicate File Name.";
                            fileList[i].FileRename = result;

                        }
                    }
                    if (result == " ")
                    {
                        // a space mean there was an error when executed this method 
                        fileList[i].FileError = "Renaming file failed";
                        break;

                    }
                }
                // all done without error?
                if (fileList[i].FileError != "Duplicate File Name.")
                {
                    fileList[i].FileRename = result;
                    fileList[i].FileError = "Successful";
                }
                if(fileList[i].FileError== "Successful")
                {
                    string tempPath = fileList[i].FilePath.Replace(fileList[i].FileName, fileList[i].FileRename);
                    if (fileList[i].FilePath != tempPath)
                    {
                        File.Move(@fileList[i].FilePath, tempPath);
                    }
                }
                fileCount = 0;
            }
            

            // batching folder
            for (int i = 0; i < folderList.Count; i++)
            {
                string result = folderList[i].FolderName;
                string path = folderList[i].FolderPath;
                foreach (IRenameRules rule in ActionListBox.Items)
                {
                    result = rule.Processor.Invoke(result);
                    ObservableCollection<FolderClass> temp = new(folderList);
                    temp.Remove(temp[i]);
                    foreach (FolderClass f in temp)
                    {
                        if (result == f.FolderName)
                        {
                            folderList[i].FolderError = "Duplicate Folder Name.";
                            folderList[i].FolderRename = result;

                        }
                    }
                    if (result == " ")
                    {
                        // a space mean there was an error when executed this method 
                        folderList[i].FolderError = "Renaming folder failed";
                        break;

                    }
                }
                // all done without error?
                if (folderList[i].FolderError != "Duplicate Folder Name.")
                {
                    folderList[i].FolderRename = result;
                    folderList[i].FolderError = "Successful.";
                }
                if(folderList[i].FolderError== "Successful.")
                {
                    string tempPath = folderList[i].FolderPath.Replace(folderList[i].FolderName, folderList[i].FolderRename);
                    if (folderList[i].FolderPath != tempPath)
                    {
                        Directory.Move(@folderList[i].FolderPath, tempPath);
                    }
                }
                fileCount = 0;
            }
        }
        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {
            // Check if ActionListBox is empty
            if(ActionListBox.Items.Count==0)
            {
                MessageBox.Show("Nothing to save!");
                return;
            }
            //save method
            var presetScreen = new PresetDialog();
            if(presetScreen.ShowDialog()==true)
            {
                string presetName = presetScreen.presetName;

                // if presetfile is not null
                if (currentPresetFile != "")
                {
                    using (StreamWriter write = File.AppendText(currentPresetFile))
                    {
                        write.WriteLine(presetName);
                        
                        foreach(IRenameRules rule in ActionListBox.Items)
                        {
                            string temp = $"{rule.Name} {rule.Args.ParseArgs()}";
                            write.WriteLine(temp);
                        }
                        write.WriteLine("//");
                    }
                    MessageBox.Show($"Saved! Please check preset file in {currentPresetFile}");
                }
                else
                {
                    string presetFolderPath = @"C:\BatchRename";
                    string presetFilePath = @"C:\BatchRename\preset.txt";
                    if (!Directory.Exists(presetFolderPath)) 
                        Directory.CreateDirectory(presetFolderPath);
                    if(File.Exists(presetFilePath))
                    {
                        using (StreamWriter write = File.CreateText(presetFilePath))
                        {
                            write.WriteLine(presetName);

                            foreach (IRenameRules rule in ActionListBox.Items)
                            {
                                string temp = $"{rule.Name} {rule.Args.ParseArgs()}";
                                write.WriteLine(temp);
                            }
                            write.WriteLine("//");
                        }
                        MessageBox.Show($"Saved. Please check preset file in {presetFilePath}");
                    }
                    else
                    {
                        using (StreamWriter write = File.AppendText(presetFilePath))
                        {
                            write.WriteLine(presetName);

                            foreach (IRenameRules rule in ActionListBox.Items)
                            {
                                string temp = $"{rule.Name} {rule.Args.ParseArgs()}";
                                write.WriteLine(temp);
                            }
                            write.WriteLine("//");
                        }
                        MessageBox.Show($"Saved. Please check preset file in {presetFilePath}");
                    }
                }
            }
        }

        private void LoadPreset_Click(object sender, RoutedEventArgs e)
        {
            var loadMethod = new System.Windows.Forms.OpenFileDialog();
            if (loadMethod.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                presetList = new ObservableCollection<Preset>();
                //clear listbox if it's not null
                ActionListBox.Items.Clear();
                string presetFilePath = loadMethod.FileName;

                //set file path
                currentPresetFile = presetFilePath;
                // start reading 
                using (StreamReader read = new StreamReader(presetFilePath))
                {
                    string currentLine;
                    while ((currentLine = read.ReadLine()) != null)
                    {
                        string presetname = currentLine;
                        ObservableCollection<IRenameRules> temp = new ObservableCollection<IRenameRules>();
                        while ((currentLine = read.ReadLine()) != "//")
                        {
                            if(currentLine.Contains("Adding Counter"))
                            {
                                temp.Add(new AddCounter() { Args = new AddCounterArg() });
                            }
                            else if (currentLine.Contains("Case Handling"))
                            {
                                temp.Add(new CaseHandling() { Args = new CaseArgs(currentLine) });
                            }
                            else if (currentLine.Contains("Replace action"))
                            {
                                temp.Add(new ReplaceAction() { Args = new ReplaceActionArguments(currentLine) });
                            }
                            else if (currentLine.Contains("Cleaning spaces"))
                            {
                                temp.Add(new SpaceClean() { Args = new SpaceArg() });
                            }
                            else if (currentLine.Contains("Adding prefix/surfix"))
                            {
                                temp.Add(new PrefixSurfixHandling() { Args = new PrefixSurfixArg(currentLine) });
                            }
                        }
                        presetList.Add(new Preset()
                        {
                            Name = presetname,
                            presetItems = temp
                        });
                    }
                }
            }
            PresetCombobox.ItemsSource = presetList;
        }

        private void PresetCombobox_DropDownClosed(object sender, EventArgs e)
        {
            if (PresetCombobox.SelectedIndex == -1) return;
            // clear methodListBox
            ActionListBox.Items.Clear();
            ActionListBox.ItemsSource = null;
            object selected = PresetCombobox.SelectedItem;
            foreach (IRenameRules rule in (selected as Preset).presetItems)
            {
                ActionListBox.Items.Add(rule);
            }
        }
    }
}
