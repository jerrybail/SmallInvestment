namespace SmallInvestment.Model
{
    public  class WageResult
    {
        /// <summary>
        /// 保险名称
        /// </summary>
        public string FundName { get; set; }

        //个人缴存比例
        public float PersonFundRadio { get; set; }
        public string PersonFundRadioStr { get; set; }

        //个人缴存金额
        public float PersonFund { get; set; }
        public string PersonFundStr { get; set; }

        //公司缴存比例
        public float CompanyFundRadio { get; set; }
        public string CompanyFundRadioStr { get; set; }

        //公司缴存金额
        public float CompanyFund { get; set; }
        public string CompanyFundStr { get; set; }

        //排序标识
        public int OrderFlag { get; set; }

        /// <summary>
        /// 类型 1 表示五险一金，2 表示个税扣缴
        /// </summary>
        public int TypeFlag { get; set; }

        public string PersonFundControlID { get; set; }
        public string CompanyFundControlID { get; set; }

        public string ControlContent { get; set; }
    }

    public class WageRadio
    {
        public int ID { get; set; }

        public string City { get; set; }

        public string CityGroup { get; set; }

        #region 住房公积金缴存比例
        /// <summary>
        /// 个人缴纳住房公积金比例
        /// </summary>
        public float HousingFundRadio { get; set; }


        /// <summary>
        /// 公司缓存住房公积金比例
        /// </summary>
        public float HousingFundCompayRadio { get; set; }
        #endregion

        #region 医疗保险缴存比例
        /// <summary>
        /// 个人缴存医疗保险比例
        /// </summary>
        public float MedicareFundRadio { get; set; }


        /// <summary>
        /// 公司缴纳医疗保险比例
        /// </summary>
        public float MedicareFundCompanyRadio { get; set; }
        #endregion

        #region 失业保险缴存比例
        /// <summary>
        /// 个人缴纳失业保险比例
        /// </summary>
        public float UnemploymentFundRadio { get; set; }


        /// <summary>
        /// 公司缴存失业保险比例
        /// </summary>
        public float UnemploymentFundCompanyRadio { get; set; }
        #endregion

        #region 养老保险缴存比例
        /// <summary>
        /// 个人缴纳养老保险比例
        /// </summary>
        public float EndowmentFundRadio { get; set; }


        /// <summary>
        /// 公司缴存养老保险比例
        /// </summary>
        public float EndowmentFundCompanyRadio { get; set; }
        #endregion

        #region 工伤保险缴存比例
        /// <summary>
        /// 个人缴存工伤保险比例
        /// </summary>
        public float InjuryFundRadio { get; set; }

        /// <summary>
        /// 公司缴纳工伤保险比例
        /// </summary>
        public float InjuryFundCompanyRadio { get; set; }
        #endregion

        #region 生育保险缴存比例
        /// <summary>
        /// 个人缴纳生育保险比例
        /// </summary>
        public float BirthFundRadio { get; set; }

        /// <summary>
        /// 公司缴纳生育保险比例
        /// </summary>
        public float BirthFundCompanyRadio { get; set; }
        #endregion

        /// <summary>
        /// 工资最高缴存基数
        /// </summary>
        public int MaxWage { get; set; }

        /// <summary>
        /// 工资最低缴存基数
        /// </summary>
        public int MinWage { get; set; }

        public string MetroColor { get; set; }

        public int UpdateTime { get; set; }

        /// <summary>
        /// 1表示有数据，0表示无数据
        /// </summary>
        public int HasData { get; set; }
    }
}
