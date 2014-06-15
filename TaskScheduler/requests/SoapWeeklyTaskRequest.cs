using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace ch.tutteli.taskscheduler.requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetWeeklyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetWeeklyTaskResponse : TaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostWeeklyTask : WeeklyTaskRequest
    {
    }

    [DataContract]
    public class PostWeeklyTaskResponse : SoapTaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutWeeklyTask : WeeklyTaskRequest
    {
    }

    [DataContract]
    public class PutWeeklyTaskResponse : SoapTaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class DeleteWeeklyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class DeleteWeeklyTaskResponse : SoapTaskResponse
    {
    }
}