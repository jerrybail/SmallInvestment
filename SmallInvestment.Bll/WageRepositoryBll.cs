using SmallInvestment.Dal;
using SmallInvestment.Model;
using System.Collections.Generic;
using System.Linq;

namespace SmallInvestment.Bll
{
    public class WageRepositoryBll
    {
        /// <summary>
        /// 个人工资扣缴
        /// </summary>
        public List<WageResult> ComputerWageResult(int wage, WageRadio wageRadio)
        {
            int wageTrue = wage;
            if (wage > wageRadio.MaxWage)
                wageTrue = wageRadio.MaxWage;

            if (wage < wageRadio.MinWage)
                wageTrue = wageRadio.MinWage;

            var wageResultList = new List<WageResult>();
            wageResultList.Add(ComputerUnemploymentFund(wageTrue, wageRadio));
            wageResultList.Add(ComputerEndowmentFund(wageTrue, wageRadio));
            wageResultList.Add(ComputerHousingFund(wageTrue, wageRadio));
            wageResultList.Add(ComputerMedicareFund(wageTrue, wageRadio));
            wageResultList.Add(ComputerBirthFund(wageTrue, wageRadio));
            wageResultList.Add(ComputerInjuryFund(wageTrue, wageRadio));
            

            foreach (var item in wageResultList)
            {
                item.PersonFundRadioStr = (item.PersonFundRadio*100).ToString();
                item.CompanyFundRadioStr = (item.CompanyFundRadio*100).ToString();
            }

            var difference = wage - wageResultList.Sum(r => r.PersonFund);
            if (difference > 3500)
                wageResultList.Add(ComputIncomeTax(difference - 3500));

            return wageResultList;
        }


        /// <summary>
        /// 更新城市工资扣缴比例信息
        /// </summary>
        public int UpateWageInfo(List<WageRadio> wageRadioList)
        {
            var repository = new WageRepositoryDal();
            foreach (var item in wageRadioList)
            {
                repository.UpdateWageInfo(item);
            }
            return 1;
        }

        /// <summary>
        /// 获取城市工资扣缴比例列表
        /// </summary>
        public List<WageRadio> GetWageRadioList()
        {
            var repository = new WageRepositoryDal();
            return repository.GetWageList();
        }


