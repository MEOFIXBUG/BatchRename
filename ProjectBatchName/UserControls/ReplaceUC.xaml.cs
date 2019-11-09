﻿using ProjectBatchName.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ReplaceUC.xaml
    /// </summary>
    public partial class ReplaceUC : BaseUserControl
    {
        public ReplaceUC() 
        {
            InitializeComponent();
            this.DataContext = this;
        }
        
        public ReplaceArgs ReplaceArgs
        {
            get { return (ReplaceArgs)GetValue(ArgsProperty); }
            set { SetValue(ArgsProperty, value); OnPropertyChanged(); }
        }

        // Using a DependencyProperty as the backing store for Args.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArgsProperty =
            DependencyProperty.Register("ReplaceArgs", typeof(ReplaceArgs), typeof(ReplaceUC), null);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var args = new ReplaceArgs() { From = Args.From, To = Args.To };
            //Args = args;
            //Args = new ReplaceArgs() { From = from.Text, To = to.Text };

        }
    }
   
}
