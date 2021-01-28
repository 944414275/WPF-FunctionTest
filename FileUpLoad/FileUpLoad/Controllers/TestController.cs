using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FileUpLoad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
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
        public void Post([FromBody] JObject student)
        {
            //return true;
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
