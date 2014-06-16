using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace CH.Tutteli.TaskScheduler.Requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetWeeklyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetWeeklyTaskResponse : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public WeeklyTaskRequest WeeklyTaskRequest { get; set; }

        public GetWeeklyTaskResponse()
        {
            ResponseStatus = new ResponseStatus();
        }
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