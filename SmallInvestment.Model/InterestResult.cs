namespace SmallInvestment.Model
{
    /// <summary>
    /// 利息计算结果
    /// </summary>
    public class InterestResult
    {
        /// <summary>
        /// 利率类型
        /// </summary>
        public string RadioTypeName { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        public string Radio{get;set;}

        /// <summary>
        /// 利息
        /// </summary>
        public string InterestMoney { get; set; }

        /// <summary>
        /// 存期
        /// </summary>
        public string StoreTime { get; set; }

        /// <summary>
        /// 排序标识
        /// </summary>
        public int OrderFlag { get; set; }

    }

}
