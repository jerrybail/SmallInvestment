﻿#pragma checksum "G:\WphoneExample\PhoneApp\SmallInvestment.PhoneWeb\UserControls\HouseLoanControls\houseLoanDebx.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5660083480636E5D0927D4A16D34908D"
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
    
    
    public partial class houseLoanDebx : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock tBlockDebx;
        
        internal System.Windows.Controls.TextBlock txtHouseLoanInterest;
        
        internal System.Windows.Controls.TextBlock txtHousePayBankAmount;
        
        internal Microsoft.Phone.Controls.LongListMultiSelector selDebx;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SmallInvestment.PhoneWeb;component/UserControls/HouseLoanControls/houseLoanDebx." +
                        "xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.tBlockDebx = ((System.Windows.Controls.TextBlock)(this.FindName("tBlockDebx")));
            this.txtHouseLoanInterest = ((System.Windows.Controls.TextBlock)(this.FindName("txtHouseLoanInterest")));
            this.txtHousePayBankAmount = ((System.Windows.Controls.TextBlock)(this.FindName("txtHousePayBankAmount")));
            this.selDebx = ((Microsoft.Phone.Controls.LongListMultiSelector)(this.FindName("selDebx")));
        }
    }
}

