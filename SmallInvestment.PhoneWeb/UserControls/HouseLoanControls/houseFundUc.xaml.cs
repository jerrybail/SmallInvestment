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
using SmallInvestment.Common;

namespace SmallInvestment.PhoneWeb.UserControls.HouseLoanControls
{
    public partial class houseFundUc : UserControl
    {
        public houseFundUc()
        {
            InitializeComponent();
            BindLoanTerm();
            BindLoanRate();

            txtLoanAmount.BorderBrush = (SolidColorBrush)DataHelper.ConvertToRGB("#0D1D4D");
            txtLoanRate.BorderBrush = (SolidColorBrush)DataHelper.ConvertToRGB("#0D1D4D");
        }


        /// <summary>
        /// 绑定公积金贷款利率
        /// </summary>
        private void BindLoanRate()
        {
            var repository = new LoanRepository();
            var ldList = repository.GetLoanRate();

            if (ldList != null && ldList.Any())
            {
                ldList = ldList.Where(r => r.Flag == 1).ToList();
                LPLoadFundRate.ItemsSource = ldList;
                LPLoadFundRate.SelectedItem = ldList.FirstOrDefault();
                txtLoanRate.Text = Convert.ToDouble((double)ldList.FirstOrDefault().LoanRateBase / 100).ToString("0.0");
            }
        }


        /// <summary>
        /// 绑定贷款期限
        /// </summary>
        private void BindLoanTerm()
        {
            var repository = new LoanRepository();
            var hltList = repository.GetHouseLoanTermList(30);

            LPLoanTerm.HorizontalAlignment = HorizontalAlignment.Left;
            LPLoanTerm.HorizontalContentAlignment = HorizontalAlignment.Left;
            LPLoanTerm.ItemsSource = hltList;
            LPLoanTerm.SelectedItem = hltList.Where(r => r.Term==120).FirstOrDefault();
        }

        private void LPLoanFundRadio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanFundRate = (LoanRate)LPLoadFundRate.SelectedItem;
            if (selLoanFundRate != null)
                txtLoanRate.Text = Convert.ToDouble((double)selLoanFundRate.LoanRateBase / 100).ToString("0.0");
        }

       

      

    }
}
