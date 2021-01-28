using Refit;
using System.Threading.Tasks;
using WpfRefit.Models;

namespace WpfRefit.Apis
{
    /// <summary>
	/// 测试接口
	/// </summary>
	//[Headers("User-Agent: JHRS-WPF-Client")]
    public interface IPostApi
    {
        [Get("api/Post/Post")]
        Task<string> Post(string id);

        [Get("api/Post/PostTwo")]
        Task<string> PostTwo();
    }
}
