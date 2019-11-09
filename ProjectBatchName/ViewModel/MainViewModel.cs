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
using System.ComponentModel;
using ProjectBatchName.Common;

namespace ProjectBatchName.ViewModel
{
    public class MainViewModel : BaseViewModel
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
                OnPropertyChanged("fileInfoList");
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
                OnPropertyChanged("folderInfoList");
            }
        }
        #endregion

        #region operation
        public ObservableCollection<StringOperation> operationCollection { get; set; }
        #endregion

        #region Duplicate
        public ObservableCollection<string> DuplicateCollection { get; set; }
        
        #endregion

        #region action
        private ObservableCollection<StringOperation> _actionList;
        public ObservableCollection<StringOperation> actionList
        {
            get => _actionList;
            set
            {
                _actionList = value;
                OnPropertyChanged("actionList");
            }
        }
        #endregion

        #endregion

        #region Selected
        #region S_file

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
        #endregion

        #region S_folder

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

        #region S_operation
        private StringOperation _SelectedOperation;
        public StringOperation SelectedOperation
        {
            get => _SelectedOperation;
            set
            {
                _SelectedOperation = value;
                OnPropertyChanged();
               //actionList.Add(_SelectedOperation.Clone());  // unnecessary
            }
        }
        #endregion

        #region S_action
        private int _SelectedAction = -1;
        public int SelectedAction
        {
            get
            {
                return _SelectedAction;
            }
            set
            {
                _SelectedAction = value;
                OnPropertyChanged();
                //MessageBox.Show(selectedOperationIndex.ToString());
                //SelectedOperations.RemoveAt(selectedOperationIndex);
            }
        }
        #endregion

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
                operationCollection = new ObservableCollection<StringOperation>();
                operationCollection.Add(new ReplaceOpertion() { Args = new ReplaceArgs() { From = "b", To = "" } });
                operationCollection.Add(new NewCaseOperation() { Args = new NewCaseArgs() { Mode = 3 } });
                operationCollection.Add(new NewFullnameNormalize());
                operationCollection.Add(new Move() { Args = new MoveArgs() { Mode = 1 } });
                operationCollection.Add(new UniqueName());
                actionList = new ObservableCollection<StringOperation>();
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

        #region Method Command

        #region AddMethod
        private ICommand _addOperationCommand;
        public ICommand AddOperationCommand
        {
            get
            {
                return _addOperationCommand ??
                     (_addOperationCommand = new RelayCommand<object>(
                         (p) => CanExecuteAddOperationCommand(),
                            (p) => ExecuteAddOperationCommand()));
            }
        }
        private bool CanExecuteAddOperationCommand()
        {
            return SelectedOperation == null ? false : true;
        }

        private void ExecuteAddOperationCommand()
        {
            actionList.Add(SelectedOperation.Clone());
            SelectedAction = actionList.Count - 1;
        }
        #endregion

        #region DelMethod

        private ICommand _deleteOperationCommand;
        public ICommand DeleteOperationCommand
        {
            get
            {
                return _deleteOperationCommand ??
                     (_deleteOperationCommand = new RelayCommand<object>(
                         (p) => CanExecuteDeleteOperationCommand(),
                            (p) => ExecuteDeleteOperationCommand()));
            }
        }


        private void ExecuteDeleteOperationCommand()
        {
            actionList.RemoveAt(SelectedAction);
        }
        private bool CanExecuteDeleteOperationCommand()
        {
            return SelectedAction < 0 ? false : true;
        }
        #endregion

        #endregion

        #region Excute
        #region ExcuteActionCommand
        private ICommand _excuteActionCommand;
        public ICommand ExcuteActionCommand
        {
            get
            {
                return _excuteActionCommand ??
                     (_excuteActionCommand = new RelayCommand<object>(
                         (p) => CanExecuteActionCommand(),
                            (p) => ExecuteActionCommand()));
            }
        }
        private bool CanExecuteActionCommand()
        {
            if ((fileInfoList.Count != 0 || folderInfoList.Count != 0) && actionList.Count != 0)
            {
                return true;
            }
            return false;
        }

        private void ExecuteActionCommand()
        {
            ObservableCollection<fileInfo> DuplicateFiles = new ObservableCollection<fileInfo>();
            ObservableCollection<folderInfo> DuplicateFolders = new ObservableCollection<folderInfo>();
            bool isDuplicate = false;
            var Temp = fileService.CopyAll(fileInfoList);
            foreach (var file in Temp)
            {
                string result = file.Filename;
                foreach (var action in actionList)
                {
                    result = action.Operate(result);
                }
                var path = Path.GetDirectoryName(file.Path);
                try
                {
                    var tempfile = new FileInfo(file.Path);
                    tempfile.MoveTo(path + "\\" + result);
                    file.Newfilename = result;
                }
                catch (Exception ex)
                {
                    isDuplicate = true;
                    file.Newfilename = result;
                    file.Error = "Duplicate";
                    DuplicateFiles.Add(file);
                }
            }
            if (isDuplicate == true)
            {
                DuplicateProcess dupWin = new DuplicateProcess(DuplicateFiles, DuplicateFolders);
                dupWin.ShowDialog();
            }
            Preview previewWin = new Preview(fileInfoList,folderInfoList);
            previewWin.ShowDialog();
            fileInfoList.Clear();
            fileInfoList = fileService.CopyAll(Temp);
            OnPropertyChanged("fileInfoList");
           
        }
        #endregion
        #endregion
        #endregion

        private StringOperation selectedValue;
        public StringOperation SelectedValue
        {
            get => selectedValue; set
            {
                selectedValue = value;
                OnPropertyChanged();
            }
        }
    }
}
