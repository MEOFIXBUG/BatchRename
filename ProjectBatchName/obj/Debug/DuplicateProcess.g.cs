﻿#pragma checksum "..\..\DuplicateProcess.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1145643FAAA30DFFFF75C8A1106287817E4B9329A80FDC099435DB5332E3473C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ProjectBatchName;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ProjectBatchName {
    
    
    /// <summary>
    /// DuplicateProcess
    /// </summary>
    public partial class DuplicateProcess : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem duplicateTabItems;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid FileDuplicateTab;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid FolderDuplicateTab;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel chooseStackPanel;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox featureComboBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\DuplicateProcess.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DuplicateMethod;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ProjectBatchName;component/duplicateprocess.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DuplicateProcess.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.duplicateTabItems = ((System.Windows.Controls.TabItem)(target));
            return;
            case 2:
            this.FileDuplicateTab = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.FolderDuplicateTab = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.chooseStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.featureComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.DuplicateMethod = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
