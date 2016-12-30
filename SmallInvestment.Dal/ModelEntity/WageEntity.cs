using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace SmallInvestment.Dal.ModelEntity
{
    [Table]
    public class WageEntity : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int ID { get; set; }

        [Column]
        public string City { get; set; }

        [Column]
        public string CityGroup { get; set; }

        #region 住房公积金缴存比例
        /// <summary>
        /// 个人缴纳住房公积金比例
        /// </summary>
        [Column]
        public float HousingFundRadio { get; set; }


        /// <summary>
        /// 公司缓存住房公积金比例
        /// </summary>
        [Column]
        public float HousingFundCompayRadio { get; set; }
        #endregion

        #region 医疗保险缴存比例
        /// <summary>
        /// 个人缴存医疗保险比例
        /// </summary>
        [Column]
        public float MedicareFundRadio { get; set; }


        /// <summary>
        /// 公司缴纳医疗保险比例
        /// </summary>
        [Column]
        public float MedicareFundCompanyRadio { get; set; }
        #endregion

        #region 失业保险缴存比例
        /// <summary>
        /// 个人缴纳失业保险比例
        /// </summary>
        [Column]
        public float UnemploymentFundRadio { get; set; }


        /// <summary>
        /// 公司缴存失业保险比例
        /// </summary>
        [Column]
        public float UnemploymentFundCompanyRadio { get; set; }
        #endregion

        #region 养老保险缴存比例
        /// <summary>
        /// 个人缴纳养老保险比例
        /// </summary>
        [Column]
        public float EndowmentFundRadio { get; set; }


        /// <summary>
        /// 公司缴存养老保险比例
        /// </summary>
        [Column]
        public float EndowmentFundCompanyRadio { get; set; }
        #endregion

        #region 工伤保险缴存比例
        /// <summary>
        /// 个人缴存工伤保险比例
        /// </summary>
        [Column]
        public float InjuryFundRadio { get; set; }

        /// <summary>
        /// 公司缴纳工伤保险比例
        /// </summary>
        [Column]
        public float InjuryFundCompanyRadio { get; set; }
        #endregion

        #region 生育保险缴存比例
        /// <summary>
        /// 个人缴纳生育保险比例
        /// </summary>
        [Column]
        public float BirthFundRadio { get; set; }

        /// <summary>
        /// 公司缴纳生育保险比例
        /// </summary>
        [Column]
        public float BirthFundCompanyRadio { get; set; }
        #endregion

        /// <summary>
        /// 工资最高缴存基数
        /// </summary>
        [Column]
        public int MaxWage { get; set; }

        /// <summary>
        /// 工资最低缓存基数
        /// </summary>
        [Column]
        public int MinWage { get; set; }

        [Column]
        public string MetroColor { get; set; }

        [Column]
        public int UpdateTime { get; set; }

        /// <summary>
        /// 1表示有数据，0表示无数据
        /// </summary>
        public int HasData { get; set; }



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
