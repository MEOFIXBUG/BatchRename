using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBatchName.Model
{
    public interface IStringArgs
    {
    }
    
    public class ReplaceArgs : IStringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string from;
        string to;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
    }

    public class NewCaseArgs: IStringArgs
    {
        int mode;

        public int Mode { get => mode; set => mode = value; }
    }

    public class MoveArgs: IStringArgs
    {
        int mode;
        int length;

        public int Mode { get => mode; set => mode = value; }
        public int Length { get => length; set => length = value; }
    }

 
}
