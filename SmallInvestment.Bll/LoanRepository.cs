using SmallInvestment.Common;
using SmallInvestment.Dal;
using SmallInvestment.Dal.ModelEntity;
using SmallInvestment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallInvestment.Bll
{
    public class LoanRepository
    {
        
        /// <summary>
        /// 住房贷款折扣
        /// </summary>
        public List<LoanDiscount> GetHouseDiscountList()
        {
            List<LoanDiscount> ldList = new List<LoanDiscount>();
            ldList.Add(new LoanDiscount() { Discount = 100, DiscountName = "无折扣", State = 1, OrderFlag = 1, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 110, DiscountName = "1.1倍", State = 1, OrderFlag = 2, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 95, DiscountName = "95折", State = 1, OrderFlag = 3, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 90, DiscountName = "9折", State = 1, OrderFlag = 4, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 85, DiscountName = "85折", State = 1, OrderFlag = 5, Selected = 0 });
            return ldList;
        }


        /// <summary>
        /// 获取房贷概念说明
        /// </summary>
        /// <returns></returns>
        public List<SubDesc> GetHouseIntroduce()
        {
            var txtInList = new List<SubDesc>();
            txtInList.Add(new SubDesc { Name = "等额本金：", Desc = "        等额本金是指一种贷款的还款方式，是在还款期内把贷款数总额等分，每月偿还同等数额的本金和剩余贷款在该月所产生的利息，这样由于每月的还款本金额固定，而利息越来越少，借款人起初还款压力较大，但是随时间的推移每月还款数也越来越少。", Order = 0 });
            txtInList.Add(new SubDesc { Name = "等额本息：", Desc = "        即把按揭贷款的本金总额与利息总额相加，然后平均分摊到还款期限的每个月中，每个月的还款额是固定的，但每月还款额中的本金比重逐月递增、利息比重逐月递减。这种方法是目前最为普遍，也是大部分银行长期推荐的方式。", Order = 1 });
            txtInList.Add(new SubDesc { Name = "等额本金和等额本息的区别：", Desc = "        等额本息的贷款时间越长。 利息就越高。而且时间长。利息会多很多。 差距大！等额本金还款特点是刚刚开始还款的时候每月会比较多， 然后就依次慢慢的变少。等额本息还款特点是每个月还款的钱数都是一样的。", Order = 2 });
            txtInList.Add(new SubDesc { Name = "贷款对比：（以贷款30万， 20年计算）", Desc = "        等额本金的利息为：197318.75 。 第一个月还款2887.50  ，然后每个月逐步变少。等额本息的利息为：238934.18。   每一个月还款2245.56元", Order = 3 });
            return txtInList;
        }

        /// <summary>
        /// 获取贷款类型
        /// </summary>
        /// <returns></returns>
        public List<HouseLoanType> GetHouseLoanTypeList()
        {
            var loanTypeLisy = new List<HouseLoanType>();
            loanTypeLisy.Add(new HouseLoanType() { HouseLoanTypeId = 1, HouseLoanTypeName = "商业贷款" });
            loanTypeLisy.Add(new HouseLoanType() { HouseLoanTypeId = 2, HouseLoanTypeName = "公积金贷款" });
            loanTypeLisy.Add(new HouseLoanType() { HouseLoanTypeId = 3, HouseLoanTypeName = "混和贷款" });
            return loanTypeLisy;
        }


        /// <summary>
        /// 住房贷款期限
        /// </summary>
        public List<HouseLoanTerm> GetHouseLoanTermList(int loanTerm)
        {
            List<HouseLoanTerm> hltList = new List<HouseLoanTerm>();
            hltList.Add(new HouseLoanTerm()
                {
                    Term = 6,
                    TermName = "6个月(6 期）",
                    State = 1,
                    OrderFlag = 0
                });
            for (int i = 1; i <= loanTerm; i++)
            {
                var itemHouseLoanTerm = new HouseLoanTerm()
                {
                    Term = i*12,
                    TermName = i + "年(" + (i * 12) + "期）",
                    State = 1,
                    OrderFlag = i
                };
                hltList.Add(itemHouseLoanTerm);
            }

            return hltList;
        }


        /// <summary>
        /// 更新银行利率信息
        /// </summary>
        public int UpdateLoanRateInfo(List<LoanRate> loanRateList)
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                foreach (var item in loanRateList)
                {
                    var query = (from m in db.LoanRateList
                                 where m.ID == item.ID
                                 select m).FirstOrDefault();

                    if (query == null)
                    {
                        query = new LoanRateEntity()
                        {
                            ID = item.ID,
                            LoanRate6M = item.LoanRate6M,
                            LoanRate1Y = item.LoanRate1Y,
                            LoanRate1To3Y = item.LoanRate1To3Y,
                            LoanRate3To5Y = item.LoanRate3To5Y,
                            LoanRate5YAbove = item.LoanRate5YAbove,
                            HouseFundLoan5Y = item.HouseFundLoan5Y,
                            HouseFundLoan5YAbove = item.HouseFundLoan5YAbove,
                            LoanRateBase = item.LoanRateBase,
                            LoanRateBaseName = item.LoanRateBaseName,
                            Flag = item.Flag,
                            OrderFlag = item.OrderFlag,
                            State = item.State
                        };
                        db.LoanRateList.InsertOnSubmit(query);
                        db.SubmitChanges();
                    }
                    else
                    {
                        query.ID = item.ID;
                        query.LoanRate6M = item.LoanRate6M;
                        query.LoanRate1Y = item.LoanRate1Y;
                        query.LoanRate1To3Y = item.LoanRate1To3Y;
                        query.LoanRate3To5Y = item.LoanRate3To5Y;
                        query.LoanRate5YAbove = item.LoanRate5YAbove;
                        query.HouseFundLoan5Y = item.HouseFundLoan5Y;
                        query.HouseFundLoan5YAbove = item.HouseFundLoan5YAbove;
                        query.LoanRateBase = item.LoanRateBase;
                        query.LoanRateBaseName = item.LoanRateBaseName;
                        query.Flag = item.Flag;
                        query.OrderFlag = item.OrderFlag;
                        query.State = item.State;
                        db.SubmitChanges();
                    }
                }
            }
            return 1;
        }



        /// <summary>
        /// 获取公积金贷款利率
        /// </summary>
        /// <returns></returns>
        public List<LoanRate> GetLoanRate()
        {
            var loanRateList = new List<LoanRate>();
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return null;

                var query = from m in db.LoanRateList
                            select m;

                loanRateList.AddRange(query.Select(item => new LoanRate
                {
                    ID = item.ID,
                    Flag = item.Flag,
                    OrderFlag = item.OrderFlag,
                    State = item.State,
                    LoanRate6M = item.LoanRate6M,
                    LoanRate1Y = item.LoanRate1Y,
                    LoanRate1To3Y = item.LoanRate1To3Y,
                    LoanRate3To5Y = item.LoanRate3To5Y,
                    LoanRate5YAbove = item.LoanRate5YAbove,
                    LoanRateBase = item.LoanRateBase,
                    LoanRateBaseName = item.LoanRateBaseName,
                    HouseFundLoan5Y = item.HouseFundLoan5Y,
                    HouseFundLoan5YAbove = item.HouseFundLoan5YAbove
                }));

                return loanRateList;
            }
        }


        /// <summary>
        /// 获取每个贷款期限对应的贷款利率
        /// </summary>
        private int GetHouseLoanRate(int i, LoanRate houseLoanRate)
        {
            if (i == 1)
                return houseLoanRate.LoanRate1Y;

            if (i > 1 && i <= 3)
                return houseLoanRate.LoanRate1To3Y;

            if (i > 3 && i <= 5)
                return houseLoanRate.LoanRate3To5Y;

            if (i > 5)
                return houseLoanRate.LoanRate5YAbove;

            return 0;
        }

     

        //等额本息还款法:
        public HouseLoanResult ComputeHouseLoan_debx(double loanAmount, double loanRate, int loanTerm)
        {
            HouseLoanResult houseLoanResultItem = new HouseLoanResult();
            var testMonthRate = loanRate / 12;//月利率
            loanAmount = loanAmount * 10000;

            houseLoanResultItem.HouseLoanAmount =  Convert.ToDecimal(loanAmount);
            houseLoanResultItem.HouseLoanTerm = loanTerm;

            var monthPay =  loanAmount * testMonthRate * Math.Pow((1 + testMonthRate), loanTerm) / (Math.Pow((1 + testMonthRate), loanTerm) - 1);

            houseLoanResultItem.HouseLoanPaybankAmount = DataHelper.GetDecimal(loanTerm * monthPay);
            //总利息=还款月数×每月月供额-贷款本金
            houseLoanResultItem.HouseLoanInterest = DataHelper.GetDecimal( loanTerm * monthPay - loanAmount);

            
            for (int i = 1; i <= loanTerm; i++)
            {
                var houseLoanMonthItem = new HouseLoanMonth();

                //每月月供额=〔贷款本金×月利率×(1＋月利率)＾还款月数〕÷〔(1＋月利率)＾还款月数-1〕
                houseLoanMonthItem.MonthTime = i;
                houseLoanMonthItem.MonthPayYg = monthPay;
                houseLoanMonthItem.MonthPayYgStr = houseLoanMonthItem.MonthPayYg.ToString("0.00");

                //每月应还本金=贷款本金×月利率×(1+月利率)^(还款月序号-1)÷〔(1+月利率)^还款月数-1〕
                houseLoanMonthItem.MonthPayBj = loanAmount * testMonthRate * Math.Pow((1 + testMonthRate), (i - 1)) / (Math.Pow((1 + testMonthRate), loanTerm) - 1);
                houseLoanMonthItem.MonthPayBjStr = houseLoanMonthItem.MonthPayBj.ToString("0.00");

                //每月应还利息=贷款本金×月利率×((1+月利率)^还款月数-(1+月利率)^(还款月序号-1)〕÷〔(1+月利率)^还款月数-1〕
                houseLoanMonthItem.MonthPayLx = loanAmount * testMonthRate * (Math.Pow((1 + testMonthRate), (loanTerm)) - Math.Pow((1 + testMonthRate), (i - 1))) / (Math.Pow((1 + testMonthRate), loanTerm) - 1);
                houseLoanMonthItem.MonthPayLxStr = houseLoanMonthItem.MonthPayLx.ToString("0.00");

                houseLoanResultItem.HouseLoanMonthList.Add(houseLoanMonthItem);
            }

            return houseLoanResultItem;
        }

        private decimal Round(decimal dec)
        {
            return decimal.Round(dec, 2);
        }

        //等额本金还款法:
        //每月月供额=(贷款本金÷还款月数)+(贷款本金-已归还本金累计额)×月利率
        //每月应还本金=贷款本金÷还款月数
        //每月应还利息=剩余本金×月利率=(贷款本金-已归还本金累计额)×月利率
        //每月月供递减额=每月应还本金×月利率=贷款本金÷还款月数×月利率

        //总利息=
        //说明:月利率=年利率÷12      15^4=15×15×15×15(15的4次方,即4个15相乘的意思)
        public HouseLoanResult ComputeHouseLoan_debj(double loanAmount, double loanRate, int loanTerm)
        {
            HouseLoanResult houseLoanResultItem = new HouseLoanResult();
            loanAmount = loanAmount * 10000;
            var testMonthRate = loanRate / 12;//月利率
            //总利息
            var testIntertest = Convert.ToDecimal(((loanAmount / loanTerm + loanAmount * testMonthRate) + loanAmount / loanTerm * (1 + testMonthRate)) / 2 * loanTerm - loanAmount);

            houseLoanResultItem.HouseLoanAmount =Convert.ToDecimal( loanAmount);
            houseLoanResultItem.HouseLoanTerm = loanTerm;
            houseLoanResultItem.HouseLoanInterest = Round(testIntertest);
            houseLoanResultItem.HouseLoanPaybankAmount = Round(houseLoanResultItem.HouseLoanAmount + testIntertest);
        
            var backedAmount = 0.00;//已归还金额

            for (int i = 1; i <= loanTerm; i++)
            {

                //每月月供额=(贷款本金÷还款月数)+(贷款本金-已归还本金累计额)×月利率
                //每月应还本金=贷款本金÷还款月数
                //每月应还利息=剩余本金×月利率=(贷款本金-已归还本金累计额)×月利率
                //每月月供递减额=每月应还本金×月利率=贷款本金÷还款月数×月利率
                var houseLoanMonthItem = new HouseLoanMonth();

                houseLoanMonthItem.MonthTime = i;
                houseLoanMonthItem.MonthPayYg = (loanAmount / loanTerm) + (loanAmount - backedAmount) * testMonthRate;
                houseLoanMonthItem.MonthPayYgStr = houseLoanMonthItem.MonthPayYg.ToString("0.00");

                houseLoanMonthItem.MonthPayBj = loanAmount / loanTerm;
                houseLoanMonthItem.MonthPayBjStr = houseLoanMonthItem.MonthPayBj.ToString("0.00");

                houseLoanMonthItem.MonthPayLx = (loanAmount - backedAmount) * testMonthRate;
                houseLoanMonthItem.MonthPayLxStr = houseLoanMonthItem.MonthPayLx.ToString("0.00");

                houseLoanMonthItem.MonthPayDj = (loanAmount / loanTerm) * testMonthRate;
                houseLoanMonthItem.MonthPayDjStr = houseLoanMonthItem.MonthPayDj.ToString("0.00");

                backedAmount += houseLoanMonthItem.MonthPayBj;
                houseLoanResultItem.HouseLoanMonthList.Add(houseLoanMonthItem);

              
            }


            return houseLoanResultItem;

        }



    }
}
