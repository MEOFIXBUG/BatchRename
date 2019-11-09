using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectBatchName.UserControls
{
    /// <summary>
    /// Interaction logic for MoveUC.xaml
    /// </summary>
    public partial class MoveUC : BaseUserControl
    {
        public MoveUC()
        {
            InitializeComponent();
        }
        public MoveArgs MoveArgs
        {
            get { return (MoveArgs)GetValue(ArgsProperty); }
            set { SetValue(ArgsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Args.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArgsProperty =
            DependencyProperty.Register("MoveArgs", typeof(MoveArgs), typeof(MoveUC), null);

        private void BaseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (MoveArgs.Mode)
            {
                case 1:
                    button1.IsChecked = true;
                    break;
                default:
                    button2.IsChecked = true;                   
                    break;
            }
        }

       
    }
}
