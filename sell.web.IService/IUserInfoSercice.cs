using sell.web.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sell.web.IService
{
    public interface IUserInfoSercice : IDependency
    {
        IEnumerable<UserInfo> GetAllList();
    }
}
