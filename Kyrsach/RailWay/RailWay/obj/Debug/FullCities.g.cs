#pragma checksum "..\..\FullCities.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "86783D883C17C2B95C520E5A1BB579C9E93DAE8C2CF20B92280828FBB75C7CF7"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using RailWay;
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


namespace RailWay {
    
    
    /// <summary>
    /// FullCities
    /// </summary>
    public partial class FullCities : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteBTN;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitBTN;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addDoljnostBTN;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid cityGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn id;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn name;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\FullCities.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn count;
        
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
            System.Uri resourceLocater = new System.Uri("/RailWay;component/fullcities.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FullCities.xaml"
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
            
            #line 8 "..\..\FullCities.xaml"
            ((RailWay.FullCities)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.deleteBTN = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\FullCities.xaml"
            this.deleteBTN.Click += new System.Windows.RoutedEventHandler(this.deleteBTN_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.exitBTN = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\FullCities.xaml"
            this.exitBTN.Click += new System.Windows.RoutedEventHandler(this.exitBTN_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.addDoljnostBTN = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\FullCities.xaml"
            this.addDoljnostBTN.Click += new System.Windows.RoutedEventHandler(this.addDoljnostBTN_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cityGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 30 "..\..\FullCities.xaml"
            this.cityGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.cityGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.id = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.name = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 8:
            this.count = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

