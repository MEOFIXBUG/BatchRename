using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Services.File
{
    public interface IFileService
    {
        int IsExist(ObservableCollection<fileInfo> fList, fileInfo f);
    }
}
