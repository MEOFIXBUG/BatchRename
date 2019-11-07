using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using ProjectBatchName.Model;
using System.Windows.Input;
using ProjectBatchName.Logic;
using ProjectBatchName.Services.File;
using ProjectBatchName.Services.Folder;
using System.IO;

namespace ProjectBatchName.ViewModel
{
    public sealed class MainViewModel : BaseViewModel
    {
        #region Properties
        #region flag

        public bool Isloaded = false;

        #endregion

        #region interface

        private readonly IFileService fileService;
        private readonly ILogic logic;
        private readonly IFolderService folderService;

        #endregion

        #region Data
        #region file
        private ObservableCollection<fileInfo> _fileInfoList;
        public ObservableCollection<fileInfo> fileInfoList
        {
            get => _fileInfoList;
            set
            {
                _fileInfoList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region folder
        private ObservableCollection<folderInfo> _folderInfoList;
        public ObservableCollection<folderInfo> folderInfoList
        {
            get => _folderInfoList;
            set
            {
                _folderInfoList = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region operation
        #endregion
        #endregion

        #region Selected

        private fileInfo _SelectedFile;
        public fileInfo SelectedFile
        {
            get => _SelectedFile;
            set
            {
                _SelectedFile = value;
                OnPropertyChanged();
            }
        }

        private folderInfo _SelectedFolder;
        public folderInfo SelectedFolder
        {
            get => _SelectedFolder;
            set
            {
                _SelectedFolder = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Contructor

        /// <summary>
        /// Constructor MainViewModel
        /// </summary>
        public MainViewModel()
        {
            if (!Isloaded)
            {
                //MessageBox.Show("Developed by MVVM");
                Isloaded = true;
                logic = Logic.Logic.Instance;
                fileService = Services.File.FileService.Instance;
                folderService = Services.Folder.FolderService.Instance;
                fileInfoList = new ObservableCollection<fileInfo>();
                folderInfoList = new ObservableCollection<folderInfo>();
               
            }
        }

        #endregion

        #region Command

        #region File Command
        #region AddFileCommand
        private ICommand _addFileCommand;
        public ICommand AddFilesCommand
        {
            get
            {
                return _addFileCommand ??
                     (_addFileCommand = new RelayCommand<object>(
                         (p) => CanExecuteAddFileCommand(),
                            (p) => ExecuteAddFileCommand()));
            }
        }
        private bool CanExecuteAddFileCommand()
        {
            return true;
        }

        private void ExecuteAddFileCommand()
        {
            var screen = new System.Windows.Forms.FolderBrowserDialog();
            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string directory = screen.SelectedPath;
                var Listfile = Directory.GetFiles(directory);
                foreach (var file in Listfile)
                {
                    var temp = new fileInfo();
                    temp.Filename = Path.GetFileName(file);
                    //temp.Filename = System.IO.Path.GetFileName(file);
                    temp.Path = file;
                    if (fileService.IsExist(fileInfoList, temp) >= 0) //should use static
                    {
                        System.Windows.MessageBox.Show("Existed file");
                    }
                    else
                    {
                        fileInfoList.Add(temp);
                    }
                }
            }
        }
        #endregion

        #region DeleteFileCommand
        private ICommand _delFileCommand;

        public ICommand DelFileCommand
        {
            get
            {
                return _delFileCommand ??
                     (_delFileCommand = new RelayCommand<object>(
                         (p) => CanExecuteDelFileCommand(),
                            (p) => ExecuteDelFileCommand()));
            }
        }

        private bool CanExecuteDelFileCommand()
        {
            return (SelectedFile != null);
        }

        private void ExecuteDelFileCommand()
        {
            fileInfoList.Remove(SelectedFile);
        }
        #endregion
        #endregion

        #region Folder Command
        #region AddForderCommand
        private ICommand _addFolderCommand;
        public ICommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand ??
                     (_addFolderCommand = new RelayCommand<object>(
                         (p) => CanExecuteAddFolderCommand(),
                            (p) => ExecuteAddFolderCommand()));
            }
        }
        private bool CanExecuteAddFolderCommand()
        {
            return true;
        }

        private void ExecuteAddFolderCommand()
        {
            string directory;
            var screen = new System.Windows.Forms.FolderBrowserDialog();
            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                directory = screen.SelectedPath;
                string[] subDirectory = Directory.GetDirectories(directory);

                foreach (var dir in subDirectory)
                {
                    var temp = new folderInfo();
                    temp.Foldername = dir.Substring(directory.Length + 1);
                    temp.Path = dir;
                    if (folderService.IsExist(folderInfoList, temp) >= 0)
                    {
                        System.Windows.MessageBox.Show("Existed folder");
                    }
                    else
                    {
                        folderInfoList.Add(temp);
                    }

                }
            }
        }
        #endregion

        #region DeleteFolderCommand
        private ICommand _delFolderCommand;

        public ICommand DelFolderCommand
        {
            get
            {
                return _delFolderCommand ??
                     (_delFolderCommand = new RelayCommand<object>(
                         (p) => CanExecuteDelFolderCommand(),
                            (p) => ExecuteDelFolderCommand()));
            }
        }

        private bool CanExecuteDelFolderCommand()
        {
            return (SelectedFolder != null);
        }

        private void ExecuteDelFolderCommand()
        {
            folderInfoList.Remove(SelectedFolder);
        }
        #endregion
        #endregion

        #endregion

    }
}
