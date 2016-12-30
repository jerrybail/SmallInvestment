using System.Data.Linq.Mapping;
using System.ComponentModel;

namespace SmallInvestment.Dal.ModelEntity
{
    [Table]
    public class LoanRateEntity : INotifyPropertyChanged, INotifyPropertyChanging
    {
            [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
            public int ID { get; set; }


            #region 贷款利率
            /// <summary>
            /// 六个月贷款利率
            /// </summary>
            [Column]
            public int LoanRate6M { get; set; }

            /// <summary>
            /// 一年贷款利率
            /// </summary>
            [Column]
            public int LoanRate1Y { get; set; }

            /// <summary>
            /// 一至三年利率
            /// </summary>
            [Column]
            public int LoanRate1To3Y { get; set; }

            /// <summary>
            /// 三至五年利率
            /// </summary>
            [Column]
            public int LoanRate3To5Y { get; set; }

            /// <summary>
            /// 五年以上利率
            /// </summary>
            public int LoanRate5YAbove { get; set; }

            /// <summary>
            /// 个人住房公积金贷款利率  五年以下(含五年)
            /// </summary>
           [Column]
            public int HouseFundLoan5Y { get; set; }


            /// <summary>
            /// 个人住房公积金贷款利率 五年以上
            /// </summary>
            [Column]
            public int HouseFundLoan5YAbove { get; set; }


            /// <summary>
            /// 基准利率
            /// </summary>
            [Column]
            public int LoanRateBase { get; set; }


            /// <summary>
            /// 基准利率名称
            /// </summary>
            [Column]
            public string LoanRateBaseName { get; set; }

            /// <summary>
            /// 排序
            /// </summary>
            [Column]
            public int OrderFlag { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            [Column]
            public int State { get; set; }

            /// <summary>
            /// 区分公积金和商贷
            /// </summary>
            [Column]
            public int Flag { get; set; }
            #endregion


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
