using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectBatchName.ViewModel
{
    class MethodsViewModel : BaseViewModel
    {
        ObservableCollection<StringOperation> operations;
        public ObservableCollection<StringOperation> Operations
        {
            get => operations;
            set
            {
                Operations = value;
                //OnPropertyChanged();
            }
        }

        ObservableCollection<StringOperation> selectedOperations;
        public ObservableCollection<StringOperation> SelectedOperations
        {
            get => selectedOperations;
            set
            {
                selectedOperations = value;
                OnPropertyChanged();
            }
        }

        StringOperation selectedOperation;
        public StringOperation SelectedOperation
        {
            get
            {
                return selectedOperation;
            }
            set
            {
                selectedOperation = value;
                OnPropertyChanged();
                SelectedOperations.Add(selectedOperation);
            }
        }
        int selectedOperationIndex = -1;
        public int SelectedOperationIndex
        {
            get
            {
                return selectedOperationIndex;
            }
            set
            {
                selectedOperationIndex = value;
                OnPropertyChanged();
                //MessageBox.Show(selectedOperationIndex.ToString());
                //SelectedOperations.RemoveAt(selectedOperationIndex);
            }
        }

        public ICommand AddOperationCommand { get; set; }
        public ICommand DeleteOperationCommand { get; set; }

        public MethodsViewModel()
        {
            operations = new ObservableCollection<StringOperation>();
            operations.Add(new ReplaceOpertion() { Args = new ReplaceArgs() { From = "From", To = "To" } });
            operations.Add(new NewCaseOperation() { Args = new NewCaseArgs() { Mode = 3 } });
            operations.Add(new NewFullnameNormalize());
            operations.Add(new Move() { Args = new MoveArgs() { Mode = 1 } });
            operations.Add(new UniqueName());

            selectedOperations = new ObservableCollection<StringOperation>();

            AddOperationCommand = new RelayCommand<object>(
             (p) =>
             CanExecuteAddOperationCommand(),
             (p) => ExecuteAddOperationCommand());

            DeleteOperationCommand = new RelayCommand<object>(
            (p) =>
            CanExecuteDeleteOperationCommand(),
            (p) => ExecuteDeleteOperationCommand());
        }

        private void ExecuteAddOperationCommand()
        {
            SelectedOperations.Add(SelectedOperation);
        }
        private bool CanExecuteAddOperationCommand()
        {
            return SelectedOperation == null ? false : true;
        }

        private void ExecuteDeleteOperationCommand()
        {
            SelectedOperations.RemoveAt(selectedOperationIndex);
        }
        private bool CanExecuteDeleteOperationCommand()
        {
            return selectedOperationIndex < 0 ? false : true;
        }
    }
}
