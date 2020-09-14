using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConWebAPITest2
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "hello world"; 
        }
    }
}
