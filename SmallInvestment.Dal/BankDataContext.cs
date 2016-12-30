
using System.Data.Linq;
using SmallInvestment.Dal.ModelEntity;
using SmallInvestment.Model;

namespace SmallInvestment.Dal
{
    public class BankDataContext:DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/ToDo.sdf";

        public BankDataContext(string connectionString)
            : base(connectionString)
        { }

        public Table<BankEntity> BankList;
        public Table<WageEntity> WageList;
        public Table<LoanRateEntity> LoanRateList;
    }
}
