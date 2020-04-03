using sell.web.TableModel.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.IRepository
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<UserInfoTableModel>> GetAllList();

        #region 增加
        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        Task<bool> AddUserInfo(UserInfoTableModel userinfo);
        #endregion


        #region  修改
        /// <summary>
        ///修改个人信息
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        Task<bool> UpdateUserInfo(UserInfoTableModel userinfo);
        #endregion

        /// <summary>
        /// 根据Id获取用户数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserInfoTableModel GetUserInfoById(int UserId);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<UserInfoTableModel>> GetUserInfoList(int PageIndex, int PageSize);
    }
}
