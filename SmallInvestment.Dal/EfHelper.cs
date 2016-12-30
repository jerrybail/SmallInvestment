using System.ComponentModel;

namespace SmallInvestment.Dal
{
    public class EfHelper : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        /// 判断本地数据库是否存在
        /// </summary>
        public bool IsExistLocalDb()
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                return db.DatabaseExists();
            }
        }


        /// <summary>
        /// 创建本地数据库
        /// </summary>
        public void CreateLocalDb()
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    db.CreateDatabase();
            }
        }

        /// <summary>
        /// 删除本地数据库
        /// </summary>
        public void DeleteLocalDb()
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists())
                    db.DeleteDatabase();
                
            }
        }


    }
}
