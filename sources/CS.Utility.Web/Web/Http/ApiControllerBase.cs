using System.Web.Http;
using CS.Logging;

namespace CS.Web.Http
{
    public class ApiControllerBase : ApiController
    {
        protected readonly ILog Log;

        //protected readonly ILog AppLog = LogManager.GetLogger();

        protected ApiControllerBase()
        {
            Log = LogManager.GetLogger(GetType());
        }




    }
}