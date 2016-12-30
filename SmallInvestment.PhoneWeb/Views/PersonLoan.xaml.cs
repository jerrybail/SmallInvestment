using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmallInvestment.Common;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class PersonLoan : PhoneApplicationPage
    {
        public PersonLoan()
        {
            InitializeComponent();
            LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative)) };
        }

        //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataHelper.PromptTip();
        }
    }
}