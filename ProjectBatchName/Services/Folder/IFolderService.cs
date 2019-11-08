using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Services.Folder
{
    public interface IFolderService
    {
        int IsExist(ObservableCollection<folderInfo> fList, folderInfo f);
        ObservableCollection<folderInfo> CopyAll(ObservableCollection<folderInfo> fList);
    }
}
