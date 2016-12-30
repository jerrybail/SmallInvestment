using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallInvestment.Model
{
    public class HouseLoanResult
    {
        //贷款总额
        public decimal HouseLoanAmount { get; set; }

        //贷款期限
        public int HouseLoanTerm { get; set; }

        //每月偿还 针对 等额本息
        public decimal HouseLoanPaybankMonth { get; set; }

        //支付利息
        public decimal HouseLoanInterest { get; set; }

        //还款总额
        public decimal HouseLoanPaybankAmount { get; set; }

        //首期偿还 针对 等额本金
        public decimal HouseLoanFirstPaybank { get; set; }

        //偿还类型 1等额本息，2等额本金
        public int HouseLoanPayBankType { get; set; }

        //每月还款明细
        public List<HouseLoanMonth> HouseLoanMonthList = new List<HouseLoanMonth>();
    }


    //每月还款
    public class HouseLoanMonth
    {
        //其次
        public int MonthTime { get; set; }

        //每月月供
        public double MonthPayYg { get; set; }
        public string MonthPayYgStr { get; set; }

        //每月偿还本金
        public double MonthPayBj { get; set; }
        public string MonthPayBjStr { get; set; }

        //每月应还利息
        public double MonthPayLx { get; set; }
        public string MonthPayLxStr { get; set; }

        //每月月供递减
        public double MonthPayDj { get; set; }
        public string MonthPayDjStr { get;set;}
    }


    /// <summary>
    /// 房贷期限
    /// </summary>
    public class HouseLoanTerm
    {
        public int Term { get; set; }
        public string TermName { get; set; }
        public int State { get; set; }
        public int OrderFlag { get; set; }
    }


    /// <summary>
    /// 房贷类型
    /// </summary>
    public class HouseLoanType
    {
        public int HouseLoanTypeId { get; set; }

        public string HouseLoanTypeName { get; set; }
    }


    /// <summary>
    /// 房贷按揭
    /// </summary>
    public class HouseLoanLay
    {
        public int HouseLoanLayId { get; set; }

        public string HouseLoanLayName { get; set; }

        public int HouseLoanLayValue { get; set; }
        
    }



}
