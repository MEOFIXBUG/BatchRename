﻿using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Services.File
{
    public class FileService : IFileService
    {
        public static FileService Instance { get; } = new FileService();
        public int IsExist(ObservableCollection<fileInfo> fList, fileInfo f)
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
