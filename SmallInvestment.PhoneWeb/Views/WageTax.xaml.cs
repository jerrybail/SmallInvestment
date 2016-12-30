using System.Linq;
using System.Windows;
using SmallInvestment.Bll;
using SmallInvestment.Common;
using SmallInvestment.Model;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using System.Windows.Navigation;
using System.Windows.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class WageTax 
    {
        private ProgressIndicator tProgIndicator;

        public WageTax()
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
            ApplicationBar.BackgroundColor = ((SolidColorBrush)DataHelper.ConvertToRGB("#343434")).Color;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 0.8;

            ApplicationBarIconButton btnSave = new ApplicationBarIconButton();
            btnSave.IconUri = new Uri("/Assets/AppBarImages/check.png", UriKind.Relative);
            btnSave.Text = "计算";
            ApplicationBar.Buttons.Add(btnSave);
            btnSave.Click += new EventHandler(BtnCompute_Click);

            ApplicationBarIconButton btnCancel = new ApplicationBarIconButton();
            btnCancel.IconUri = new Uri("/Assets/AppBarImages/delete.png", UriKind.Relative);
            btnCancel.Text = "清空";
            ApplicationBar.Buttons.Add(btnCancel);
            btnCancel.Click += new EventHandler(BtnClear_Click);
        }


        /// <summary>
        /// 计算工资扣税
        /// </summary>
        private void BtnCompute_Click(object sender, EventArgs e)
        {
            var wageRadioObject = LLSContacts.SelectedItem;
            var wageAmount = DataHelper.GetInteger(txtWage.Text);

            var wagePersonSum = 0.0;
            var wageResult = 0.0;
            var wageCompanySum = 0.0;

            if (wageAmount == 0 || string.IsNullOrEmpty(txtCity.Text))
            {
                tBlockTip.Text = "提示：请您输入税前工资，并选择所在城市。 ";
            }
            else
            {
                if (wageRadioObject != null)
                {
                    var repository = new WageRepositoryBll();
                    var wageTaxResult = repository.ComputerWageResult(wageAmount, (WageRadio) wageRadioObject);
                    if (wageTaxResult.Any())
                    {
                        selWageTax.ItemsSource = wageTaxResult.Where(r=>r.TypeFlag==1).OrderBy(r => r.OrderFlag).ToList();

                        foreach (var item in wageTaxResult)
                        {
                            wagePersonSum += item.PersonFund;
                            wageCompanySum += item.CompanyFund;
                        }

                        if (!wageTaxResult.Any()) return;

                        wageResult = wageAmount - wagePersonSum;

                        txtWuXian.Text = "五险一金：" + wageTaxResult.Where(r => r.TypeFlag == 1).Sum(r => r.PersonFund).ToString("0.00") + "    个人所得税：" + wageTaxResult.Where(r => r.TypeFlag == 2).Sum(r => r.PersonFund).ToString("0.00");
                        txtPeronSum.Text = "个人缴纳：" + wagePersonSum.ToString("0.00") + "    公司缴纳：" + wageCompanySum.ToString("0.00") + "   税后工资：" + wageResult.ToString("0.00");
                        PivotLayout.SelectedItem = PivotWageDetail;
                        BtnReComputer.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        //清空操作
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtWage.Text = "";
            txtCity.Text = "";
        }


        private void HideControl()
        {
            foreach (var item in GWage.Children)
            {
                item.Visibility = Visibility.Collapsed;
            }
            LLSContacts.Visibility = Visibility.Visible;
        }

        private void ShowControl()
        {
            foreach (var item in GWage.Children)
            {
                item.Visibility = Visibility.Visible;
            }
            LLSContacts.Visibility = Visibility.Collapsed;
        }

        private void txtCity_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HideControl();
        }

        private void LLSContacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowControl();

            if (LLSContacts.SelectedItem != null)
               txtCity.Text = ((WageRadio)LLSContacts.SelectedItem).City;
        }

        private void PivotLayout_LoadedPivotItem(object sender, Microsoft.Phone.Controls.PivotItemEventArgs e)
        {
            ShowControl();
        }

        //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataHelper.PromptTip();

            var repository = new WageRepositoryBll();
            var wageRadioList = repository.GetWageRadioList();

            if (wageRadioList != null && wageRadioList.Any())
            {
                List<AlphaKeyGroup<WageRadio>> DataSource = AlphaKeyGroup<WageRadio>.CreateGroups(wageRadioList,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (WageRadio s) => { return s.CityGroup; }, true);

                LLSContacts.ItemsSource = DataSource;
            }

            var wageTaxInList = repository.GetWageTaxIntroduce();
            listIntroduction.ItemsSource = wageTaxInList;
            BtnReComputer.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// 重新计算
        /// </summary>
        /// http://blog.csdn.net/wushang923/article/details/6742378
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var wageRadioObject = LLSContacts.SelectedItem;
            var wageAmount = DataHelper.GetInteger(txtWage.Text);
            var wagePersonSum = 0.0;
            var wageResult = 0.0;
            var wageCompanySum = 0.0;
            float wageRateNum = 0;
            var wageRadioEntity = new WageRadio();

            if (wageRadioObject != null)
            {
                wageRadioEntity = (WageRadio)wageRadioObject;

                List<TextBox> collection = GetChildObjects<TextBox>(selWageTax, "");//第2个参数为空，表示查找所有指定类型的控件（返回一个CheckBox集合）     
                foreach (TextBox item in collection)//遍历这个集合  
                {
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        wageRateNum = DataHelper.GetFloat(item.Text)/100;
                        switch (item.Tag.ToString())
                        {

                            case "txtPersonE": //养老个人
                                wageRadioEntity.EndowmentFundRadio = wageRateNum;
                                break;

                            case "txtCompanyE": //养老公司
                                wageRadioEntity.EndowmentFundCompanyRadio = wageRateNum;
                                break;

                            case "txtPersonH":  //住房个人
                                wageRadioEntity.HousingFundRadio = wageRateNum;
                                break;

                            case "txtCompanyH":  //住房公司
                                wageRadioEntity.HousingFundCompayRadio = wageRateNum;
                                break;

                            case "txtPersonM":  // 医疗个人
                                wageRadioEntity.MedicareFundRadio = wageRateNum;
                                break;

                            case "txtCompanyM": // 医疗公司
                                wageRadioEntity.MedicareFundCompanyRadio = wageRateNum;
                                break;

                            case "txtPersonI": //工伤个人
                                wageRadioEntity.InjuryFundRadio = wageRateNum;
                                break;

                            case "txtCompanyI": //工伤公司
                                wageRadioEntity.InjuryFundCompanyRadio = wageRateNum;
                                break;

                            case "txtPersonB": //生育个人
                                wageRadioEntity.BirthFundRadio = wageRateNum;
                                break;

                            case "txtCompanyB"://生育公司
                                wageRadioEntity.BirthFundCompanyRadio = wageRateNum;
                                break;

                            case "txtPersonU": //失业个人
                                wageRadioEntity.UnemploymentFundRadio = wageRateNum;
                                break;
                            case "txtCompanyU": //失业公司
                                wageRadioEntity.UnemploymentFundCompanyRadio = wageRateNum;
                                break;
                        }
                    }
                }



                var repository = new WageRepositoryBll();
                var wageTaxResult = repository.ComputerWageResult(wageAmount, wageRadioEntity);
                if (wageTaxResult.Any())
                {
                    selWageTax.ItemsSource = wageTaxResult.Where(r => r.TypeFlag == 1).OrderBy(r => r.OrderFlag).ToList();

                    foreach (var item in wageTaxResult)
                    {
                        wagePersonSum += item.PersonFund;
                        wageCompanySum += item.CompanyFund;
                    }

                    if (!wageTaxResult.Any()) return;

                    wageResult = wageAmount - wagePersonSum;

                    txtWuXian.Text = "五险一金：" + wageTaxResult.Where(r => r.TypeFlag == 1).Sum(r => r.PersonFund).ToString("0.00") + "    个人所得税：" + wageTaxResult.Where(r => r.TypeFlag == 2).Sum(r => r.PersonFund).ToString("0.00");
                    txtPeronSum.Text = "个人缴纳：" + wagePersonSum.ToString("0.00") + "    公司缴纳：" + wageCompanySum.ToString("0.00") + "   税后工资：" + wageResult.ToString("0.00");
                    PivotLayout.SelectedItem = PivotWageDetail;
                }
            }

        }




        public List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, ""));//指定集合的元素添加到List队尾  
            }
            return childList;
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