        /// <summary>
        /// 获取扣税说明
        /// </summary>
        public List<SubDesc> GetWageTaxIntroduce()
        {
            var descList = new List<SubDesc>();
            descList.Add(new SubDesc { Name = "", Desc = "社会保障体系包括社会保险、社会救济、社会福利、社会优抚安置和国有企业下岗职工基本生活保障和再就业等方面，其中社会保险包括养老保险、医疗保险、失业保险、工伤保险和生育保险五个项目。", Order = 0 });
            descList.Add(new SubDesc { Name = "养老保险：", Desc = "        全称社会基本养老保险，是国家和社会根据一定的法律和法规，为解决劳动者在达到国家规定的解除劳动义务的劳动年龄界限，或因年老丧失劳动能力退出劳动岗位后的基本生活而建立的一种社会保险制度。是社会保障制度的重要组成部分，是社会保险五大险种中最重要的险种之一。养老保险的目的是为保障老年人的基本生活需求，为其提供稳定可靠的生活来源。", Order = 1 });
            descList.Add(new SubDesc { Name = "医疗保险：", Desc = "        医疗保险指通过国家立法，按照强制性社会保险原则基本医疗保险费应由用人单位和职工个人按时足额缴纳。不按时足额缴纳的，不计个人帐户，基本医疗保险统筹基金不予支付其医疗费用。以北京市医疗保险缴费比例为例：用人单位每月按照其缴费总基数的10%缴纳，职工按照本人工资的2%+3块钱的大病统筹缴纳。医疗保险是为补偿疾病所带来的医疗费用的一种保险。职工因疾病、负伤、生育时，由社会或企业提供必要的医疗服务或物质帮助的社会保险。如中国的公费医疗、劳保医疗。中国职工的医疗费用由国家、单位和个人共同负担，以减轻企业负担，避免浪费。发生保险责任责任事故需要进行治疗是按比例付保险金", Order = 2 });
            descList.Add(new SubDesc { Name = "失业保险：", Desc = "        指国家通过立法强制实行的，由社会集中建立基金，对因失业而暂时中断生活来源的劳动者提供物质帮助进而保障失业人员失业期间的基本生活，促进其再就业的制度。", Order = 3 });
            descList.Add(new SubDesc { Name = "工伤保险：", Desc = "        是指劳动者在工作中或在规定的特殊情况下，遭受意外伤害或患职业病导致暂时或永久丧失劳动能力以及死亡时，劳动者或其遗属从国家和社会获得物质帮助的一种社会保险制度。", Order = 4 });
            descList.Add(new SubDesc { Name = "生育保险：", Desc = "        国家通过立法，在怀孕和分娩的妇女劳动者暂时中断劳动时，由国家和社会提供医疗服务、生育津贴和产假的一种社会保险制度，国家或社会对生育的职工给予必要的经济补偿和医疗保健的社会保险制度。我国生育保险待遇主要包括两项。一是生育津贴，二是生育医疗待遇。人社部《生育保险办法（征求意见稿）》从2012年11月20日起面向社会公开征求意见。意见稿明确，生育险待遇将不再限户籍，单位不缴生育险须掏生育费。", Order = 5 });
            descList.Add(new SubDesc { Name = "住房公积金：", Desc = "        住房公积金是单位及其在职职工缴存的长期住房储金，是住房分配货币化、社会化和法制化的主要形式。住房公积金制度是国家法律规定的重要的住房社会保障制度，具有强制性、互助性、保障性。单位和职工个人必须依法履行缴存住房公积金的义务。 这里的单位包括国家机关、国有企业、城镇集体企业、外商投资企业、城镇私营企业及其他城镇企业、事业单位、民办非企业单位、社会团体。2011年10月27日，住建部副部长齐骥表示，住建部正在联合各个部门，研究修订公积金条例工作中，放开个人提取公积金用于支付住房租金的规定。2013年部分城市出台办法，允许患有重大疾病的职工或其直系亲属提取公积金救急。", Order = 6 });
            return descList;
        }


        /// <summary>
        /// 养老
        /// </summary>
        private WageResult ComputerEndowmentFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "养老",
                PersonFundRadio = wageRadio.EndowmentFundRadio,
                PersonFund = wage*wageRadio.EndowmentFundRadio,
                PersonFundStr = (wage*wageRadio.EndowmentFundRadio).ToString("F2"),
                PersonFundControlID = "txtPersonE",

