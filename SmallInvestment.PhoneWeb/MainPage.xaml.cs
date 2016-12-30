using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;
using System.Net.NetworkInformation;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using SmallInvestment.Bll;
using SmallInvestment.Common;
using SmallInvestment.Model;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Microsoft.Phone.Shell;
using Coding4Fun.Toolkit.Controls;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Phone.Tasks;
using Windows;
using Windows.UI.ViewManagement;
using System.Windows.Controls;


namespace SmallInvestment.PhoneWeb
{
    public partial class MainPage : PhoneApplicationPage
    {
       

        private ProgressIndicator tProgIndicator;
        public MainPage()
        {
            InitializeComponent();
            LayoutRoot.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri("/Assets/Tiles/bgImage.png", UriKind.Relative)) };

            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 1.0);
            SystemTray.SetBackgroundColor(this,((SolidColorBrush)DataHelper.ConvertToRGB("#D24726")).Color);

            tProgIndicator = new ProgressIndicator();
            tProgIndicator.IsVisible = true;
            tProgIndicator.Text = "小投资";
            SystemTray.SetProgressIndicator(this, tProgIndicator);       
            this.Loaded += new RoutedEventHandler(BindApplicationBar);
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


            ApplicationBarIconButton btnUpdate = new ApplicationBarIconButton() { Text = "更新数据", IconUri = new Uri("/Assets/AppBarImages/refresh.png", UriKind.Relative) };    
            ApplicationBar.Buttons.Add(btnUpdate);
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            ApplicationBarIconButton btnMark = new ApplicationBarIconButton() { Text = "给我评分", IconUri = new Uri("/Assets/AppBarImages/like.png", UriKind.Relative) };
            ApplicationBar.Buttons.Add(btnMark);
            btnMark.Click += new EventHandler(btnMark_Click);

            ApplicationBarIconButton btnAbout = new ApplicationBarIconButton() { Text = "关于", IconUri = new Uri("/Assets/AppBarImages/questionmark.png", UriKind.Relative) };
            ApplicationBar.Buttons.Add(btnAbout);
            btnAbout.Click += new EventHandler(btnAbout_Click);    
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Views/About.xaml", UriKind.Relative));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())//如果已连网
                AsyncData("update");
            else
                new ToastPrompt()
                {
                    Height = 55,
                    Message = "请您连接 Wi-Fi 进行数数据更新。",
                    VerticalContentAlignment = VerticalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    FontSize = 17,
                    TextOrientation = System.Windows.Controls.Orientation.Vertical,
                    Background = (SolidColorBrush)DataHelper.ConvertToRGB("#D24726"),//#58A9DE
                    Foreground = new SolidColorBrush(Colors.White),
                    Opacity = 1.0
                }.Show();
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
        #endregion
        
       
        //当本页成为活动页面时触发OnNavigateTo事件
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //整个项目初始化时同步一下数据
            if (EfRepositoryBll.IsExistLocalDb() == false)
            {
                if (NetworkInterface.GetIsNetworkAvailable())//如果已连网
                    AsyncData("create");
                else
                    new ToastPrompt()
                    {
                        Height = 55,
                        Message = "请您连接 Wi-Fi，小投资需要同步银行利率数据。",
                        VerticalContentAlignment = VerticalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontSize = 17,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Background = (SolidColorBrush)DataHelper.ConvertToRGB("#D24726"),//#58A9DE
                        Foreground = new SolidColorBrush(Colors.White),
                        Opacity = 1.0
                    }.Show();
            }

            BindMenue();
        }

        #region BindMenue
        private void BindMenue()
        {
            var menuList = new List<AppMenu>
            {
                new AppMenu {ImgUrl = "/Assets/Tiles/bankImg.png",Message = "",NavUrl = "/Views/ComputeInterest.xaml",Title = "利息计算", OrderFlagStr = "常用"},             
                new AppMenu {ImgUrl = "/Assets/Tiles/houseloanImg.png", Message = "", NavUrl = "/Views/HouseLoan.xaml", Title = "房贷计算", OrderFlagStr = "常用"},
                new AppMenu {ImgUrl = "/Assets/Tiles/carloanImg.png", Message = "", NavUrl = "/Views/CarLoan.xaml", Title = "车贷计算", OrderFlagStr = "常用"},
                new AppMenu {ImgUrl = "/Assets/Tiles/wageImg.png", Message = "", NavUrl = "/Views/WageTax.xaml", Title = "工资扣税", OrderFlagStr = "常用"}  
            };


            List<AlphaKeyGroup<AppMenu>> DataSource = AlphaKeyGroup<AppMenu>.CreateGroups(menuList,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (AppMenu s) => { return s.OrderFlagStr; }, true);

            LLSContacts.ItemsSource = DataSource;
           
        }
        #endregion

        //同步数据 EfRepositoryBll.DeleteLocalDb();  创建数据库  EfRepositoryBll.CreateDataBase();
        public void AsyncData(string operStr)
        {
            try
            {
                SmallInvestmentWs.SmallInWbsSoapClient myClient = new SmallInvestmentWs.SmallInWbsSoapClient();
                if (myClient != null)
                {
                    myClient.GetBankListCompleted += client_GetBankListCompleted; //银行存储利率
                    myClient.GetWageListCompleted += client_GetWageListCompleted; //城市工资扣缴比列
                    myClient.GetLoanRateListCompleted += client_GetLoanRateListCompleted; //贷款利率

                    var repository = new BankRepositoryBll();
                    var wageRepository = new WageRepositoryBll();
                    var loanRepository = new LoanRepository();

                    if (operStr == "create")
                        EfRepositoryBll.CreateDataBase();//创建数据库

                    myClient.GetBankListAsync(DataHelper.WbsPassStr());//同步银行利率
                    myClient.GetWageListAsync(DataHelper.WbsPassStr());//同步各城市工资扣缴比例
                    myClient.GetLoanRateListAsync(DataHelper.WbsPassStr());//同步贷款利率(商贷、公积金贷款)

                    new ToastPrompt()
                    {
                        Height = 55,
                        Message = "数据更新完毕",
                        VerticalContentAlignment = VerticalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Top,
                        FontSize = 17,
                        TextOrientation = System.Windows.Controls.Orientation.Vertical,
                        Background = (SolidColorBrush)DataHelper.ConvertToRGB("#D24726"),//#58A9DE
                        Foreground = new SolidColorBrush(Colors.White),
                        Opacity = 1.0
                    }.Show();

                    myClient.CloseAsync();
                }
            }
            catch (Exception k)
            {

            }
            finally { }
        }
        
        /// <summary>
        /// 通过WebService 获取银行列表,并更新到本地数据库中
        /// </summary>
        private void client_GetBankListCompleted(object sender, SmallInvestmentWs.GetBankListCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null && e.Result.Any())
                {
                    var repository = new BankRepositoryBll();
                    var bankUpdateList = e.Result.Select(item => new Bank
                    {
                        BankID = item.BankID,
                        BankName = item.BankName,
                        BankGroup = item.BankGroup,
                        CurrentRadio = item.CurrentRadio,
                        Radio3m = item.Radio3m,
                        Radio6m = item.Radio6m,
                        Radio1y = item.Radio1y,
                        Radio2y = item.Radio2y,
                        Radio3y = item.Radio3y,
                        Radio5y = item.Radio5y,
                        Disperse_1yRadio = item.Disperse_1yRadio,
                        Disperse_3yRadio = item.Disperse_3yRadio,
                        Disperse_5yRadio = item.Disperse_5yRadio,
                        MetroColor = item.MetroColor,
                        UpdateTime = DataHelper.GetInteger(DateTime.Now.ToString("yyyyMMdd"))
                    }).ToList();
                    repository.UpdateBankInfo(bankUpdateList);
                }
            }
            catch (Exception k)
            { 
            }
            finally { }

        }

        /// <summary>
        /// 通过WebService获取贷款利率
        /// </summary>
        private void client_GetLoanRateListCompleted(object sender, SmallInvestmentWs.GetLoanRateListCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null && e.Result.Any())
                {
                    var loanRepository = new LoanRepository();
                    var loanRateList = e.Result.Select(item => new LoanRate()
                    {
                        LoanRateBase = item.LoanRate,
                        LoanRateBaseName = item.LoanRateName,
                        ID = item.ID,
                        Flag = item.Flag,
                        OrderFlag = item.OrderFlag,
                        LoanRate6M = item.LoanRate6M,
                        LoanRate1Y = item.LoanRate1Y,
                        LoanRate1To3Y = item.LoanRate1To3Y,
                        LoanRate3To5Y = item.LoanRate3To5Y,
                        LoanRate5YAbove = item.LoanRate5YAbove,
                        HouseFundLoan5Y = item.HouseFundLoan5Y,
                        HouseFundLoan5YAbove = item.HouseFundLoan5YAbove
                    }).ToList();
                    loanRepository.UpdateLoanRateInfo(loanRateList);
                }
            }
            catch (Exception k)
            {

            }
            finally { }

        }


        /// <summary>
        /// 通过WebService 获取各城市工资扣缴比例
        /// </summary>
        private void client_GetWageListCompleted(object sender, SmallInvestmentWs.GetWageListCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null && e.Result.Any())
                {
                    var repository = new WageRepositoryBll();
                    var wageRadioUpdateList = e.Result.Select(item => new WageRadio()
                    {
                        ID = item.ID,
                        City = item.City,
                        CityGroup = item.CityGroup,
                        HousingFundRadio = item.HousingFundRadio,
                        HousingFundCompayRadio = item.HousingFundCompayRadio,
                        MedicareFundRadio = item.MedicareFundRadio,
                        MedicareFundCompanyRadio = item.MedicareFundCompanyRadio,
                        UnemploymentFundRadio = item.UnemploymentFundRadio,
                        UnemploymentFundCompanyRadio = item.UnemploymentFundCompanyRadio,
                        EndowmentFundRadio = item.EndowmentFundRadio,
                        EndowmentFundCompanyRadio = item.EndowmentFundCompanyRadio,
                        InjuryFundRadio = item.InjuryFundRadio,
                        InjuryFundCompanyRadio = item.InjuryFundCompanyRadio,
                        BirthFundRadio = item.BirthFundRadio,
                        BirthFundCompanyRadio = item.BirthFundCompanyRadio,
                        MetroColor = item.MetroColor,
                        UpdateTime = DataHelper.GetInteger(DateTime.Now.ToString("yyyyMMdd")),
                        MaxWage = DataHelper.GetInteger(item.MaxWage),
                        MinWage = DataHelper.GetInteger(item.MinWage)
                    }).ToList();
                    repository.UpateWageInfo(wageRadioUpdateList);
                }
            }
            catch(Exception k)
            { 
            }
            finally { }
        }


        /// <summary>
        /// 通过WebService 获取指南列表
        /// </summary>
        private void client_GetGuideListCompleted(object sender, SmallInvestmentWs.GetGuideListCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null && e.Result.Any())
                {
                    ProLoadGuide.Visibility = Visibility.Visible;

                    var guideList = e.Result.Where(r=>r.Title!="").Select(item => new GuideEntity()
                    {
                       ImageUrl = item.ImageUrl,
                       Title = item.Title,
                       Desc = item.Desc,
                       ContentUrl = item.ContentUrl,
                       OrderFlag="指南"
                    }).ToList();

                    List<AlphaKeyGroup<GuideEntity>> DataSource = AlphaKeyGroup<GuideEntity>.CreateGroups(guideList,
                    System.Threading.Thread.CurrentThread.CurrentUICulture,
                    (GuideEntity s) => { return s.OrderFlag; }, true);


                    LLGuide.ItemsSource = DataSource;           
                    ProLoadGuide.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception k)
            {
            }
            finally { }
        }

        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ht1 = (Button)sender;
            this.NavigationService.Navigate(new Uri(ht1.Tag.ToString(), UriKind.Relative));
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var panorama = (Panorama)sender;
            switch (panorama.SelectedIndex)
            {
                case 0:
                    SystemTray.SetBackgroundColor(this, ((SolidColorBrush)DataHelper.ConvertToRGB("D24726")).Color);
                    break;

                case 1:
                    if (NetworkInterface.GetIsNetworkAvailable()) //如果已连网
                    {
                        if (LLGuide.ItemsSource==null)
                        { 
                            SmallInvestmentWs.SmallInWbsSoapClient myClient = new SmallInvestmentWs.SmallInWbsSoapClient();
                            myClient.GetGuideListCompleted += client_GetGuideListCompleted; //获取指南列表
                            myClient.GetGuideListAsync(DataHelper.WbsPassStr());//获取指南列表     
                            myClient.CloseAsync();
                        }
    
                    }
                    else
                    {
                        new ToastPrompt()
                        {
                            Height = 55,
                            Message = "请您连接 Wi-Fi，指南需要联网查看。",
                            VerticalContentAlignment = VerticalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Top,
                            FontSize = 17,
                            TextOrientation = System.Windows.Controls.Orientation.Vertical,
                            Background = (SolidColorBrush)DataHelper.ConvertToRGB("#D24726"),//#58A9DE
                            Foreground = new SolidColorBrush(Colors.White),
                            Opacity = 1.0
                        }.Show();
                    }
                    
                    break;
            }
        }

        private void LLGuide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LLGuide.SelectedItem != null)
            {
                var selItem = (GuideEntity)LLGuide.SelectedItem;
                this.NavigationService.Navigate(new Uri(string.Format("/Views/SeekPage.xaml?Title={0}&ContentUrl={1}",selItem.Title,selItem.ContentUrl), UriKind.Relative));
            }
        }

       


     


    }
}