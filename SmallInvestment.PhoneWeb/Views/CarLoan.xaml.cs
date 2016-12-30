using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using SmallInvestment.Bll;
using SmallInvestment.Model;
using SmallInvestment.Common;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class CarLoan : PhoneApplicationPage
    {
        private ProgressIndicator tProgIndicator;

        public CarLoan()
        {
            InitializeComponent();
            LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative)) };

            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 1.0);
            SystemTray.SetBackgroundColor(this, ((SolidColorBrush)DataHelper.ConvertToRGB("#048EFF")).Color);

            tProgIndicator = new ProgressIndicator();
            tProgIndicator.IsVisible = true;
            tProgIndicator.Text = "小投资";
            SystemTray.SetProgressIndicator(this, tProgIndicator);
            this.Loaded += new RoutedEventHandler(BindApplicationBar);      
        }

        void BindApplicationBar(object sender, RoutedEventArgs e)
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.BackgroundColor = ((SolidColorBrush)DataHelper.ConvertToRGB("#343434")).Color;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 0.8;

            ApplicationBarIconButton btnSave = new ApplicationBarIconButton();
            btnSave.IconUri = new Uri("/Assets/AppBarImages/check.png", UriKind.Relative);
            btnSave.Text = "计算";
            ApplicationBar.Buttons.Add(btnSave);
            btnSave.Click += new EventHandler(BtnCompute_Click);
        }

        /// <summary>
        /// 计算
        /// </summary>
        private void BtnCompute_Click(object sender, EventArgs e)
        {
            ComputerCarloan();
        }


        //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            carLoanDetail.Visibility = Visibility.Collapsed;
            DataHelper.PromptTip();
        }


        private void ComputerCarloan()
        {
            CarLoanRepository repository= new CarLoanRepository();

            if (!string.IsNullOrEmpty(carLoanUc.txtCarAmount.Text) && !string.IsNullOrEmpty(carLoanUc.txtLoanRate.Text))
            {
                var loanAmount = Convert.ToDouble(carLoanUc.txtCarAmount.Text);
                var loanRate = Convert.ToDouble(carLoanUc.txtLoanRate.Text) / 100;
                var loanTerm = (HouseLoanTerm)carLoanUc.LPLoanTerm.SelectedItem;
                var loanPayLay = (HouseLoanLay)carLoanUc.LPLoanPayLay.SelectedItem;

                var firtPay = loanAmount * loanPayLay.HouseLoanLayValue / 100;
                loanAmount = loanAmount - firtPay;

                var carLoanResult = repository.ComputeCarLoan(loanAmount, loanRate, DataHelper.GetInteger(loanTerm.Term));

                if (carLoanResult != null)
                {
                    carLoanDetail.txtCarLoanTitle.Text = "贷款金额 " + loanAmount.ToString("0.00") + " 万元 " + " (共 " + loanTerm.Term + " 期)";
                    carLoanDetail.txtFirstPay.Text = "首付比例：" + loanPayLay.HouseLoanLayName + "  首付(" + firtPay.ToString("0.00") + " 万元 )";
                    carLoanDetail.txtCarLoanInterest.Text = "支付利息：" + carLoanResult.HouseLoanInterest.ToString("0.00") + " 元";
                    carLoanDetail.txtCarPayBankMonth.Text = "每期还款：" + carLoanResult.HouseLoanPaybankMonth.ToString("0.00") + " 元";
                    carLoanDetail.txtCarPayBankAmount.Text = "还款总额：" + carLoanResult.HouseLoanPaybankAmount.ToString("0.00") + " 元";

                    if (carLoanResult.HouseLoanMonthList != null && carLoanResult.HouseLoanMonthList.Count() > 0)
                        carLoanDetail.selDebx.ItemsSource = carLoanResult.HouseLoanMonthList;

                    PivotLayout.SelectedItem = PivotItemDetail;
                    carLoanDetail.Visibility = Visibility.Visible;
                }
            }
            else
            {
                tBlockTip.Text = "请输入裸车价格，并输入贷款利率。";
            }
        }

        private void ComputerCarTax()
        {
            //CarLoanRepository repository = new CarLoanRepository();

            //if (!string.IsNullOrEmpty(carTaxUc.txtCarAmount.Text) && !string.IsNullOrEmpty(carTaxUc.txtCarCity.Text))
            //{
            //    var loanAmount = Convert.ToDouble(carTaxUc.txtCarAmount.Text);

            //    var carLoanResult = repository.ComputerCarTax(loanAmount);

            //    if (carLoanResult != null)
            //    {
            //        PivotLayout.SelectedItem = pivotDetail;

            //        carTaxDetail.Visibility = Visibility.Collapsed;
            //        carLoanDetail.Visibility = Visibility.Visible;
            //    }
            //}
        }

        private void RadioCarLoan_Click(object sender, RoutedEventArgs e)
        {
            BindUserControl();
        }

        
        private void RadioCarTax_Click(object sender, RoutedEventArgs e)
        {
            BindUserControl();
        }

        private void BindUserControl()
        {
            //if (RadioCarLoan.IsChecked == true)
            //{
                carLoanUc.Visibility = Visibility.Visible;
               // carTaxUc.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
                //carTaxUc.Visibility = Visibility.Visible;
                //carLoanUc.Visibility = Visibility.Collapsed;
            //}
        }

        private void PivotLayout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivotItem = (PivotItem)PivotLayout.SelectedItem;

            if (ApplicationBar != null)
            {
                if (pivotItem.Name == "PivotItemComputer")
                    ApplicationBar.IsVisible = true;
                else
                    ApplicationBar.IsVisible = false;
            }
        }


    }
}