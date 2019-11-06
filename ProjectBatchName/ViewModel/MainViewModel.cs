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
    public class MainViewModel : BaseViewModel
    {
        #region Properties
        public bool Isloaded = false;
        private readonly IFileService fileService;
        private readonly ILogic logic;
        private readonly IFolderService folderService;

        private ObservableCollection<fileInfo> _fileInfoList;
        public ObservableCollection<fileInfo> fileInfoList
        {
            get => _fileInfoList;
            set { _fileInfoList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<folderInfo> _folderInfoList;
        public ObservableCollection<folderInfo> folderInfoList
        {
            get => _folderInfoList;
            set { _folderInfoList = value; OnPropertyChanged(); }
        }
        private fileInfo _SelectedItem;
        public fileInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {

                }
            }
        }
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor MainViewModel
        /// </summary>
        public MainViewModel()
        {
            logic = Logic.Logic.Instance;
            fileService = Services.File.FileService.Instance;
            folderService = Services.Folder.FolderService.Instance;
            if (!Isloaded)
            {
                Isloaded = true;
                //MessageBox.Show("Developed by MVVM");
            }
            fileInfoList = new ObservableCollection<fileInfo>();
            folderInfoList = new ObservableCollection<folderInfo>();
        }
        #endregion

        #region Command
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
                    folderInfoList.Add(new folderInfo()
                    {
                        Foldername = dir.Substring(directory.Length + 1),
                        Path = dir
                    });
                }
            }
        }
        #endregion
        #endregion

    }
}
