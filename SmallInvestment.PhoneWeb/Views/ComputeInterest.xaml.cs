using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SmallInvestment.Bll;
using SmallInvestment.Common;
using SmallInvestment.Model;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;

namespace SmallInvestment.PhoneWeb.Views
{
    public partial class ComputeInterest 
    {
        private ProgressIndicator tProgIndicator;

        public ComputeInterest()
        {
            InitializeComponent();
            LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative)) };

            this.Loaded += new RoutedEventHandler(BindApplicationBar);

            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 1.0);
            SystemTray.SetBackgroundColor(this, ((SolidColorBrush)DataHelper.ConvertToRGB("#048EFF")).Color);

            tProgIndicator = new ProgressIndicator();
            tProgIndicator.IsVisible = true;
            tProgIndicator.Text = "小投资";
            SystemTray.SetProgressIndicator(this, tProgIndicator);
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


        //计算利息
        private void BtnCompute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmount.Text) || string.IsNullOrEmpty(txtStoreTime.Text) || string.IsNullOrEmpty(txtSelBank.Text))
            {
                txtTip.Text = "提示：请您输入存款金额和存款期限，并选择存款银行。";
            }
            else
            {
                if (LLSContacts.SelectedItem != null)
                {
                    var repository = new BankRepositoryBll();
                    var interestResultList = repository.ComputeInterest((Bank)LLSContacts.SelectedItem, DataHelper.GetInteger(txtAmount.Text),
                        DataHelper.GetInteger(txtStoreTime.Text));

                    if (!interestResultList.Any()) return;
                    
                    LongSelInterestDetail.ItemsSource = interestResultList;

                    var bankName=((Bank)LLSContacts.SelectedItem).BankName;

                    lblStoreAmount.Text = "存款金额：" + txtAmount.Text + "(元)  " + bankName;
                    lblStoreTime.Text = "存款期限：" + txtStoreTime.Text+" 年 （到期自动转存）";
                    PivotLayout.SelectedItem = PivotItemResult;
                    ApplicationBar.IsVisible =false;

                    lblStoreTip.Text = "提示：系统会按照每种存款类型自动计算定期转存的利息，如您存10年，系统自动计算3个月，6个月等定期转存的利息。";

                   
                }
            }
        }

        //清空操作
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            txtStoreTime.Text = "";
        }


        private void HideControl()
        {
            foreach (var item in GComputer.Children)
            {
                item.Visibility = Visibility.Collapsed;
            }
            LLSContacts.Visibility = Visibility.Visible;
        }

        private void ShowControl()
        {
            foreach (var item in GComputer.Children)
            {
                item.Visibility = Visibility.Visible;
            }
            LLSContacts.Visibility = Visibility.Collapsed;
        }

        private void txtSelBank_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            HideControl();
        }

        private void LLSContacts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ShowControl();

            if (LLSContacts.SelectedItem != null)
                txtSelBank.Text = ((Bank)LLSContacts.SelectedItem).BankName;
        }

        private void PivotLayout_LoadedPivotItem(object sender, Microsoft.Phone.Controls.PivotItemEventArgs e)
        {
            ShowControl();
        }



        //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataHelper.PromptTip();

            try
            {
                var repository = new BankRepositoryBll();
                var bankList = repository.GetBankList();
                if (bankList != null && bankList.Any())
                {
                    List<AlphaKeyGroup<Bank>> DataSource = AlphaKeyGroup<Bank>.CreateGroups(bankList,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (Bank s) => { return s.BankGroup; }, true);

                    LLSContacts.ItemsSource = DataSource;
                    LLSContacts.Visibility = Visibility.Collapsed;
                }
                listIntroduction.ItemsSource = repository.GetInterestDesc();

            }
            catch (Exception k)
            {

            }
            
        }



        private void PivotLayout_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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