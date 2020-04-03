using sell.web.Common.Enum;
using sell.web.IRepository;
using sell.web.IService;
using sell.web.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Service
{
    public class UserInfoService : IUserInfoSercice
    {
        public IUserInfoRepository _userInfoRepository;
        public UserInfoService(IUserInfoRepository userInfoRepository)
        {
            this._userInfoRepository = userInfoRepository;
        }
        public IEnumerable<UserInfo> GetAllList()
        {
            var list = _userInfoRepository.GetAllList();
            var userList = new List<UserInfo>();
            list.Result.ToList().ForEach(p =>
            {
                userList.Add(
                    new UserInfo
                    {
                        UserId = p.UserId,
                        UserName = p.UserName,
                        UserPwd = p.UserPwd,
                        UserPhone = p.UserPhone,
                        UserFlag = (EnumValid)p.UserFlag,
                        UserGender = p.UserGender,
                        CreateTime = p.CreateTime,
                        UpdateTime = p.UpdateTime
                    });
            });
            return userList;
        }
    }
}