                CompanyFundRadio = wageRadio.EndowmentFundCompanyRadio,
                CompanyFund = wage*wageRadio.EndowmentFundCompanyRadio,
                CompanyFundStr = (wage*wageRadio.EndowmentFundCompanyRadio).ToString("F2"),
                CompanyFundControlID ="txtCompanyE",
                OrderFlag = 1,
                TypeFlag = 1
            };
        }


        /// <summary>
        /// 住房
        /// </summary>
        private WageResult ComputerHousingFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "住房",
                PersonFundRadio = wageRadio.HousingFundRadio,
                PersonFund = wage*wageRadio.HousingFundRadio,
                PersonFundStr = (wage*wageRadio.HousingFundRadio).ToString("F2"),
                PersonFundControlID ="txtPersonH",

                CompanyFundRadio = wageRadio.HousingFundCompayRadio,
                CompanyFund = wage*wageRadio.HousingFundCompayRadio,
                CompanyFundStr = (wage * wageRadio.HousingFundCompayRadio).ToString("F2"),
                CompanyFundControlID ="txtCompanyH",
                OrderFlag = 6,
                TypeFlag = 1
            };
        }


        /// <summary>
        /// 医疗
        /// </summary>
        private WageResult ComputerMedicareFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "医疗",
                PersonFundRadio = wageRadio.MedicareFundRadio,
                PersonFund = wage*wageRadio.MedicareFundRadio+3,
                PersonFundStr = (wage*wageRadio.MedicareFundRadio).ToString("F2"),
                PersonFundControlID="txtPersonM",

                CompanyFundRadio = wageRadio.MedicareFundCompanyRadio,
                CompanyFund = wage*wageRadio.MedicareFundCompanyRadio,
                CompanyFundStr = (wage * wageRadio.MedicareFundCompanyRadio).ToString("F2"),
                CompanyFundControlID ="txtCompanyM",
                OrderFlag = 2,
                TypeFlag = 1
            };
        }


        /// <summary>
        /// 工伤
        /// </summary>
        private WageResult ComputerInjuryFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "工伤",
                PersonFund = wage*wageRadio.InjuryFundRadio,
                PersonFundRadio = wageRadio.InjuryFundRadio,
                PersonFundStr="",
                PersonFundControlID="txtPersonI",

                CompanyFundRadio = wageRadio.InjuryFundCompanyRadio,
                CompanyFund = wage * wageRadio.InjuryFundCompanyRadio,
                CompanyFundStr = (wage * wageRadio.InjuryFundCompanyRadio).ToString("F2"),
                CompanyFundControlID = "txtCompanyI",
                OrderFlag = 4,
                TypeFlag=1
            };
        }

        /// <summary>
        /// 生育
        /// </summary>
        private WageResult ComputerBirthFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "生育",
                PersonFundRadio = wageRadio.BirthFundRadio,
                PersonFund = wage * wageRadio.BirthFundRadio,
                PersonFundStr = "",
                PersonFundControlID="txtPersonB",
                

                CompanyFundRadio = wageRadio.BirthFundCompanyRadio,
                CompanyFund = wage * wageRadio.BirthFundCompanyRadio,
                CompanyFundStr = (wage * wageRadio.BirthFundCompanyRadio).ToString("F2"),
                CompanyFundControlID="txtCompanyB",
                OrderFlag = 5,
                TypeFlag=1
            };
        }

        /// <summary>
        /// 失业
        /// </summary>
        private WageResult ComputerUnemploymentFund(int wage, WageRadio wageRadio)
        {
            return new WageResult
            {
                FundName = "失业",
                PersonFundRadio = wageRadio.UnemploymentFundRadio,
                PersonFund = wage*wageRadio.UnemploymentFundRadio,
                PersonFundStr = (wage * wageRadio.UnemploymentFundRadio).ToString("F2"),
                PersonFundControlID="txtPersonU",

                CompanyFundRadio = wageRadio.UnemploymentFundCompanyRadio,
                CompanyFund = wage*wageRadio.UnemploymentFundCompanyRadio,
                CompanyFundStr = (wage * wageRadio.UnemploymentFundCompanyRadio).ToString("F2"),
                CompanyFundControlID = "txtCompanyU",

                OrderFlag = 3,
                TypeFlag=1
            };
        }

        /// <summary>
        /// 个税
        /// </summary>
        private WageResult ComputIncomeTax(float wage)
        {
            float result = 0;
            var wageResult = new WageResult();
            //1	不超过1500搜索元的部分 3% 
            if (wage <= 1500)
                result += wage * 0.03f;

            //2	超过1500元至4500元的部分 10%	
            if (wage>1500 && wage <= 4500)
                result += wage * 0.10f - 105;

            //3	超过4500元至9000元的部分 20%
            if (wage>4500 && wage <= 9000)
                result += wage * 0.20f - 555;

            //4	超过9000元至35000元的部分 25%
            if (wage>9000 && wage <= 35000)
                result += wage * 0.25f - 1005;

            //5	超过35000元至55000元的部分 30%
            if (wage>35000 && wage <= 55000)
                result += wage * 0.30f - 2755;

            //6	超过55000元至80000元的部分    35%
            if (wage>55000 && wage <= 80000)
                result += wage * 0.35f - 5505;

            //7	超过80000的部分	 45%
            if (wage >80000)
                result += wage * 0.45f - 13505;

            wageResult.FundName = "个税";
            wageResult.PersonFund = result;
            wageResult.PersonFundStr = result.ToString();
            wageResult.OrderFlag = 7;
            wageResult.TypeFlag = 2;
            return wageResult;
        }
    }
}