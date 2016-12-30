using SmallInvestment.Dal;

namespace SmallInvestment.Bll
{
    public class EfRepositoryBll
    {
        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateDataBase()
        {
            var repository = new EfHelper();
            repository.CreateLocalDb();
        }


        /// <summary>
        /// 判断是否存在本地数据库
        /// </summary>
        public static bool IsExistLocalDb()
        {
            var repository = new EfHelper();
            return repository.IsExistLocalDb();
        }

        /// <summary>
        /// 删除本地数据库
        /// </summary>
        public static void DeleteLocalDb()
        {
            var repository = new EfHelper();
            repository.DeleteLocalDb();
        }
    }
}
