using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallInvestment.Model
{
    public class AppMenu
    {
        /// <summary>
        /// HubTitle 的 Title 属性
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// HubTitle 的 Message 属性
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// HubTitle 的 Notification属性
        /// </summary>
        public string Notification { get; set; }

        /// <summary>
        /// 导航地址
        /// </summary>
        public string NavUrl { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 排序标识
        /// </summary>
        public int OrderFlag { get; set; }

        /// <summary>
        /// 排序标识，字符串
        /// </summary>
        public string OrderFlagStr { get; set; }
    }
}
