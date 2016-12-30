using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SmallInvestment.Common;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class About : PhoneApplicationPage
    {
        private ProgressIndicator tProgIndicator;

        public About()
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

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask wt = new WebBrowserTask();
            wt.Uri = new Uri("http://www.windowsphone.com/zh-cn/store/app/%E5%BE%AE%E7%96%AF%E5%AE%A2%E6%89%8B%E6%9C%BA%E5%8A%A9%E6%89%8B/86f26f7d-c4de-4a49-ac51-9ae63fc334f6", UriKind.Absolute);
            wt.Show();
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