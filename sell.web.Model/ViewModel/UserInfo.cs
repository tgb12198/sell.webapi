using sell.web.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Model.ViewModel
{
    public class UserInfo
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserPhone { get; set; }
        public int UserGender { get; set; }
        public EnumValid UserFlag { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public UserInfo()
        {
            this.UserName = string.Empty;
            this.UserPwd = string.Empty;
            this.UserPhone = string.Empty;
            this.UserGender = 0;
            this.UserFlag = EnumValid.Valid;
            this.CreateTime = new DateTime(1900, 1, 1);
            this.UpdateTime = new DateTime(1900, 1, 1);
        }
    }
}
