﻿#pragma checksum "G:\WphoneExample\PhoneApp\SmallInvestment.PhoneWeb\UserControls\HouseLoanControls\houseFundUc.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6FABE2404F1F535E870D028091D18406"
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


namespace SmallInvestment.PhoneWeb.UserControls.HouseLoanControls {
    
    
    public partial class houseFundUc : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox txtLoanAmount;
        
        internal Microsoft.Phone.Controls.ListPicker LPLoanTerm;
        
        internal Microsoft.Phone.Controls.ListPicker LPLoadFundRate;
        
        internal System.Windows.Controls.TextBox txtLoanRate;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SmallInvestment.PhoneWeb;component/UserControls/HouseLoanControls/houseFundUc.xa" +
                        "ml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtLoanAmount = ((System.Windows.Controls.TextBox)(this.FindName("txtLoanAmount")));
            this.LPLoanTerm = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("LPLoanTerm")));
            this.LPLoadFundRate = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("LPLoadFundRate")));
            this.txtLoanRate = ((System.Windows.Controls.TextBox)(this.FindName("txtLoanRate")));
        }
    }
}
