using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Common.Comm
{
    public class BaseResponse<T>
    {
        /// <summary>
        /// 0:成功；1：失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
