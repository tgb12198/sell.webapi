using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Common.Enum
{
    public enum EnumCodeResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Ok=0,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail =1,
    }
}
