using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.TableModel.TableModel
{
    public class UserInfoTableModel
    {
        [Column(Name = "user_id")]
        public long UserId { get; set; }

        [Column(Name = "user_name")]
        public string UserName { get; set; }

        [Column(Name = "user_pwd")]
        public string UserPwd { get; set; }

        [Column(Name = "user_phone")]
        public string UserPhone { get; set; }

        [Column(Name = "user_gender")]
        public int UserGender { get; set; }

        [Column(Name = "user_flag")]
        public int UserFlag { get; set; }

        [Column(Name = "create_time")]
        public DateTime CreateTime { get; set; }

        [Column(Name = "update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
