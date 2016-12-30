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
using System.Windows.Media;
using SmallInvestment.Common;
using SmallInvestment.Model;

namespace SmallInvestment.PhoneWeb.UserControls.HouseLoanControls
{
    public partial class houseGroupUc : UserControl
    {
        public houseGroupUc()
        {
            InitializeComponent();

            //贷款期限
            BindLoanTerm();

            //商贷利率
            BindBusinessLoanRate();

            //公积金利率
            BindLoanFundRate();

            //折扣
            BindLoanDiscount();
        }


        /// <summary>
        /// 绑定商业贷款利率
        /// </summary>
        private void BindBusinessLoanRate()
        {
            var repository = new LoanRepository();
            var ldList = repository.GetLoanRate();

            if (ldList != null && ldList.Any())
            {
                ldList = ldList.Where(r => r.Flag == 2).ToList();
                LPLoanRate.ItemsSource = ldList;
                LPLoanRate.SelectedItem = ldList.FirstOrDefault();
                txtLoanRate.Text = ((float)ldList.FirstOrDefault().LoanRateBase / 100).ToString();
            }
        }

        /// <summary>
        /// 绑定公积金贷款利率
        /// </summary>
        private void BindLoanFundRate()
        {
            var repository = new LoanRepository();
            var ldList = repository.GetLoanRate();

            if (ldList != null && ldList.Any())
            {
                ldList = ldList.Where(r => r.Flag == 1).ToList();
                LPLoadFundRate.ItemsSource = ldList;
                LPLoadFundRate.SelectedItem = ldList.FirstOrDefault();
                txtLoanFundRate.Text = Convert.ToDouble((double)ldList.FirstOrDefault().LoanRateBase / 100).ToString("0.0");
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
            LPLoanTerm.SelectedItem = hltList.Where(r => r.Term == 120).FirstOrDefault();
        }

        /// <summary>
        /// 绑定贷款折扣
        /// </summary>
        private void BindLoanDiscount()
        {
            var repository = new LoanRepository();
            var ldList = repository.GetHouseDiscountList();

            LPLoanDiscount.ItemsSource = ldList;
            LPLoanDiscount.SelectedItem = ldList.FirstOrDefault();
        }

        private void LPLoanDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanRate = (LoanRate)LPLoanRate.SelectedItem;
            var selHouseLoanTerm = (LoanDiscount)LPLoanDiscount.SelectedItem;

            if (selLoanRate != null && selHouseLoanTerm != null)
                txtLoanRate.Text = (
                    Convert.ToDouble(selHouseLoanTerm.Discount) / 100
                    *
                    Convert.ToDouble(selLoanRate.LoanRateBase) / 100

                    ).ToString("0.00");

        }



    }
}
