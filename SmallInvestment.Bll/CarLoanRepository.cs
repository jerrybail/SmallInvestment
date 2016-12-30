using SmallInvestment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallInvestment.Bll
{
    /// <summary>
    /// 车贷计算
    /// </summary>
    public class CarLoanRepository
    {
        /// <summary>
        /// 获取贷款利率
        /// </summary>
        public List<LoanRate> GetCarLoanRate()
        {
            return new List<LoanRate>(){
              new LoanRate(){LoanRateBase=640,LoanMonth=60, LoanRateBaseName="五年及以内",OrderFlag=3},
              new LoanRate(){LoanRateBase=615,LoanMonth=36, LoanRateBaseName="三年及以内",OrderFlag=2},
              new LoanRate(){LoanRateBase=600,LoanMonth=12, LoanRateBaseName="一年及以内",OrderFlag=1},
              new LoanRate(){LoanRateBase=560,LoanMonth=6, LoanRateBaseName="六个月及以内",OrderFlag=0}};
        }


        /// <summary>
        /// 车贷折扣
        /// </summary>
        public List<LoanDiscount> GetCarLoanDiscount()
        {
            List<LoanDiscount> ldList = new List<LoanDiscount>();
            ldList.Add(new LoanDiscount() { Discount = 100, DiscountName = "无折扣", State = 1, OrderFlag = 1, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 95, DiscountName = "95折", State = 1, OrderFlag = 2, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 90, DiscountName = "9折", State = 1, OrderFlag = 3, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 85, DiscountName = "85折", State = 1, OrderFlag = 4, Selected = 0 });
            ldList.Add(new LoanDiscount() { Discount = 80, DiscountName = "8折", State = 1, OrderFlag = 5, Selected = 0 });
            return ldList;
        }

       

        /// <summary>
        /// 获取按揭
        /// </summary>
        public List<HouseLoanLay> GetCarLoanLayList()
        {
            var houseLoanLayList = new List<HouseLoanLay>();
            houseLoanLayList.Add(new HouseLoanLay() { HouseLoanLayId = 8, HouseLoanLayName = "7成", HouseLoanLayValue = 70 });
            houseLoanLayList.Add(new HouseLoanLay() { HouseLoanLayId = 7, HouseLoanLayName = "6成", HouseLoanLayValue = 60 });
            houseLoanLayList.Add(new HouseLoanLay() { HouseLoanLayId = 6, HouseLoanLayName = "5成", HouseLoanLayValue = 50 });
            houseLoanLayList.Add(new HouseLoanLay() { HouseLoanLayId = 5, HouseLoanLayName = "4成", HouseLoanLayValue = 40 });
            houseLoanLayList.Add(new HouseLoanLay() { HouseLoanLayId = 4, HouseLoanLayName = "3成", HouseLoanLayValue = 30 });
            return houseLoanLayList;
        }


        /// <summary>
        /// 获取汽车排量
        /// </summary>
        public List<CarDisplacement> GetCarDisplacement()
        {
            return new List<CarDisplacement>()
            {
                new CarDisplacement(){Name="1.0L及以下"},
                new CarDisplacement(){Name="1.0L-1.6L(含)"},
                new CarDisplacement(){Name="2.1L-2.5L(含)"},
                new CarDisplacement(){Name="2.6L-3.0L(含)"},
                new CarDisplacement(){Name="3.1L-4.0L(含)"},
                new CarDisplacement(){Name="4.0L以上"},

            };
        }


        /// <summary>
        /// 计算车贷
        /// </summary>
        public HouseLoanResult ComputeCarLoan(double loanAmount, double loanRate, int loanTerm)
        {
            HouseLoanResult loanResultItem = new HouseLoanResult();

            loanAmount = loanAmount * 10000;
            var test = loanAmount * (loanRate) / 12;
            var test1 = Math.Pow((1 + loanRate / 12), loanTerm);
            var test2 = Math.Pow((1 + loanRate / 12), loanTerm) - 1;
            var test3 = test * test1 / test2;
            var testMonthRate = loanRate / 12;//月利率

            loanResultItem.HouseLoanAmount = Convert.ToDecimal(loanAmount);
            loanResultItem.HouseLoanTerm = loanTerm;
            loanResultItem.HouseLoanPaybankMonth = Round(Convert.ToDecimal(test3));
            loanResultItem.HouseLoanPaybankAmount = Round(loanResultItem.HouseLoanPaybankMonth * loanTerm);
            loanResultItem.HouseLoanInterest = loanResultItem.HouseLoanPaybankAmount - Convert.ToDecimal(loanAmount);


            var backedAmount = 0.00;//已归还金额

            for (int i = 1; i <= loanTerm; i++)
            {
                var houseLoanMonthItem = new HouseLoanMonth();

                houseLoanMonthItem.MonthTime = i;
               

                houseLoanMonthItem.MonthPayBj = loanAmount / loanTerm;
                houseLoanMonthItem.MonthPayBjStr = houseLoanMonthItem.MonthPayBj.ToString("0.00");

                houseLoanMonthItem.MonthPayLx = Convert.ToDouble(loanResultItem.HouseLoanInterest/loanTerm);
                houseLoanMonthItem.MonthPayLxStr = houseLoanMonthItem.MonthPayLx.ToString("0.00");

                houseLoanMonthItem.MonthPayYg = Convert.ToDouble(loanResultItem.HouseLoanPaybankMonth);
                houseLoanMonthItem.MonthPayYgStr = houseLoanMonthItem.MonthPayYg.ToString("0.00");


                backedAmount += houseLoanMonthItem.MonthPayYg;
                loanResultItem.HouseLoanMonthList.Add(houseLoanMonthItem);
            }

            return loanResultItem;
        }


        /// <summary>
        /// 计算购车税费
        /// </summary>
        public List<CarResult> ComputerCarTax(double carAmount)
        {
            carAmount = carAmount * 10000;

            List<CarResult> carResultList = new List<CarResult> ();
            CarResult carResult= new CarResult ();
            //车辆购置税=不含税车价*10%=含税车价/（1+17%）*10%  购车款/(1+17%)×购置税率(10%) 
            carResult.CarTaxName = "车辆购置税";
            carResult.CarTaxAmount=carAmount/1.17*0.10;
            carResultList.Add(carResult);

            //通常商家提供的一条龙服务收费约500元，个人办理约 373元，其中工商验证、出库150元、移动证30元、环保 卡3元、拓号费40元、行驶证相片20元、托盘费130元 
            CarResult carResult1 = new CarResult();
            carResult1.CarTaxName = "上牌费用";
            carResult1.CarTaxAmount = carAmount / 1.17 * 0.10;
            carResultList.Add(carResult1);

            //各省不统一，以北京为例(单位/年)。
            //1.0L(含)以下300元； 1.0-1.6L(含)420元； 1.6-2.0L(含)480元； 2.0-2.5L(含)900元； 2.5-3.0L(含)1920元； 3.0-4.0L(含)3480元； 4.0L以上5280元；不足一年按当年剩余月算。 
            CarResult carResult2 = new CarResult();
            carResult2.CarTaxName = "车船使用税";
            carResult2.CarTaxAmount = carAmount / 1.17 * 0.10;
            carResultList.Add(carResult2);

            //家用6座以下950元/年，家用6座及以上1100元/年
            CarResult carResult3 = new CarResult();
            carResult3.CarTaxName = "交强险费用";
            carResult3.CarTaxAmount = carAmount / 1.17 * 0.10;
            carResultList.Add(carResult3);


            return carResultList;
            
        }

        private decimal Round(decimal dec)
        {
            return decimal.Round(dec, 2);
        }



    }
}
