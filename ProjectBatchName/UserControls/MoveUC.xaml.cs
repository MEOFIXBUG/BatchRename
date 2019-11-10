using Microsoft.Xaml.Behaviors;
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
            get { return (MoveArgs)GetValue(MoveArgsProperty); }
            set { SetValue(MoveArgsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Args.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoveArgsProperty =
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

        private string oldTextInTextbox;
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            int res = 0;
            if (textBox.Text.Length == 0)
            {
                return;
            }
            else if (!int.TryParse(textBox.Text, out res))
            {
                MessageBox.Show("Length must be an interger");
                textBox.Text = oldTextInTextbox;
                return;
            }


        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            oldTextInTextbox = textBox.Text;
        }
    }
    public class ClearFocusOnOutsideClickBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {

            AssociatedObject.GotFocus += AssociatedObjectOnGotFocus;
            AssociatedObject.LostFocus += AssociatedObjectOnLostFocus;
            base.OnAttached();
        }

        private void AssociatedObjectOnLostFocus(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.MouseUp -= _paren_PreviewMouseUp;
        }

        private void AssociatedObjectOnGotFocus(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.MouseUp += _paren_PreviewMouseUp;
        }

        private void _paren_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= AssociatedObjectOnGotFocus;
            AssociatedObject.LostFocus -= AssociatedObjectOnLostFocus;
        }
    }
}
