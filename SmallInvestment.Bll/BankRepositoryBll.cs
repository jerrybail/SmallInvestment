using System;
using System.Collections.Generic;
using SmallInvestment.Dal;
using SmallInvestment.Model;
using Microsoft.Phone.Globalization;
using System.Globalization;

namespace SmallInvestment.Bll
{
    public class BankRepositoryBll
    {
        /// <summary>
        /// 获取银行列表 
        /// </summary>
        public List<Bank> GetBankList()
        {
            var repository = new BankRepositoryDal();
            return repository.GetBankList();
        }


        public List<SubDesc> GetInterestDesc()
        {
            var descList = new List<SubDesc>();
            descList.Add(new SubDesc { Name = "到期自动转存：", Desc = "        客户存款到期后，客户如不前往银行办理转存手续，银行可自动将到期的存款本息按相同存期一并转存，不受次数限制，续存期利息按前期到期日利率计算。续存后如不足一个存期客户要求支取存款，续存期间按支取日的活期利率计算该期利息。例如：客户存入一年期定期存款A元，存款到期后该客户并未要求提款或挪作他用，银行将自动将A元及A元在本年度所产生的利息再存为一笔一年期定期存款；如客户在一年零五个月时要求支取本息，则续存期内的五个月利率按照支取日当日利率计算。", Order = 0 });
            return descList;
        }

        /// <summary>
        /// 获取银行信息
        /// </summary>
        public Bank GetBankInfo(int bankId)
        {
            var repository = new BankRepositoryDal();
            return repository.GetBankInfo(bankId);
        }

        /// <summary>
        /// 判断银行利率是否更新
        /// </summary>
        public int IsUpdate(int updateTime)
        {
            var repository = new BankRepositoryDal();
            return repository.IsUpdate(updateTime);
        }

        /// <summary>
        /// 更新银行利率信息
        /// </summary>
        public int UpdateBankInfo(List<Bank> bankInfoList)
        {
            var repository = new BankRepositoryDal();
            foreach (var item in bankInfoList)
            {
                repository.UpdateBankInfo(item);
            }
            return 1;
        }


        /// <summary>
        /// 返回利息计算结果
        /// </summary>
        public List<InterestResult> ComputeInterest(Bank bank, int capital, int depositPeriod)
        {
            var interestList = new List<InterestResult>();

            if (bank.BankID != 0 && bank.CurrentRadio != 0)
            {
                interestList.Add(ComputeCurrentRadiorResult(bank.CurrentRadio, capital, depositPeriod));//活期存款利息

                #region 整存整取利息计算
                interestList.Add(ComputerRadiomResult(bank.Radio3m, capital, depositPeriod == 0 ? 3 : depositPeriod, 3));//3个月整存整取利息
                interestList.Add(ComputerRadiomResult(bank.Radio6m, capital, depositPeriod == 0 ? 6 : depositPeriod, 6));//6个月整存整取利息

                if (bank.Radio1y != 0 && depositPeriod >= 1)
                    interestList.Add(ComputerRadioYResult(bank.Radio1y, capital, depositPeriod == 0 ? 1 : depositPeriod, 1));

                if (bank.Radio2y != 0 && (depositPeriod/2)>0)
                    interestList.Add(ComputerRadioYResult(bank.Radio2y, capital, depositPeriod == 0 ? 2 : depositPeriod, 2));

                if (bank.Radio3y != 0 && (depositPeriod / 3) > 0)
                    interestList.Add(ComputerRadioYResult(bank.Radio3y, capital, depositPeriod == 0 ? 3 : depositPeriod, 3));

                if (bank.Radio5y != 0 && (depositPeriod / 5) > 0)
                    interestList.Add(ComputerRadioYResult(bank.Radio5y, capital, depositPeriod == 0 ? 5 : depositPeriod, 5));
                #endregion

                #region 零存整取利息计算

                #endregion

            }

            return interestList;
        }

        #region 根据利率计算利息
        /// <summary>
        /// 计算活期存款利息
        /// </summary>
        private InterestResult ComputeCurrentRadiorResult(int radio, int capital, int depositPeriod)
        {
            //(Convert.ToSingle(Convert.ToSingle(radio)/10000*(capital*depositPeriod*360))/360).ToString(),
            var radioHq = Convert.ToDouble(radio) / 10000 * capital;
            var amount = Convert.ToDouble(depositPeriod * 360)*radioHq/ 360;
            var item = new InterestResult
            {
                RadioTypeName = "活期存储",
                Radio = ((float)radio / 100).ToString("0.00"),
                InterestMoney = amount.ToString(),
                StoreTime = depositPeriod + "年",
                OrderFlag = 1
            };
            return item;
        }

