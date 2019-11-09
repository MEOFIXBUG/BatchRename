using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for NewCaseUC.xaml
    /// </summary>
    public partial class NewCaseUC : BaseUserControl
    {
        public NewCaseUC()
        {
            InitializeComponent();
        }



        public NewCaseArgs NewCaseArgs
        {
            get { return (NewCaseArgs)GetValue(ArgsProperty); }
            set { SetValue(ArgsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Args.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArgsProperty =
            DependencyProperty.Register("NewCaseArgs", typeof(NewCaseArgs), typeof(NewCaseUC), null);

        private void BaseUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            switch (NewCaseArgs.Mode)
            {
                case 1: button1.IsChecked = true;
                    break;
                case 2:
                    button2.IsChecked = true;
                    break;
                default:
                    button3.IsChecked = true;
                    break;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
