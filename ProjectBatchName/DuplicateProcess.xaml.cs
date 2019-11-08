using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ProjectBatchName
{
    /// <summary>
    /// Interaction logic for DuplicateProcess.xaml
    /// </summary>
    public partial class DuplicateProcess : Window
    {
        public DuplicateProcess(ObservableCollection<fileInfo> DuplicateFiles, ObservableCollection<folderInfo> DuplicateFolders)
        {
            InitializeComponent();
            FileDuplicateTab.ItemsSource = DuplicateFiles;
            FolderDuplicateTab.ItemsSource = DuplicateFolders;
        }
    }
}
