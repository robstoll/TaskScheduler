using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace CH.Tutteli.TaskScheduler.Requests
{
    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetMonthlyTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetMonthlyTaskResponse : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public MonthlyTaskRequest MonthlyTaskRequest { get; set; }

        public GetMonthlyTaskResponse()
        {
            ResponseStatus = new ResponseStatus();
        }

    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostMonthlyTask
    {
        [DataMember]
        public MonthlyTaskRequest MonthlyTaskRequest { get; set; }
    }

    [DataContract]
    public class PostMonthlyTaskResponse : SoapTaskResponse
    {
    }

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutMonthlyTask
    {
        [DataMember]
        public MonthlyTaskRequest MonthlyTaskRequest { get; set; }
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
    public class DeleteMonthlyTaskResponse : SoapTaskResponse
    {
    }
}