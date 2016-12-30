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

namespace SmallInvestment.PhoneWeb.UserControls.CarLoanControls
{
    /// <summary>
    /// 车贷计算
    /// </summary>
    public partial class carLoanUc : UserControl
    {
        public carLoanUc()
        {
            InitializeComponent();

            var repository = new CarLoanRepository();
           
            //贷款折扣
            LPLoanDiscount.ItemsSource = repository.GetCarLoanDiscount();

            //贷款分层
            LPLoanPayLay.ItemsSource = repository.GetCarLoanLayList().OrderBy(r => r.HouseLoanLayValue);

            BindCarLoanRate(0);

            BindCarLoanTerm(0);
            
        }

       

        private void LPLoanDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanRate = (LoanRate)LPLoanBusinessRate.SelectedItem;
            var selHouseLoanTerm = (LoanDiscount)LPLoanDiscount.SelectedItem;

            if (selLoanRate != null && selHouseLoanTerm != null)
                txtLoanRate.Text = (
                    Convert.ToDouble(selHouseLoanTerm.Discount) / 100
                    *
                    Convert.ToDouble(selLoanRate.LoanRateBase) / 100

                    ).ToString("0.00");
        }

        private void LPLoanBusinessRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanRate = (LoanRate)LPLoanBusinessRate.SelectedItem;
            var selHouseLoanTerm = (LoanDiscount)LPLoanDiscount.SelectedItem;

            if (selLoanRate != null && selHouseLoanTerm != null)
                txtLoanRate.Text = Convert.ToDouble(
                    Convert.ToDouble(selHouseLoanTerm.Discount) / 100
                    *
                     Convert.ToDouble(selLoanRate.LoanRateBase) / 100

                    ).ToString("0.0");
        }

        //绑定贷款期限
        private void BindCarLoanTerm(int parms)
        {
            var houseRepository = new LoanRepository();
            //贷款期限
            var loanTermList = houseRepository.GetHouseLoanTermList(5).OrderBy(r=>r.OrderFlag);
            LPLoanTerm.ItemsSource = loanTermList;
            
        }

        //绑定利率
        private void BindCarLoanRate(int parms)
        {
            var repository = new CarLoanRepository();

            //贷款利率
            var loanRateList = repository.GetCarLoanRate().OrderBy(r => r.OrderFlag);
            LPLoanBusinessRate.ItemsSource = loanRateList;      

        }

        private void LPLoanTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selLoanTerm = (HouseLoanTerm)LPLoanTerm.SelectedItem;
            if (selLoanTerm.Term <= 6)
                LPLoanBusinessRate.SelectedIndex = 0;
            else if (selLoanTerm.Term <=12)
                LPLoanBusinessRate.SelectedIndex = 1;
            else if (selLoanTerm.Term <=36)
                LPLoanBusinessRate.SelectedIndex = 2;
            else if (selLoanTerm.Term <=64)
                LPLoanBusinessRate.SelectedIndex = 3;

        }



    }
}
