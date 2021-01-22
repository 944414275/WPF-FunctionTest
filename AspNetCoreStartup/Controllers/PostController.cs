using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspNetCoreStartup.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PostController : ControllerBase
    {
        /// <summary>
        /// 这是get和post的区别，get默认是通过url后面的参数获取到参数的，
        /// 而post默认是通过http方法体中获取参数的，所以这个方法会返回404
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public string Post(string id)
        {
            return "fengcaihongx" + id;
        }

        /// <summary>
        /// 这个和上面的区别是在属性标签上面没有{id},这样就不会让调用方认为是从url中获取
        /// 参数，所以id是null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostTwo(string id)
        {
            return "fengcaihongx" + id;
        }

        /// <summary>
        /// 可以正常调用，但是不是正常的json格式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostThree([FromBody] string value)
        {
            return "fengcaihongx" + value;
        }

        /// <summary>
        ///  实体类 class 需要加[FromBody]标签，前端数据字段可以不区分大小写一样，但是字段要一样
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostFour([FromBody] Student model)
        {
            return "fengcaihongx" + model.Name;
        }

        /// <summary>
        /// 实体类 class 可以不需要[FromBody]标签，前端数据字段可以不区分大小写一样，但是字段要一样
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostFive(Student model)
        {
            return "fengcaihongx" + model.Name;
        }

        /// <summary>
        /// dynamic动态类型 class,因为是dynamic，不是指定的strudent类型，所以前端字段就必须和后台的一样
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostSix([FromBody] dynamic model)
        {
            return "fengcaihongx" + model.Name;
        }

        /// <summary>
        /// dynamic动态类型 class,因为是dynamic，不是指定的strudent类型，所以前端字段就必须和后台的一样
        /// 可以不需要[FromBody]标签
        /// 使用实体作为参数的时候，前端直接传递普通json，后台直接使用对应的类型去接收即可，不用FromBody。但是这里需要注意的一点就是，这里不能指定contentType为appplication/json，
        /// 否则，参数无法传递到后台。我们来看看它默认的contentType是什么：
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostSeven(dynamic model)
        {
            return "fengcaihongx" + model.name;
        }

        /// <summary>
        /// JObject参数 string
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public void PostEight(string company,string devNum,JObject jsonObject)
        {
            //把jsonObject反序列化成dynamic
            string jsonStr = JsonConvert.SerializeObject(jsonObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            //获取dynamic里边的数据
            string destId = jsonParams.destId;
            string token = jsonParams.token;
            //return "fengcaihongx" + model["Name"];
        }

        [HttpPost]
        public string PostNine([FromBody] JObject jsonObject)
        {
            //把jsonObject反序列化成dynamic
            string jsonStr = JsonConvert.SerializeObject(jsonObject);
            var jsonParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);
            //获取dynamic里边的数据
            string destId = jsonParams.destId;
            string token = jsonParams.token;
            return "fengcaihongx";
        }
    }
    /*webapi接收jobject对象，json在api中的使用非常常见，但是core在api的请求中是不支持
     弱类型对象的，可以确定的是支持自定义类型和基础数据类型。通常使用post发送一个json，
    如果json是api已经定义好的强类型，那么core可以将json直接反序列化成自定义类型。但如果
    json的内容不固定，或不便于定义强类型，我们通常选择是弱类型jobject，但是很遗憾得事3.0之前是
    不支持json反序列化jobject的。（3.0之前的core通常自定义一个模型绑定构建器和模型绑定方法，参考资料3）
    3.0之后添加了对jobject的优化支持，引用包Microsoft.AspNetCore.Mvc.Newtonsoft，并在配置服务的时候对
    controller添加json的支持即可，如下：
    　services.AddControllers().AddNewtonsoftJson();
    然后便可以在controll中添加post的api
     */
}
