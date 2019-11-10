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
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

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
                OnPropertyChanged("fi-leInfoList");
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
                operationCollection.Add(new ReplaceOpertion() { Args = new ReplaceArgs() { From = "From", To = "To" } });
                operationCollection.Add(new NewCaseOperation() { Args = new NewCaseArgs() { Mode = 3 } });
                operationCollection.Add(new NewFullnameNormalize() { Args = new NewFullNameNormalizeArgs() }); ;
                operationCollection.Add(new Move() { Args = new MoveArgs() { Mode = 1 } });
                operationCollection.Add(new UniqueName() { Args = new UniqueNameArgs() });
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
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var listFile = Directory.GetFiles(dialog.FileName);
                //var listFile = Directory.GetFiles(dialog.FileName);
                foreach (var file in listFile)
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
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var directory = dialog.FileName;
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

        #region SavePresetMethod
        private ICommand _savePresetCommand;
        public ICommand SavePresetCommand
        {
            get
            {
                return _savePresetCommand ??
                     (_savePresetCommand = new RelayCommand<object>(
                         (p) => CanExecuteSavePresetCommand(),
                            (p) => ExecuteSavePresetCommand()));
            }
        }
        private bool CanExecuteSavePresetCommand()
        {
            return actionList.Count() > 0;
        }

        private void ExecuteSavePresetCommand()
        {

            string preset = "";
            foreach (var action in actionList)
            {
                //   Debug.WriteLine(action.Name);
                if (action.Name == "Replace")
                {
                    var args = action.Args as ReplaceArgs;
                    preset += "0" + "," + args.From + "," + args.To + "\r\n";
                }
                else if (action.Name == "New Case")
                {
                    var args = action.Args as NewCaseArgs;
                    preset += "1" + "," + args.Mode.ToString() + "\r\n";
                }
                else if (action.Name == "Move")
                {
                    var args = action.Args as MoveArgs;
                    preset += "2" + "," + args.Mode + "\r\n";
                }
                else if (action.Name == "Unique Name")
                {
                    preset += "3" + "\r\n";
                }
                else
                {
                    preset += "4" + "\r\n";
                }
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                    sw.WriteLine(preset);
            }
        }
        #endregion

        #region LoadPresetMethod
        private ICommand _loadPresetCommand;
        public ICommand LoadPresetCommand
        {
            get
            {
                return _loadPresetCommand ??
                     (_loadPresetCommand = new RelayCommand<object>(
                         (p) => CanExecuteLoadPresetCommand(),
                            (p) => ExecuteLoadPresetCommand()));
            }
        }
        private bool CanExecuteLoadPresetCommand()
        {
            return true;
        }

        private void ExecuteLoadPresetCommand()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string preset = File.ReadAllText(openFileDialog.FileName);
                string[] seperator = { ",", "\r\n" };
                var tokens = preset.Split(seperator, StringSplitOptions.None);
                actionList.Clear();
                int i = 0;
                while (i < tokens.Length)
                {
                    if (tokens[i] == "0")
                    {
                        actionList.Add(new ReplaceOpertion() { Args = new ReplaceArgs() { From = tokens[i + 1], To = tokens[i + 2] } });
                        i += 3;
                        continue;
                    }
                    if (tokens[i] == "1")
                    {
                        actionList.Add(new NewCaseOperation() { Args = new NewCaseArgs() { Mode = Int32.Parse(tokens[i + 1]) } });
                        i += 2;
                        continue;
                    }
                    if (tokens[i] == "2")
                    {
                        actionList.Add(new Move() { Args = new MoveArgs() { Mode = Int32.Parse(tokens[i + 1]) } }); ;
                        i += 2;
                        continue;
                    }
                    if (tokens[i] == "3")
                    {
                        actionList.Add(new UniqueName() { Args = new UniqueNameArgs() });
                        i += 1;
                        continue;
                    }
                    if (tokens[i] == "4")
                    {
                        actionList.Add(new NewFullnameNormalize() { Args = new NewFullNameNormalizeArgs() });
                        i += 1;
                        continue;
                    }
                    break;
                }
            }

        }
        #endregion
        #endregion

        #region Move Acton Command
        #region Move To Top
        private ICommand moveActionToTopCommand;
        public ICommand MoveActionToTopCommand
        {
            get
            {
                return moveActionToTopCommand ??
                     (moveActionToTopCommand = new RelayCommand<object>(
                         (p) => { return SelectedAction > 0 ? true : false; },
                         (p) =>
                            {
                                if (SelectedAction > 0)
                                    actionList.Move(SelectedAction, 0);
                            }));


            }
        }


        #endregion

        #region Move up
        private ICommand moveActionUpCommand;
        public ICommand MmoveActionUpCommand
        {
            get
            {
                return moveActionUpCommand ??
                     (moveActionUpCommand = new RelayCommand<object>(
                         (p) => { return SelectedAction > 0 ? true : false; },
                         (p) =>
                         {
                             if (SelectedAction > 0)
                                 actionList.Move(SelectedAction, SelectedAction - 1);
                         }));


            }
        }
        #endregion

        #region Move Down
        private ICommand moveActionDownCommand;
        public ICommand MmoveActionDownCommand
        {
            get
            {
                return moveActionDownCommand ??
                     (moveActionDownCommand = new RelayCommand<object>(
                         (p) => { return SelectedAction >= 0 && SelectedAction < actionList.Count -1 ? true : false; },
                         (p) =>
                         {
                             if (SelectedAction < actionList.Count - 1)
                                 actionList.Move(SelectedAction, SelectedAction + 1);
                         }));


            }
        }

        #region Move to bottom
        private ICommand moveActionToBottomCommand;
        public ICommand MoveActionToBottomCommand
        {
            get
            {
                return moveActionToBottomCommand ??
                     (moveActionToBottomCommand = new RelayCommand<object>(
                         (p) => { return SelectedAction >= 0 && SelectedAction < actionList.Count - 1 ? true : false; },
                         (p) =>
                         {
                             if (SelectedAction < actionList.Count - 1)
                                 actionList.Move(SelectedAction, actionList.Count - 1);
                         }));


            }
        }
        #endregion
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
            //_____________file
            var Temp1 = fileService.CopyAll(fileInfoList);
            foreach (var item in Temp1)
            {
                string result = item.Filename;
                foreach (var action in actionList)
                {
                    result = action.Operate(result);
                }
                var path = Path.GetDirectoryName(item.Path);
                try
                {
                    var tempfile = new FileInfo(item.Path);
                    tempfile.MoveTo(path + "\\" + result);
                    item.Newfilename = result;
                }
                catch (Exception ex)
                {
                    isDuplicate = true;
                    item.Newfilename = result;
                    item.Error = "Duplicate";
                    DuplicateFiles.Add(item);
                }
            }

            //_____________folder
            var Temp2 = folderService.CopyAll(folderInfoList);
            int next = 0;
            foreach (var item in Temp2)
            {
                string result = item.Foldername;

                foreach (var action in actionList)
                {
                    result = action.Operate(result);
                }
                string newfolderpath = Path.GetDirectoryName(item.Path) + "\\" + result;
                string tempFolderName = "\\Temp";
                string tempFolderPath = Path.GetDirectoryName(item.Path) + tempFolderName;
                CopyAll(item.Path, tempFolderPath, true);

                if (item.Path.Equals(newfolderpath) == false)
                {
                    RemoveDirectory(item.Path);
                    Directory.Delete(item.Path);
                    try
                    {
                        Directory.Move(tempFolderPath, newfolderpath);
                        item.Newfoldername = result;
                        item.Error = "OK";
                    }
                    catch (Exception ex)
                    {
                        string duplicatestore = Path.GetDirectoryName(item.Path) + "\\Store" + $"{++next}";
                        CopyAll(tempFolderPath, duplicatestore, true);
                        RemoveDirectory(tempFolderPath);
                        Directory.Delete(tempFolderPath);
                        isDuplicate = true;
                        item.Newfoldername = result;
                        item.Error = "Duplicate Foldername";
                        DuplicateFolders.Add(item);
                    }
                }
                else
                {
                    RemoveDirectory(tempFolderPath);
                    Directory.Delete(tempFolderPath);
                }
            }

            if (isDuplicate == true)
            {

                DuplicateProcess dupWin = new DuplicateProcess(DuplicateFiles, DuplicateFolders);
                dupWin.ShowDialog();
            }
            Preview previewWin = new Preview(fileInfoList, folderInfoList);
            previewWin.ShowDialog();
            fileInfoList.Clear();
            fileInfoList = fileService.CopyAll(Temp1);
            OnPropertyChanged("fileInfoList");
            folderInfoList.Clear();
            folderInfoList = folderService.CopyAll(Temp2);
            OnPropertyChanged("folderInfoList");

        }

        public static void CopyAll(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            if (!dir.Exists)
            {
                System.Windows.MessageBox.Show("Source Directory does not exist or could not be found !");
            }

            if (!Directory.Exists(destDirName))
            {
                DirectoryInfo tempFolder = Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    CopyAll(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static void RemoveDirectory(string sourcePath)
        {
            DirectoryInfo src = new DirectoryInfo(sourcePath);

            foreach (var file in src.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in src.GetDirectories())
            {
                dir.Delete(true);
            }
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
