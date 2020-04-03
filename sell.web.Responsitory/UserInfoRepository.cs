using sell.database;
using sell.web.Common.Comm;
using sell.web.Common.Enum;
using sell.web.IRepository;
using sell.web.Model.ViewModel;
using sell.web.TableModel;
using sell.web.TableModel.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        public IDbContext _dbContext;
        public UserInfoRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> AddUserInfo(UserInfoTableModel userinfo)
        {
            string query = "insert into user(user_id, user_name,user_pwd,user_phone,user_gender,user_flag,create_time,update_time) values(@user_id,@user_name,@user_pwd,@user_phone,@user_gender,@user_flag,@create_time,@update_time)";
            int row = await _dbContext.Update<UserInfoTableModel>(query, userinfo);

            return row > 0 ? true : false;
        }

        public async Task<IEnumerable<UserInfoTableModel>> GetAllList()
        {
            var query = "select user_id UserId, user_name UserName,user_pwd UserPwd,user_phone UserPhone,user_gender UserGender,user_flag UserFlag,create_time CreateTime,update_time UpdateTime from user_info";
            var list = await _dbContext.Select<UserInfoTableModel>(query);
            return list;
        }

        public UserInfoTableModel GetUserInfoById(int userId)
        {
            var query = string.Format("select user_id, user_name,user_pwd,user_phone,user_gender,user_flag,create_time,update_time where user_id=@userId");
            var user = _dbContext.SelectFirst<UserInfoTableModel>(query, new { UserId = userId });
            return user;
        }

        public async Task<IEnumerable<UserInfoTableModel>> GetUserInfoList(int pageIndex, int pageSize)
        {
            var sql = string.Format("select user_id, user_name,user_pwd,user_phone,user_gender,user_flag,create_time,update_time from user_info limit @start,@end");
            var list = await _dbContext.Select<UserInfoTableModel>(sql, new { start = (pageIndex - 1) * pageSize, end = pageSize });
            return list;
        }

        public async Task<bool> UpdateUserInfo(UserInfoTableModel userinfo)
        {
            var sql = string.Format("update user_info set user_name=@userName,user_pwd=@userPwd,user_phone=@userPhone,user_gender=@userGender,user_flag=@userFlag,create_time=@createTime,update_time=@updateTime where user_id=@userId");
            var row = await _dbContext.Update<UserInfoTableModel>(sql, userinfo);
            return row > 0 ? true : false;
        }
    }
}
