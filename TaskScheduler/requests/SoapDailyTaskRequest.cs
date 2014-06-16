using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace CH.Tutteli.TaskScheduler.Requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetDailyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetDailyTaskResponse : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public DailyTaskRequest DailyTaskRequest { get; set; }

        public GetDailyTaskResponse()
        {
            ResponseStatus = new ResponseStatus();
        }
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostDailyTask : DailyTaskRequest
    {
    }

    [DataContract]
    public class PostDailyTaskResponse : SoapTaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutDailyTask : DailyTaskRequest
    {
    }

    [DataContract]
    public class PutDailyTaskResponse : SoapTaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class DeleteDailyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class DeleteDailyTaskResponse : SoapTaskResponse
    {
    }
}