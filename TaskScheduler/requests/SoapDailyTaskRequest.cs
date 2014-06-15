using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
namespace ch.tutteli.taskscheduler.requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetDailyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetDailyTaskResponse : TaskResponse
    {
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