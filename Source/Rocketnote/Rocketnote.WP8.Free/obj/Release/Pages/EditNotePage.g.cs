﻿#pragma checksum "H:\PROJECTS\Windows Phone\Rocketnote\Source\Rocketnote\Rocketnote.WP8.Free\Pages\EditNotePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4DA6FE797458AE1FD8A40F6AF49E16DD"
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


namespace Rocketnote.Pages {
    
    
    public partial class EditNotePage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtNewTitle;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtNewContent;
        
        internal Microsoft.Phone.Controls.ToggleSwitch tglPriority;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Rocketnote;component/Pages/EditNotePage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtNewTitle = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtNewTitle")));
            this.txtNewContent = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtNewContent")));
            this.tglPriority = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("tglPriority")));
        }
    }
}

