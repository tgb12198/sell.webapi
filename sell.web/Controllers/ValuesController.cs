using sell.web.Common.Comm;
using sell.web.IService;
using sell.web.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sell.web.Controllers
{
    public class ValuesController : ApiController
    {
        //public IUserInfoSercice _userInfoService { get; set; }

        public IUserInfoSercice _userInfoService { get; set; }

        public ValuesController(IUserInfoSercice userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        public BaseResponse<IEnumerable<UserInfo>> GetAllList()
        {
            var list = _userInfoService.GetAllList();
            return new BaseResponse<IEnumerable<UserInfo>> { Code = 0, Message = "success", Data = list };
        }
    }
}