        /// <summary>
        /// 计算三个月整存 / 六个月整存整取利息 depositPeriod表示月数
        /// </summary>
        private InterestResult ComputerRadiomResult(int radio, int capital, int depositPeriod, int radioM)
        {
            float result = 0;
            float nowCapital = capital;
            for (int i = 0; i < (depositPeriod*12 / radioM); i++)
            {
                result += Convert.ToSingle(Convert.ToSingle(radio) / 10000 * (nowCapital)  / 12)*radioM;
                nowCapital = capital + result;
            }

            var item = new InterestResult
            {
                RadioTypeName = radioM + "个月整存整取",
                InterestMoney = result.ToString("0.00"),
                Radio = ((float) radio/100).ToString(),
                StoreTime= depositPeriod + "年",
                OrderFlag = 0
            };
            return item;
        }

        /// <summary>
        /// 计算年整存整取利息
        /// </summary>
        private InterestResult ComputerRadioYResult(int radio, int capital, int depositPeriod, int radioY)
        {
            float result = 0;
            float nowCapital = capital;
     
            for (int i = 0; i < depositPeriod / radioY; i++)
            {
                result += Convert.ToSingle(Convert.ToSingle(radio) / 10000 * nowCapital) * radioY;
                nowCapital = capital + result;
            }

            var item = new InterestResult
            {
                RadioTypeName = radioY + "年整存整取",
                Radio = ((float) radio/100).ToString("0.00"),
                InterestMoney = result.ToString(),
                StoreTime = depositPeriod + "年",
                OrderFlag = radioY
            };
            return item;
        }


        #endregion



        public class Group<T> : IEnumerable<T>
        {
            public string Key { get; set; }
            public IList<T> Items { get; set; }

            public Group(string key, IEnumerable<T> Item)
            {
                this.Key = key;
                this.Items = new List<T>(Item);
            }

            public override bool Equals(object obj)
            {
                Group<T> NowGroup = obj as Group<T>;
                return (NowGroup != null) && (this.Key.Equals(NowGroup.Key));
            }

            #region

            public IEnumerator<T> GetEnumerator()
            {
                return this.Items.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.Items.GetEnumerator();
            }

            #endregion
        }
    }



    public class AlphaKeyGroup<T> : List<T>
    {
        /// <summary>
        /// The delegate that is used to get the key information.
        /// </summary>
        /// <param name="item">An object of type T</param>
        /// <returns>The key value to use for this object</returns>
        public delegate string GetKeyDelegate(T item);

        /// <summary>
        /// The Key of this group.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="key">The key for this group.</param>
        public AlphaKeyGroup(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Create a list of AlphaGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="slg">The </param>
        /// <returns>Theitems source for a LongListSelector</returns>
        private static List<AlphaKeyGroup<T>> CreateGroups(SortedLocaleGrouping slg)
        {
            List<AlphaKeyGroup<T>> list = new List<AlphaKeyGroup<T>>();

            foreach (string key in slg.GroupDisplayNames)
            {
                list.Add(new AlphaKeyGroup<T>(key));
            }

            return list;
        }

        /// <summary>
        /// Create a list of AlphaGroup<T> with keys set by a SortedLocaleGrouping.
        /// </summary>
        /// <param name="items">The items to place in the groups.</param>
        /// <param name="ci">The CultureInfo to group and sort by.</param>
        /// <param name="getKey">A delegate to get the key from an item.</param>
        /// <param name="sort">Will sort the data if true.</param>
        /// <returns>An items source for a LongListSelector</returns>
        public static List<AlphaKeyGroup<T>> CreateGroups(IEnumerable<T> items, CultureInfo ci, GetKeyDelegate getKey, bool sort)
        {
            SortedLocaleGrouping slg = new SortedLocaleGrouping(ci);
            List<AlphaKeyGroup<T>> list = CreateGroups(slg);

            foreach (T item in items)
            {
                int index = 0;
                if (slg.SupportsPhonetics)
                {
                    //check if your database has yomi string for item
                    //if it does not, then do you want to generate Yomi or ask the user for this item.
                    //index = slg.GetGroupIndex(getKey(Yomiof(item)));
                }
                else
                {
                    index = slg.GetGroupIndex(getKey(item));
                }
                if (index >= 0 && index < list.Count)
                {
                    list[index].Add(item);
                }
            }

            if (sort)
            {
                foreach (AlphaKeyGroup<T> group in list)
                {
                    group.Sort((c0, c1) => { return ci.CompareInfo.Compare(getKey(c0), getKey(c1)); });
                }
            }

            return list;
        }

    }
}
