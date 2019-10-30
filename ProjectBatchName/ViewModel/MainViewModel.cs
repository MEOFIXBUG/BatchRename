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

namespace ProjectBatchName.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //code
        private readonly IFileService fileService;
        private readonly ILogic logic;
        //private readonly IFolderService Service;
        private ObservableCollection<fileInfo> _fileInfoList;
        public ObservableCollection<fileInfo> fileInfoList
        {
            get => _fileInfoList;
            set { _fileInfoList = value; OnPropertyChanged(); }
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
        /*Add New Item Command*/
        public ICommand AddFilesCommand { get; set; }
        private bool CanExecuteAddFileCommand()
        {
            return true;
        }

        private void ExecuteAddFileCommand()
        {
            var screen = new Microsoft.Win32.OpenFileDialog();
            screen.Multiselect = true;
            if (screen.ShowDialog() == true)
            {
                foreach (var file in screen.FileNames)
                {
                    var temp = new fileInfo();
                    temp.Filename = System.IO.Path.GetFileName(file);
                    temp.Path = file;
                    if (fileService.IsExist(fileInfoList,temp)>=0)
                    {
                        MessageBox.Show("Existed file");
                    }
                    else
                    {
                        fileInfoList.Add(temp);
                    }
                }
            }
        }
        public bool Isloaded = false;
        public MainViewModel()
        {
            logic = Logic.Logic.Instance;
            fileService = Services.File.FileService.Instance;
            if (!Isloaded)
            {
                Isloaded = true;
                //MessageBox.Show("Developed by MVVM");
            }
            if (fileInfoList == null)
            {
                fileInfoList = new ObservableCollection<fileInfo>();
            }
            AddFilesCommand = new RelayCommand<object>(
                (p) =>
                CanExecuteAddFileCommand(),
                (p) => ExecuteAddFileCommand());
        }
    }
}
