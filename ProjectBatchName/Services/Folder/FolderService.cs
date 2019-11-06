using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Services.Folder
{
    public class FolderService:IFolderService
    {
        public static FolderService Instance { get; } = new FolderService();
        public int IsExist(ObservableCollection<folderInfo> fList, folderInfo f)
        {
            int i = -1;
            foreach (var item in fList)
            {
                if (item.Path == f.Path)
                {
                    return ++i;
                }
            }
            return i;
        }
    }
}
