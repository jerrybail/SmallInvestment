using System.Collections.Generic;
using System.Linq;
using Microsoft.Phone.Data.Linq;
using SmallInvestment.Dal.ModelEntity;
using SmallInvestment.Model;

namespace SmallInvestment.Dal
{
    public class WageRepositoryDal
    {
        /// <summary>
        /// 获取各城市工次扣缴比例
        /// </summary>
        public List<WageRadio> GetWageList()
        {
            var wageList = new List<WageRadio>();
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                if (db.DatabaseExists() == false)
                    return null;

                if (db.WageList.Any())
                {
                    var query = from m in db.WageList select m;
                    wageList.AddRange(query.Select(item => new WageRadio
                    {
                        ID = item.ID,
                        City = item.City,
                        CityGroup = item.CityGroup,
                        HousingFundRadio = item.HousingFundRadio,
                        HousingFundCompayRadio = item.HousingFundCompayRadio,
                        MedicareFundRadio = item.MedicareFundRadio,
                        MedicareFundCompanyRadio = item.MedicareFundCompanyRadio,
                        UnemploymentFundRadio = item.UnemploymentFundRadio,
                        UnemploymentFundCompanyRadio = item.UnemploymentFundCompanyRadio,
                        EndowmentFundRadio = item.EndowmentFundRadio,
                        EndowmentFundCompanyRadio = item.EndowmentFundCompanyRadio,
                        InjuryFundRadio = item.InjuryFundRadio,
                        InjuryFundCompanyRadio = item.InjuryFundCompanyRadio,
                        BirthFundRadio = item.BirthFundRadio,
                        BirthFundCompanyRadio = item.BirthFundCompanyRadio,
                        MetroColor = item.MetroColor,
                        UpdateTime = item.UpdateTime,
                        MaxWage = item.MaxWage,
                        MinWage = item.MinWage
                    }));

                    return wageList;
                }
                return  null;
            }
        }


        /// <summary>
        /// 更新 工资扣缴比例信息
        /// </summary>
        public int UpdateWageInfo(WageRadio wage)
        {
            using (var db = new BankDataContext(BankDataContext.DBConnectionString))
            {
                var query = (from m in db.WageList
                             where m.ID == wage.ID
                             select m).FirstOrDefault();

                if (query == null)
                {
                    query = new WageEntity()
                    {
                         ID=wage.ID,
                         City = wage.City,
                         CityGroup =wage.CityGroup,
                         HousingFundRadio =wage.HousingFundRadio,
                         HousingFundCompayRadio =wage.HousingFundCompayRadio,
                         MedicareFundRadio =wage.MedicareFundRadio,
                         MedicareFundCompanyRadio =wage.MedicareFundCompanyRadio,
                         UnemploymentFundRadio =wage.UnemploymentFundRadio,
                         UnemploymentFundCompanyRadio =wage.UnemploymentFundCompanyRadio,
                         EndowmentFundRadio =wage.EndowmentFundRadio,
                         EndowmentFundCompanyRadio =wage.EndowmentFundCompanyRadio,
                         InjuryFundRadio =wage.InjuryFundRadio,
                         InjuryFundCompanyRadio =wage.InjuryFundCompanyRadio,
                         BirthFundRadio =wage.BirthFundRadio,
                         BirthFundCompanyRadio =wage.BirthFundCompanyRadio,
                         MetroColor =wage.MetroColor,
                         UpdateTime =wage.UpdateTime,
                         MaxWage = wage.MaxWage,
                         MinWage = wage.MinWage
                    };
                    db.WageList.InsertOnSubmit(query);
                    db.SubmitChanges();
                }
                else
                {
                    query.City = wage.City;
                    query.CityGroup = wage.CityGroup;
                    query.HousingFundRadio = wage.HousingFundRadio;
                    query.HousingFundCompayRadio = wage.HousingFundCompayRadio;
                    query.MedicareFundRadio = wage.MedicareFundRadio;
                    query.MedicareFundCompanyRadio = wage.MedicareFundCompanyRadio;
                    query.UnemploymentFundRadio = wage.UnemploymentFundRadio;
                    query.UnemploymentFundCompanyRadio = wage.UnemploymentFundCompanyRadio;
                    query.EndowmentFundRadio = wage.EndowmentFundRadio;
                    query.EndowmentFundCompanyRadio = wage.EndowmentFundCompanyRadio;
                    query.InjuryFundRadio = wage.InjuryFundRadio;
                    query.InjuryFundCompanyRadio = wage.InjuryFundCompanyRadio;
                    query.BirthFundRadio = wage.BirthFundRadio;
                    query.BirthFundCompanyRadio = wage.BirthFundCompanyRadio;
                    query.MetroColor = wage.MetroColor;
                    query.UpdateTime = wage.UpdateTime;
                    query.MaxWage = wage.MaxWage;
                    query.MinWage = wage.MinWage;
                    db.SubmitChanges();
                }
            }
            return 1;
        }



    }
}
