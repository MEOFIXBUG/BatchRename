using ProjectBatchName.Common;
using ProjectBatchName.Logic;
using ProjectBatchName.Model;
using ProjectBatchName.Services.File;
using ProjectBatchName.Services.Folder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectBatchName.ViewModel
{
    public class DuplicateViewModel :BaseViewModel
    {
        #region Propertises
        private readonly IFileService fileService;
        private readonly IFolderService folderService;
        private ObservableCollection<fileInfo> _duplicateFiles;
        public ObservableCollection<fileInfo> DuplicateFiles 
        {
            get => _duplicateFiles;
            set
            {
                _duplicateFiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<folderInfo> _duplicateFolders;
        public ObservableCollection<folderInfo> DuplicateFolders
        {
            get => _duplicateFolders;
            set
            {
                _duplicateFolders = value;
                OnPropertyChanged();
            }
        }
        #region DuplicateMethod
        public ObservableCollection<string> DuplicateCollection { get; set; }

        #endregion

        #region S_SolveMethod
        private string _SelectedMethodToSolve;
        public string SelectedMethodToSolve
        {
            get => _SelectedMethodToSolve;
            set
            {
                _SelectedMethodToSolve = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #endregion

        #region Contructor
        public DuplicateViewModel()
        {
            fileService = Services.File.FileService.Instance;
            folderService = Services.Folder.FolderService.Instance;
            DuplicateCollection = new ObservableCollection<string>(Enum.GetNames(typeof(DuplicateMethod)).ToList());
        }

        #endregion

        #region Command
        #region AddFileCommand
        private ICommand _nextExcuteCommand;
        public ICommand NextExcuteCommand
        {
            get
            {
                return _nextExcuteCommand ??
                     (_nextExcuteCommand = new RelayCommand<object>(
                         (p) => CanExecuteNextCommand(),
                            (p) => ExecuteNextCommand()));
            }
        }
        private bool CanExecuteNextCommand()
        {
             return true;
        }

        private void ExecuteNextCommand()
        {
            if(SelectedMethodToSolve=="KeepOldName")
            {

            }
            else
            {
                var Temp1 = fileService.CopyAll(DuplicateFiles);

                foreach (var item in Temp1)
                {
                    string newfilePath = item.Path;
                    int next = 1;
                    do
                    {
                        item.Newfilename = item.Newfilename.Insert(item.Newfilename.IndexOf(Path.GetExtension(item.Newfilename), 1), next.ToString());
                        newfilePath = System.IO.Path.GetDirectoryName(item.Path) + "'\\" + item.Newfilename;
                        next++;
                    } while (System.IO.File.Exists(newfilePath));
                    var tempfile = new FileInfo(item.Path);
                    tempfile.MoveTo(System.IO.Path.GetDirectoryName(item.Path) + "\\" + item.Newfilename);
                }
                var Temp2 = folderService.CopyAll(DuplicateFolders);
                foreach (var item in Temp2)
                {
                    string newfolderpath = System.IO.Path.GetDirectoryName(item.Path) + "\\" + item.Newfoldername;
                    int next = 1;
                    while (System.IO.Directory.Exists(newfolderpath))
                    {
                        item.Newfoldername = item.Newfoldername  + $"{next}";
                        newfolderpath = System.IO.Path.GetDirectoryName(item.Path) + "\\" + item.Newfoldername;
                        next++;
                    }
                    DirectoryInfo dir = new DirectoryInfo(item.Path);
                    dir.MoveTo(System.IO.Path.GetDirectoryName(item.Path) + "\\" + item.Newfoldername);
                }

                DuplicateFiles.Clear();
                DuplicateFiles = fileService.CopyAll(Temp1);
                OnPropertyChanged("DuplicateFiles");
                DuplicateFolders.Clear();
                DuplicateFolders = folderService.CopyAll(Temp2);
                OnPropertyChanged("DuplicateFolders");
            }
            
        }
        #endregion
        #endregion

    }
}
