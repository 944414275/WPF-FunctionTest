using System; 

namespace AspNetCoreJWTTest2.Model
{
    public class Token
    {
        public string TokenContent { get; set; }

        public DateTime Expires { get; set; }
    }
}
