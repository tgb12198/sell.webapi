using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Common.Enum
{
    public enum EnumValid
    {
        /// <summary>
        /// 有效
        /// </summary>
        [Description("无效")]
        InValid = 0,

        /// <summary>
        /// 无效
        /// </summary>
        [Description("有效")]
        Valid = 1
    }
}
