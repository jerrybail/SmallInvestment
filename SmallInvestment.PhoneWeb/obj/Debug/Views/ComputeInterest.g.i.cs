﻿#pragma checksum "G:\WphoneExample\PhoneApp\SmallInvestment.PhoneWeb\Views\ComputeInterest.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BBD1D678D658648C6502BDD3A9FFB377"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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


namespace SmallInvestment.PhoneWeb.Views {
    
    
    public partial class ComputeInterest : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot PivotLayout;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItemComputer;
        
        internal System.Windows.Controls.Grid GComputer;
        
        internal System.Windows.Controls.TextBox txtAmount;
        
        internal System.Windows.Controls.TextBox txtStoreTime;
        
        internal System.Windows.Controls.TextBox txtSelBank;
        
        internal System.Windows.Controls.TextBlock txtTip;
        
        internal Microsoft.Phone.Controls.LongListSelector LLSContacts;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItemResult;
        
        internal Microsoft.Phone.Controls.LongListSelector LongSelInterestDetail;
        
        internal System.Windows.Controls.TextBlock lblStoreAmount;
        
        internal System.Windows.Controls.TextBlock lblStoreTime;
        
        internal System.Windows.Controls.TextBlock lblStoreTip;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItemDetail;
        
        internal System.Windows.Controls.ListBox listIntroduction;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SmallInvestment.PhoneWeb;component/Views/ComputeInterest.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.PivotLayout = ((Microsoft.Phone.Controls.Pivot)(this.FindName("PivotLayout")));
            this.PivotItemComputer = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItemComputer")));
            this.GComputer = ((System.Windows.Controls.Grid)(this.FindName("GComputer")));
            this.txtAmount = ((System.Windows.Controls.TextBox)(this.FindName("txtAmount")));
            this.txtStoreTime = ((System.Windows.Controls.TextBox)(this.FindName("txtStoreTime")));
            this.txtSelBank = ((System.Windows.Controls.TextBox)(this.FindName("txtSelBank")));
            this.txtTip = ((System.Windows.Controls.TextBlock)(this.FindName("txtTip")));
            this.LLSContacts = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("LLSContacts")));
            this.PivotItemResult = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItemResult")));
            this.LongSelInterestDetail = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("LongSelInterestDetail")));
            this.lblStoreAmount = ((System.Windows.Controls.TextBlock)(this.FindName("lblStoreAmount")));
            this.lblStoreTime = ((System.Windows.Controls.TextBlock)(this.FindName("lblStoreTime")));
            this.lblStoreTip = ((System.Windows.Controls.TextBlock)(this.FindName("lblStoreTip")));
            this.PivotItemDetail = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItemDetail")));
            this.listIntroduction = ((System.Windows.Controls.ListBox)(this.FindName("listIntroduction")));
        }
    }
}

