﻿#pragma checksum "H:\PROJECTS\Windows Phone\Rocketnote\Source\Rocketnote\Rocketnote.WP8.Free\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A1DB9A550E95732D9E36958A0718B782"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Rocketnote {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot pivRocketnote;
        
        internal Microsoft.Phone.Controls.PivotItem pivNotebookTitle;
        
        internal System.Windows.Controls.ListBox lstActivNotes;
        
        internal System.Windows.Controls.ListBox lstNotesInTrash;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Rocketnote;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.pivRocketnote = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivRocketnote")));
            this.pivNotebookTitle = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("pivNotebookTitle")));
            this.lstActivNotes = ((System.Windows.Controls.ListBox)(this.FindName("lstActivNotes")));
            this.lstNotesInTrash = ((System.Windows.Controls.ListBox)(this.FindName("lstNotesInTrash")));
        }
    }
}

