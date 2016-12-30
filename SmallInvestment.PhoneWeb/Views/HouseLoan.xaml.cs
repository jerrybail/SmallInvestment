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
using SmallInvestment.Model;
using SmallInvestment.Bll;
using System.Windows.Controls.Primitives;
using SmallInvestment.Common;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class HouseLoan : PhoneApplicationPage
    {
        private ProgressIndicator tProgIndicator;

        public HouseLoan()
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


         //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataHelper.PromptTip();

            var repository = new LoanRepository();

            //绑定概念说明
            listIntroduction.ItemsSource = repository.GetHouseIntroduce();

            PrivotHouseLoanDebx.Visibility = Visibility.Collapsed;
            PrivotHouseLoanDebj.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 计算
        /// </summary>
        private void BtnCompute_Click(object sender, EventArgs e)
        {
            var flag = true;
            if (radBusinessLoan.IsChecked == true)
            {
                if (!string.IsNullOrEmpty(houseBusinessUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseBusinessUc.txtLoanRate.Text))
                    ComputerBusinessHouseLoan();
                else
                    flag = false;
            }

            if (radFundLoad.IsChecked == true)
            {
                if (!string.IsNullOrEmpty(houseFundUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseFundUc.txtLoanRate.Text))
                    ComputerFundHouseLoan();
                else
                    flag = false;
            }

            if (radGroupLoad.IsChecked == true)
            {
                if (!string.IsNullOrEmpty(houseGroupUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseGroupUc.txtLoanRate.Text))
                    ComputerGroupHouseLoan();
                else
                    flag = false;

            }

            if (flag == false)
                txtHouseLoanTip.Text = "提示：请您输入贷款金额和贷款利率。";

            PrivotHouseLoanDebx.Visibility = Visibility.Visible;
            PrivotHouseLoanDebj.Visibility = Visibility.Visible;

        }





        //商贷计算
        private void ComputerBusinessHouseLoan()
        {
            if (!string.IsNullOrEmpty(houseBusinessUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseBusinessUc.txtLoanRate.Text))
            {
                var loanAmount =Convert.ToDouble(houseBusinessUc.txtLoanAmount.Text) ;
                var loanRate = Convert.ToDouble(houseBusinessUc.txtLoanRate.Text) / 100;    
                var houseLoanTerm = (HouseLoanTerm)houseBusinessUc.LPLoanTerm.SelectedItem;
                var repository = new LoanRepository();

                var itemDebx = repository.ComputeHouseLoan_debx(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));
                var itemDebj = repository.ComputeHouseLoan_debj(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));

                if (itemDebx.HouseLoanTerm != 0)
                {               
                    //等额本息
                    houseLoanDebx.tBlockDebx.Text = "还款期： (" + itemDebx.HouseLoanTerm + ") 期   贷款总金额：" + loanAmount.ToString("0.00") + "万元";
                    houseLoanDebx.txtHouseLoanInterest.Text = "支付利息：" + itemDebx.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanDebx.txtHousePayBankAmount.Text = "还款总额：" + itemDebx.HouseLoanPaybankAmount.ToString("0.00") + " 元";
                  
                    if (itemDebx.HouseLoanMonthList.Count() != 0)
                        houseLoanDebx.selDebx.ItemsSource = itemDebx.HouseLoanMonthList;

                    //等额本金
                    houseLoanDebj.tBlockDebj.Text = "还款期： (" + itemDebj.HouseLoanTerm + ") 期   贷款总金额：" + loanAmount.ToString("0.00") + "万元";
                    houseLoanDebj.txtHouseLoanInterest1.Text ="支付利息："+ itemDebj.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanDebj.txtHousePayBankAmount1.Text ="还款总额：" + itemDebj.HouseLoanPaybankAmount.ToString("0.00") + "元";

                    if (itemDebj.HouseLoanMonthList.Count() != 0)
                        houseLoanDebj.selDebj.ItemsSource = itemDebj.HouseLoanMonthList;

                    PivotLayout.SelectedItem = PrivotHouseLoanDebx;

                    houseLoanGroupDebj.Visibility = Visibility.Collapsed;
                    houseLoanGroupDebx.Visibility = Visibility.Collapsed;

                    houseLoanDebx.Visibility = Visibility.Visible;
                    houseLoanDebj.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// 公积金贷款
        /// </summary>
        private void ComputerFundHouseLoan()
        {

            if (!string.IsNullOrEmpty(houseFundUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseFundUc.txtLoanRate.Text))
            {
                var loanAmount = Convert.ToDouble(houseFundUc.txtLoanAmount.Text);
                var loanRate = Convert.ToDouble(houseFundUc.txtLoanRate.Text) / 100;
                var repository = new LoanRepository();
                var houseLoanTerm = (HouseLoanTerm)houseFundUc.LPLoanTerm.SelectedItem;

                var itemDebx = repository.ComputeHouseLoan_debx(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));
                var itemDebj = repository.ComputeHouseLoan_debj(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));

                if (itemDebx.HouseLoanTerm != 0)
                {
                    //等额本息

                    houseLoanDebx.tBlockDebx.Text = "还款期： (" + itemDebx.HouseLoanTerm + ") 期   贷款总金额：" + loanAmount.ToString("0.00") + "万元";
                    houseLoanDebx.txtHouseLoanInterest.Text = "利息总额：" + itemDebx.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanDebx.txtHousePayBankAmount.Text = "还款总额：" + itemDebx.HouseLoanPaybankAmount.ToString("0.00") + " 元";
                    if (itemDebx.HouseLoanMonthList.Count() != 0)
                        houseLoanDebx.selDebx.ItemsSource = itemDebx.HouseLoanMonthList;


                    //等额本金
                    houseLoanDebj.tBlockDebj.Text = "还款期： (" + itemDebj.HouseLoanTerm + ") 期   贷款总金额：" + loanAmount.ToString("0.00") + "万元";
                    houseLoanDebj.txtHouseLoanInterest1.Text = "利息总额：" + itemDebj.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanDebj.txtHousePayBankAmount1.Text = "还款总额：" + itemDebj.HouseLoanPaybankAmount.ToString("0.00") + "元";

                    if (itemDebj.HouseLoanMonthList.Count() != 0)
                        houseLoanDebj.selDebj.ItemsSource = itemDebj.HouseLoanMonthList;

                    PivotLayout.SelectedItem = PrivotHouseLoanDebx;

                    houseLoanGroupDebj.Visibility = Visibility.Collapsed;
                    houseLoanGroupDebx.Visibility = Visibility.Collapsed;

                    houseLoanDebx.Visibility = Visibility.Visible;
                    houseLoanDebj.Visibility = Visibility.Visible;
                }
            }
        }


        /// <summary>
        /// 组合贷款
        /// </summary>
        private void ComputerGroupHouseLoan()
        {
            if (!string.IsNullOrEmpty(houseGroupUc.txtLoanAmount.Text) && !string.IsNullOrEmpty(houseGroupUc.txtLoanFundAmount.Text))
            {
                
                var loanAmount = Convert.ToDouble(houseGroupUc.txtLoanAmount.Text);
                var loanRate = Convert.ToDouble(houseGroupUc.txtLoanRate.Text) / 100;

                var loanFundAmount = Convert.ToDouble(houseGroupUc.txtLoanFundAmount.Text);
                var loanFundRate = Convert.ToDouble(houseGroupUc.txtLoanFundRate.Text)/100;

                var repository = new LoanRepository();
                var houseLoanTerm = (HouseLoanTerm)houseGroupUc.LPLoanTerm.SelectedItem;

                var itemDebx = repository.ComputeHouseLoan_debx(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));
                var itemDebxFund = repository.ComputeHouseLoan_debx(loanFundAmount, loanFundRate, DataHelper.GetInteger(houseLoanTerm.Term));

                var itemDebj = repository.ComputeHouseLoan_debj(loanAmount, loanRate, DataHelper.GetInteger(houseLoanTerm.Term));
                var itemDebjFund = repository.ComputeHouseLoan_debj(loanFundAmount, loanFundRate, DataHelper.GetInteger(houseLoanTerm.Term));

                var loanAmountSum = (loanAmount + loanFundAmount);
                if (itemDebx.HouseLoanTerm != 0)
                {
                    //等额本息
                    houseLoanGroupDebx.tBlockDebx.Text = "还款期： (" + itemDebx.HouseLoanTerm + ") 期   贷款总金额：" + loanAmountSum.ToString("0.00") + "万元";
                    houseLoanGroupDebx.txtHouseLoanBusinessInterest.Text = "商贷利息：" + itemDebx.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanGroupDebx.txtHouseLoanFundInterest.Text = "公积金利息：" + itemDebxFund.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanGroupDebx.txtHouseLoanInterestSum.Text = "支付利息："+(itemDebx.HouseLoanInterest + itemDebxFund.HouseLoanInterest).ToString("0.00") + " 元";
                    houseLoanGroupDebx.txtHouseLoanPayBankSum.Text = "还款总额：" + (itemDebx.HouseLoanAmount + itemDebxFund.HouseLoanAmount + itemDebx.HouseLoanInterest + itemDebxFund.HouseLoanInterest).ToString("0.00") + " 元";
                    
                    if (itemDebx.HouseLoanMonthList.Count() != 0 && itemDebx.HouseLoanMonthList.Count() != 0)
                    {
                        foreach (var item in itemDebx.HouseLoanMonthList)
                        {
                            var itemFund = (from m in itemDebxFund.HouseLoanMonthList
                                            where m.MonthTime == item.MonthTime
                                            select m).FirstOrDefault();

                            item.MonthPayBj += itemFund.MonthPayBj;
                            item.MonthPayBjStr = item.MonthPayBj.ToString("0.00");

                            item.MonthPayDj += itemFund.MonthPayDj;
                            item.MonthPayDjStr = item.MonthPayDj.ToString("0.00");

                            item.MonthPayLx += itemFund.MonthPayLx;
                            item.MonthPayLxStr = item.MonthPayLx.ToString("0.00");

                            item.MonthPayYg += itemFund.MonthPayYg;
                            item.MonthPayYgStr = item.MonthPayYg.ToString("0.00");

                        }

                        houseLoanGroupDebx.selDebx.ItemsSource = itemDebx.HouseLoanMonthList;
                    }


                    //等额本金
                    houseLoanGroupDebj.tBlockDebj.Text += "还款期： (" + itemDebx.HouseLoanTerm + ") 期   贷款总金额：" + loanAmountSum.ToString("0.00") + "万元";
                    houseLoanGroupDebj.txtHouseLoanBusinessInterest1.Text = "商贷利息：" + itemDebj.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanGroupDebj.txtHouseLoanFundInterest1.Text = "公积金利息：" + itemDebjFund.HouseLoanInterest.ToString("0.00") + " 元";
                    houseLoanGroupDebj.txtHouseLoanInterestSum1.Text = "支付利息：" + (itemDebj.HouseLoanInterest + itemDebjFund.HouseLoanInterest).ToString("0.00") + " 元";
                    houseLoanGroupDebj.txtHouseLoanPayBankSum1.Text = "还款总额：" + (itemDebj.HouseLoanAmount + itemDebjFund.HouseLoanAmount + itemDebj.HouseLoanInterest + itemDebjFund.HouseLoanInterest).ToString("0.00") + " 元";

                    if (itemDebj.HouseLoanMonthList.Count() != 0 && itemDebjFund.HouseLoanMonthList.Count()!=0)
                    {
                        foreach (var item in itemDebj.HouseLoanMonthList)
                        {
                            var itemFund = (from m in itemDebjFund.HouseLoanMonthList
                                           where m.MonthTime == item.MonthTime
                                           select m).FirstOrDefault();

                            item.MonthPayBj += itemFund.MonthPayBj;
                            item.MonthPayBjStr = item.MonthPayBj.ToString("0.00");

                            item.MonthPayDj += itemFund.MonthPayDj;
                            item.MonthPayDjStr = item.MonthPayDj.ToString("0.00");

                            item.MonthPayLx += itemFund.MonthPayLx;
                            item.MonthPayLxStr = item.MonthPayLx.ToString("0.00");

                            item.MonthPayYg += itemFund.MonthPayYg;
                            item.MonthPayYgStr = item.MonthPayYg.ToString("0.00");
                            
                        }

                        houseLoanGroupDebj.selDebj.ItemsSource = itemDebj.HouseLoanMonthList;
                    }

                    PivotLayout.SelectedItem = PrivotHouseLoanDebx;

                    houseLoanDebx.Visibility = Visibility.Collapsed;
                    houseLoanDebj.Visibility = Visibility.Collapsed;


                    houseLoanGroupDebx.Visibility = Visibility.Visible;
                    houseLoanGroupDebj.Visibility = Visibility.Visible;
                }
            }
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var selItem = (RadioButton)sender;
            if (selItem != null && houseBusinessUc!=null && houseFundUc!=null && houseGroupUc!=null)
            {
                switch (selItem.Name)
                {
                    case "radBusinessLoan":
                        houseBusinessUc.Visibility = Visibility.Visible;
                        houseFundUc.Visibility = Visibility.Collapsed;
                        houseGroupUc.Visibility = Visibility.Collapsed;
                        break;
                    case "radFundLoad":
                        houseBusinessUc.Visibility = Visibility.Collapsed;
                        houseFundUc.Visibility = Visibility.Visible;
                        houseGroupUc.Visibility = Visibility.Collapsed;
                        break;
                    case "radGroupLoad":
                        houseBusinessUc.Visibility = Visibility.Collapsed;
                        houseFundUc.Visibility = Visibility.Collapsed;
                        houseGroupUc.Visibility = Visibility.Visible;
                        break;
                }
            }
        }


    }
}