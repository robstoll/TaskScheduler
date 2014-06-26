using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.Requests;
using CH.Tutteli.TaskScheduler.Test.Utils;
using ServiceStack.ServiceClient.Web;

namespace CH.Tutteli.TaskScheduler.Test
{
    public abstract class ARestIntegrationTest : AIntegrationTest
    {
        public static TResponse SendRequest<TResponse>(ITaskRequest request, ServiceClientBase client, string httpMethod)
        {
            return SendRequest<TResponse>(request, client, httpMethod, null);
        }

        public static TResponse SendRequest<TResponse>(ITaskRequest request, ServiceClientBase client, string httpMethod, Action<HttpWebResponse> responseFilter)
        {
            using (client)
            {
                client.HttpMethod = httpMethod;
                client.LocalHttpWebResponseFilter = responseFilter;
                return client.Send<TResponse>(request);
            }
        }

        public IEnumerable<ServiceClientBase> GetDifferentRestClients()
        {
            return new ServiceClientBase[]{
            new XmlServiceClient(BaseUrl),
            new JsonServiceClient(BaseUrl),
            new JsvServiceClient(BaseUrl),
            };
        }

        public IEnumerable<ITaskRequest> GetDifferentTaskRequests()
        {
            return new ITaskRequest[]{
                TaskHelper.CreateOneTimeTaskRequest(),
                TaskHelper.CreateDailyTaskRequest(),
                TaskHelper.CreateWeaklyTaskRequest(),
                TaskHelper.CreateMonthlyTaskRequest()
            };
        }
    }
}
