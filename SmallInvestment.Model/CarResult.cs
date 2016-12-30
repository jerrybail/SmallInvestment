using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallInvestment.Model
{
    public class CarResult
    {
        /// <summary>
        /// 税费名称
        /// </summary>
        public string CarTaxName { get; set; }

        
        /// <summary>
        /// 税费金额
        /// </summary>
        public double CarTaxAmount { get; set; }
    }

    /// <summary>
    /// 汽车排量
    /// </summary>
    public class CarDisplacement
    {
        public string Name { get; set;}

        public double Displacement { get; set; }
    }
}
