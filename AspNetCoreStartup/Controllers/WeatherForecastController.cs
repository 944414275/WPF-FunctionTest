using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace AspNetCoreStartup.Controllers
{
    //目前传参只能是简单的参数，字符串，int等，还不能是复杂参数
    [ApiController]
    [Route("api/[controller]/[action]")] 
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        #region HttpGet
        [HttpGet("test")]
        public Student Test(Student model)
        {
            return model;
        }

        /// <summary>
        /// 没问题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetTwo")]
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
        [HttpGet("GetThree")]
        public string TestThree(int id, string name)
        {
            return "fengcaihong" + id.ToString() + name;
        }

        /// <summary>
        /// 不可以，和传参有关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("TestFour")]
        public string TestFour([FromBody] Student model)
        {
            return model.Name;
        }
        #endregion



        #region HttpPost
        /// <summary>
        /// 没问题
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("Post")]
        public void Post([FromBody] JObject student)
        {
            //return true;
        }

        [HttpPost]
        public string PostTwo([FromBody] string value)
        {
            return "fengcaihongx" + value;
        }

        /// <summary>
        /// 如果[HttpPost("PostTwo/{id}")]获取不到和入参及{id}没关系,，提示405 method not allowed
        /// 需要设置成[HttpPost("PostTwo")]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //[HttpPost]
        //public void PostThree([FromBody] string val, [FromBody] string val2)
        //{
        //    //return "fengcaihongx" + val;
        //}

        [HttpPost]
        public string PostFour(string value)
        {
            //string val = Request.Form["value"];
            return "fengcaihongx" + value;
        }
        
        [HttpPost]
        public void PostFive([FromBody] Student value)
        {
            string s = "1";
        }

        [HttpPost]
        public string PostSix([FromBody] string value)
        {
            //string strName = Convert.ToString(value.Name);
            return value;
        }
        #endregion
        
        //// POST api/values
        //[HttpPost("PostThree")]
        //public string PostFive([FromBody] JsonResult model)
        //{
        //    return null;
        //    //return model.ID.ToString()+ model.Name;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("PostFour")]
        //public void PostFour(TestParam model)
        //{

        //     string s=model.ID.ToString()+ model.Name;
        //}
         
        /// <summary>
        /// 有[FromBody]，说明参数从协议包体中获取，如果包体中没有参数，则不可以用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("PostFive")]
        public string Pos([FromBody] Student model)
        {
            return "fengcaihongx" + model.Name;
        }


        #region HttpPut
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        #endregion

        #region HttpDelete
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        #endregion
         
    }

    public class Student
    {
        public string Name { get; set; }
        public string Age { get; set; }
    }
}
