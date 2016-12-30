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
    //房贷商贷
    public partial class houseBusinessUc : UserControl
    {
        public houseBusinessUc()
        {
            InitializeComponent();

            BindLoanTerm();
            BindBusinessLoanRate();
            BindLoanDiscount();
        }

        /// <summary>
        /// 绑定贷款利率
        /// </summary>
        private void BindBusinessLoanRate()
        {
            var repository = new LoanRepository();
            var ldList = repository.GetLoanRate();

            if (ldList != null && ldList.Any())
            {
                ldList = ldList.Where(r=>r.Flag==2).ToList();
                LPLoanBusinessRate.ItemsSource = ldList;
                LPLoanBusinessRate.SelectedItem = ldList.FirstOrDefault();
                txtLoanRate.Text = ((float)ldList.FirstOrDefault().LoanRateBase / 100).ToString();
            }
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
        /// 当选择贷款期限
        /// </summary>
        private void LPLoanTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listLoanTermPicker = (sender as ListPicker);
            var selectedItem = listLoanTermPicker.SelectedItem;

            if (selectedItem != null)
            {
                HouseLoanTerm selectedHouseLoanTerm = (HouseLoanTerm)selectedItem;
            }
        }

       


        private void LPLoanBusinessRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanRate = (LoanRate)LPLoanBusinessRate.SelectedItem;
            var selHouseLoanTerm = (LoanDiscount)LPLoanDiscount.SelectedItem;

            if (selLoanRate != null && selHouseLoanTerm!=null)
                txtLoanRate.Text = Convert.ToDouble(
                    Convert.ToDouble(selHouseLoanTerm.Discount) / 100 
                    *
                     Convert.ToDouble(selLoanRate.LoanRateBase) / 100
                    
                    ).ToString("0.0");
        }

        private void LPLoanDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanRate = (LoanRate)LPLoanBusinessRate.SelectedItem;
            var selHouseLoanTerm = (LoanDiscount)LPLoanDiscount.SelectedItem;

            if (selLoanRate!=null &&selHouseLoanTerm != null)
                txtLoanRate.Text = (
                    Convert.ToDouble(selHouseLoanTerm.Discount) / 100 
                    *
                    Convert.ToDouble(selLoanRate.LoanRateBase) / 100
                    
                    ).ToString("0.00");
        }
        
        



    }
}
