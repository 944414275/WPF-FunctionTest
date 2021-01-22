using Refit;
using System.Threading.Tasks;
using WpfRefit.Models;

namespace WpfRefit.Apis
{
    /// <summary>
	/// 测试接口
	/// </summary>
	[Headers("User-Agent: JHRS-WPF-Client")]
    public interface ITestApi
    {
        [Get("api/WeatherForecast/PostFour")]
        Task<TestModel> Test();
         
    }
}
