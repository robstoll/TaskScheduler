using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Test.Utils;

namespace CH.Tutteli.TaskScheduler.Test
{
    abstract class ASoapIntegrationTest : AIntegrationTest
    {
        public IEnumerable<ISyncReplyClient> GetDifferentSaopClients()
        {
            return new ISyncReplyClient[]{
                new CH.Tutteli.TaskScheduler.Test.Soap11.SyncReplyClient("BasicHttpBinding_ISyncReply", BaseUrl+"/soap11"),
                new CH.Tutteli.TaskScheduler.Test.Soap12.SyncReplyClient("WSHttpBinding_ISyncReply", BaseUrl + "/soap12")
            };
        }
    }
}

namespace CH.Tutteli.TaskScheduler.Test.Soap11
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}

namespace CH.Tutteli.TaskScheduler.Test.Soap12
{
    public partial class SyncReplyClient : ISyncReplyClient
    {
    }
}