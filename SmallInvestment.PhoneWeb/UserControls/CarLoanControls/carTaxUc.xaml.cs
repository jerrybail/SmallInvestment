using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SmallInvestment.Bll;
using SmallInvestment.Model;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmallInvestment.PhoneWeb.UserControls.CarLoanControls
{
    public partial class carTaxUc : UserControl
    {
        public carTaxUc()
        {
            InitializeComponent();

            BindCarDisplacement();

            BindCity();
        }


        /// <summary>
        /// 绑定汽车排量
        /// </summary>
        private void BindCarDisplacement()
        {
            //车辆购置税=不含税车价*10%=含税车价/（1+17%）*10%
            CarLoanRepository repository = new CarLoanRepository();
            LPDisplacement.ItemsSource = repository.GetCarDisplacement();
        }


        /// <summary>
        /// 绑定城市
        /// </summary>
        public void BindCity()
        {
            var repository = new WageRepositoryBll();
            var wageRadioList = repository.GetWageRadioList();
            if (wageRadioList!=null && wageRadioList.Any())
            {
                List<AlphaKeyGroup<WageRadio>> DataSource = AlphaKeyGroup<WageRadio>.CreateGroups(wageRadioList,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (WageRadio s) => { return s.CityGroup; }, true);

                LLSContacts.ItemsSource = DataSource;
            }

            LLSContacts.Visibility = Visibility.Collapsed;

            var imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative));
            LLSContacts.Background = imgBrush;
        }

        private void txtCarCity_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LLSContacts.Visibility = Visibility.Visible;
        }

        private void LLSContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LLSContacts.Visibility = Visibility.Collapsed;

            if (LLSContacts.SelectedItem != null)
                txtCarCity.Text = ((WageRadio)LLSContacts.SelectedItem).City;
            
        }
    }
}
