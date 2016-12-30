using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SmallInvestment.Dal.ModelEntity;
using SmallInvestment.Model;

namespace SmallInvestment.Dal
{
    public class BankRepositoryDal:INotifyPropertyChanged
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
        /// 获取银行列表
        /// </summary>
        public List<Bank> GetBankList()
        {
            var bankList = new List<Bank>();
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return null;

                var query = from m in db.BankList
                            select m;

                bankList.AddRange(query.Select(item => new Bank
                {
                    BankID = item.BankID, 
                    BankName = item.BankName,
                    BankGroup = item.BankGroup,
                    CurrentRadio = item.CurrentRadio,
                    Radio3m = item.Radio3m, 
                    Radio6m = item.Radio6m, 
                    Radio1y = item.Radio1y, 
                    Radio2y = item.Radio2y, 
                    Radio3y = item.Radio3y, 
                    Radio5y = item.Radio5y, 
                    Disperse_1yRadio = item.Disperse_Radio1y, 
                    Disperse_3yRadio = item.Disperse_Radio3y, 
                    Disperse_5yRadio = item.Disperse_Radio5y, 
                    MetroColor = item.MetroColor
                }));

                return bankList;
            }
        }

        /// <summary>
        /// 判断银行利率是否更新
        /// </summary>
        public int IsUpdate(int updateTime)
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return 0;

                var query = from m in db.BankList
                    where m.UpdateTime == updateTime
                    select m;

                return query.Count();
            }
        }


        /// <summary>
        /// 获取银行信息
        /// </summary>
        public Bank GetBankInfo(int bankId)
        {
            var bank = new Bank();

            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return null;

                var query = (from m in db.BankList
                             where m.BankID == bankId
                             select m).FirstOrDefault();

                if (query != null)
                {
                    //基本信息
                    bank.BankID = query.BankID;
                    bank.BankName = query.BankName;
                    bank.CurrentRadio = query.CurrentRadio;

                    bank.Radio3m = query.Radio3m;
                    bank.Radio6m = query.Radio6m;
                    bank.Radio1y = query.Radio1y;
                    bank.Radio2y = query.Radio2y;
                    bank.Radio3y = query.Radio3y;
                    bank.Radio5y = query.Radio5y;

                    bank.Disperse_1yRadio = query.Disperse_Radio1y;
                    bank.Disperse_3yRadio = query.Disperse_Radio3y;
                    bank.Disperse_5yRadio = query.Disperse_Radio5y;
                    bank.MetroColor = query.MetroColor;
                    bank.UpdateTime = query.UpdateTime;
                }
            }

            return bank;
        }

        /// <summary>
        /// 更新银行利率信息
        /// </summary>
        public int UpdateBankInfo(Bank bankInfo)
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return 0;

                var query = (from m in db.BankList
                    where m.BankID == bankInfo.BankID
                    select m).FirstOrDefault();

                if (query == null)
                {
                    query = new BankEntity()
                    {
                        BankID = bankInfo.BankID,
                        BankName = bankInfo.BankName,
                        BankGroup = bankInfo.BankGroup,
                        CurrentRadio = bankInfo.CurrentRadio, //活期存款 利率
                        Radio3m = bankInfo.Radio3m, //整存整取 利率
                        Radio6m = bankInfo.Radio6m,
                        Radio1y = bankInfo.Radio1y,
                        Radio2y = bankInfo.Radio2y,
                        Radio3y = bankInfo.Radio3y,
                        Radio5y = bankInfo.Radio5y,
                        Disperse_Radio1y = bankInfo.Disperse_1yRadio, //零存整去 利率
                        Disperse_Radio3y = bankInfo.Disperse_3yRadio,
                        Disperse_Radio5y = bankInfo.Disperse_5yRadio,
                        MetroColor =bankInfo.MetroColor,
                        UpdateTime = bankInfo.UpdateTime
                    };
                    db.BankList.InsertOnSubmit(query);
                    db.SubmitChanges();
                }
                else
                {
                    query.BankName = bankInfo.BankName;
                    query.CurrentRadio = bankInfo.CurrentRadio; //活期存款 利率
                    query.BankGroup = bankInfo.BankGroup;
                    query.Radio3m = bankInfo.Radio3m; //整存整取利率
                    query.Radio6m = bankInfo.Radio6m;
                    query.Radio1y = bankInfo.Radio1y;
                    query.Radio2y = bankInfo.Radio2y;
                    query.Radio3y = bankInfo.Radio3y;
                    query.Radio5y = bankInfo.Radio5y;
                    query.Disperse_Radio1y = bankInfo.Disperse_1yRadio; //零存整去 利率
                    query.Disperse_Radio3y = bankInfo.Disperse_3yRadio;
                    query.Disperse_Radio5y = bankInfo.Disperse_5yRadio;
                    query.MetroColor = bankInfo.MetroColor;
                    db.SubmitChanges();

                }
            }
            return 1;
        }


         

    }
}
