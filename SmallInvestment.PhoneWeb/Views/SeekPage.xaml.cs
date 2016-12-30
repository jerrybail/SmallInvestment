using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.NetworkInformation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmallInvestment.Common;
using Coding4Fun.Toolkit.Controls;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class SeekPage : PhoneApplicationPage
    {
        private ProgressIndicator tProgIndicator;

        public SeekPage()
        {
            InitializeComponent();
            LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative)) };

            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 1.0);
            SystemTray.SetBackgroundColor(this, ((SolidColorBrush)DataHelper.ConvertToRGB("#D24726")).Color);

            tProgIndicator = new ProgressIndicator();
            tProgIndicator.IsVisible = true;
            tProgIndicator.Text = "小投资";
            SystemTray.SetProgressIndicator(this, tProgIndicator);

            this.Loaded += new RoutedEventHandler(BindApplicationBar);    
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var cnt= NavigationContext.QueryString.Count();
            if (cnt!=0)
                txtTitle.Text = NavigationContext.QueryString["Title"];
            else
                txtTitle.Text = "公积金提取指南";

            if (NetworkInterface.GetIsNetworkAvailable())
            {

                WbHtml.FontSize = 12;
                WbHtml.Background = (SolidColorBrush)DataHelper.ConvertToRGB("#F2F2F2");

                WbHtml.Opacity = 1.0;
                if (cnt != 0)
                    WbHtml.Navigate(new Uri(NavigationContext.QueryString["ContentUrl"], UriKind.Absolute));
                else
                    WbHtml.Navigate(new Uri("http://bailing.equla.com/houseTip.htm", UriKind.Absolute));
            }
            else
            {
                new ToastPrompt()
                {
                    Height = 55,
                    Message = "对不起，公积金提取指南需联网查看！",
                    VerticalContentAlignment = VerticalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    FontSize = 17,
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Background = (SolidColorBrush)DataHelper.ConvertToRGB("#D24726"),//#58A9DE
                    Foreground = new SolidColorBrush(Colors.White),
                    Opacity = 1.0
                }.Show();
            }
        }


        #region BindApplicationBar
        void BindApplicationBar(object sender, RoutedEventArgs e)
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.BackgroundColor = ((SolidColorBrush)DataHelper.ConvertToRGB("#343434")).Color;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 0.8;


            ApplicationBarIconButton btnBack = new ApplicationBarIconButton() { Text = "返回", IconUri = new Uri("/Assets/AppBarImages/back.png", UriKind.Relative) };
            ApplicationBar.Buttons.Add(btnBack);
            btnBack.Click += new EventHandler(btnBack_Click);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        #endregion



    }
}