using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FileUpLoad.Controllers
{
    /// <summary>
    /// Test
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Get1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string Get1()
        {
            return "fengcaihong";
        }

        /// <summary>
        /// 没问题
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="name">用户名字</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<string> Get(int id, string name)
        {
            return "fengcaihong" + id.ToString() + name;
        }

        /// <summary>
        /// 没问题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetTwo(int id, string name)
        {
            return "fengcaihong" + id.ToString() + name;
        }

        /// <summary>
        /// 不可以，和传参有关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetThree([FromBody] Student model)
        {
            return model.Name;
        }

        /// <summary>
        /// 没问题
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public void Post([FromBody] JObject student)
        {
            //return true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
    }
}
