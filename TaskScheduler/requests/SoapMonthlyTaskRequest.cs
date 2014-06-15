using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
namespace ch.tutteli.taskscheduler.requests
{
    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetMonthlyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetMonthlyTaskResponse : TaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostMonthlyTask : MonthlyTaskRequest
    {
    }

    [DataContract]
    public class PostMonthlyTaskResponse : SoapTaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutMonthlyTask : MonthlyTaskRequest
    {
    }

    [DataContract]
    public class PutMonthlyTaskResponse : SoapTaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class DeleteMonthlyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class DeleteMonthlyTaskResponse: SoapTaskResponse
    {
    }
}