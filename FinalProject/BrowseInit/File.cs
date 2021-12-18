using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BrowseInit
{
    public class FileClass : INotifyPropertyChanged
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileRename { get; set; }
        public string? FileError { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyChange(string origin)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(origin));
            }
        }
        public FileClass Clone()
        {
            return new FileClass()
            {
                FileName = this.FileName,
                FilePath = this.FilePath,
                FileRename = this.FileRename,
                FileError = this.FileError
            };
        }

    }

    public class FolderClass
    {
        public string? FolderName { get; set; }
        public string? FolderPath { get; set; }
        public string? FolderRename { get; set; }
        public string? FolderError { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyChange(string origin)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(origin));
            }
        }
        public FolderClass Clone()
        {
            return new FolderClass()
            {
                FolderName = this.FolderName,
                FolderPath = this.FolderPath,
                FolderRename = this.FolderRename,
                FolderError = this.FolderError
            };
        }
    }
}
