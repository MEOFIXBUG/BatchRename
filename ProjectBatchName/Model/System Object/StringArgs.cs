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

        public string From
        {
            get => from; set
            {
                from = value;
                OnPropertyChanged();
                OnPropertyChanged("Description");
            }
        }
        public string To { get => to; set { to = value; OnPropertyChanged(); OnPropertyChanged("Description"); } }

        public string Description { get => $"Replace from {from} to {to}"; }


    }

    public class NewCaseArgs : IStringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int mode;

        public int Mode { get => mode; set { mode = value; OnPropertyChanged(); OnPropertyChanged("Description"); } }
        public string Description
        {
            get
            {
                switch (Mode)
                {
                    case 1:
                        return "Convert to lowercase";
                    case 2:
                        return "Convert to upercase";
                    default:
                        return "Convert to capitalize case";
                }
            }
        }

    }

    public class MoveArgs : IStringArgs, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int mode;
        int length;

        public int Mode { get => mode; set { mode = value; OnPropertyChanged(); } }
        public int Length { get => length; set { length = value; OnPropertyChanged(); } }
        public string Description
        {
            get
            {
                switch (Mode)
                {
                    case 1:
                        return "Move ISBN to Start";
                    default:
                        return "Move ISBN to End";
                }
            }
        }
    }

    public class NewFullNameNormalizeArgs : IStringArgs
    {
        public string Description { get => "New full name normalize"; }
    }
    public class UniqueNameArgs : IStringArgs
    {

        public string Description { get => "Use GUID to set an unique name"; }
    }

}
