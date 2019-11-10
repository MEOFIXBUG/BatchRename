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
    /// Interaction logic for Preview.xaml
    /// </summary>
    public partial class Preview : Window
    {
        public Preview(ObservableCollection<fileInfo> FileList, ObservableCollection<folderInfo> FolderList)
        {
            InitializeComponent();
            FilePreviewTab.ItemsSource = FileList;
            FolderPreviewTab.ItemsSource = FolderList;
        }

    }
}
