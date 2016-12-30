namespace SmallInvestment.Model
{
    public class Bank
    {
        /// <summary>
        /// 银行编号
        /// </summary>
        public int BankID { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行分组
        /// </summary>
        public string BankGroup { get; set; }

        #region 活期存款
        /// <summary>
        /// 活期存款利率
        /// </summary>
        public int CurrentRadio { get; set; }
        #endregion


        #region 整存整取利率
        /// <summary>
        /// 三月存款利率 整存整取三个月
        /// </summary>
        public int Radio3m { get; set; }

        /// <summary>
        /// 六个月存款利率 整存整取半年
        /// </summary>
        public int Radio6m { get; set; }


        /// <summary>
        /// 一年存款利率 整存整取一年
        /// </summary>
        public int Radio1y { get; set; }

        /// <summary>
        /// 二年存款利率 整存整取二年
        /// </summary>
        public int Radio2y { get; set; }

        /// <summary>
        /// 三年存款利率 整存整取三年
        /// </summary>
        public int Radio3y { get; set; }

        /// <summary>
        /// 五年存款利率 整存整取五年
        /// </summary>
        public int Radio5y { get; set; }

        #endregion


        #region   零存整取、整存零取、存本取息 利率
        /// <summary>
        /// 零存整取、整存零取、存本取息一年
        /// </summary>
        public int Disperse_1yRadio { get; set; }


        /// <summary>
        /// 零存整取、整存零取、存本取息三年
        /// </summary>
        public int Disperse_3yRadio { get; set; }


        /// <summary>
        /// 零存整取、整存零取、存本取息五年
        /// </summary>
        public int Disperse_5yRadio { get; set; }

        #endregion


        //色彩
        public string MetroColor { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int UpdateTime { get; set; }

    }


    /// <summary>
    /// 贷款利率
    /// </summary>
    public class LoanRate
    {
        public int ID{get;set;}
        #region 贷款利率
        /// <summary>
        /// 六个月贷款利率
        /// </summary>
        public int LoanRate6M { get; set; }

        /// <summary>
        /// 一年贷款利率
        /// </summary>
        public int LoanRate1Y { get; set; }

        /// <summary>
        /// 一至三年利率
        /// </summary>
        public int LoanRate1To3Y { get; set; }

        /// <summary>
        /// 三至五年利率
        /// </summary>
        public int LoanRate3To5Y { get; set; }

        /// <summary>
        /// 五年以上利率
        /// </summary>
        public int LoanRate5YAbove { get; set; }

        /// <summary>
        /// 个人住房公积金贷款利率  五年以下(含五年)
        /// </summary>
        public int HouseFundLoan5Y { get; set; }


        /// <summary>
        /// 个人住房公积金贷款利率 五年以上
        /// </summary>
        public int HouseFundLoan5YAbove { get; set; }


        /// <summary>
        /// 基准利率
        /// </summary>
        public int LoanRateBase { get; set; }


        /// <summary>
        /// 基准利率名称
        /// </summary>
        public string LoanRateBaseName { get; set; }


        public int Flag { get; set; }

        public int State { get; set; }


        /// <summary>
        /// 排序
        /// </summary>
        public int OrderFlag { get; set; }
        #endregion

        #region 扩展属性
        public int LoanMonth { get;set; }
        #endregion
    }

    /// <summary>
    /// 贷款打折
    /// </summary>
    public class LoanDiscount
    {
        /// <summary>
        /// 折扣名称
        /// </summary>
        public string DiscountName { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public int Discount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 排序标识
        /// </summary>
        public int OrderFlag { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public int Selected { get; set; }
    }

    /// <summary>
    /// 贷款结果
    /// </summary>
    public class LoanResult
    {
        //贷款总额
        public decimal LoanAmount { get; set; }

        //贷款期限
        public int LoanTerm { get; set; }

        //每月偿还
        public decimal LoanPaybankMonth { get; set; }

        //支付利息
        public decimal LoanInterest { get; set; }

        //还款总额
        public decimal LoanPaybankAmount { get; set; }
    }

    /// <summary>
    ///  名词解释
    /// </summary>
    public class SubDesc
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Order { get; set; }
    }
   
}
