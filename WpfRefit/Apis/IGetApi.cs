using Refit;
using System.Threading.Tasks;
using WpfRefit.Models;

namespace WpfRefit.Apis
{
    /// <summary>
	/// 测试接口
	/// </summary>
	//[Headers("User-Agent: JHRS-WPF-Client")]
    public interface IGetApi
    {
        [Get("/api/Get/Get")]
        Task<string> Get(string name);

        [Get("/api/Get/GetTwo")]
        Task<string> GetTwo();
    }
}
