using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace CH.Tutteli.TaskScheduler.Requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetOneTimeTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetOneTimeTaskResponse : IHasResponseStatus
    {
        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
        [DataMember]
        public OneTimeTaskRequest OneTimeTaskRequest { get; set; }

        public GetOneTimeTaskResponse()
        {
            ResponseStatus = new ResponseStatus();
        }
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostOneTimeTask
    {
        [DataMember]
        public OneTimeTaskRequest OneTimeTaskRequest { get; set; }
    }

    [DataContract]
    public class PostOneTimeTaskResponse : SoapTaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutOneTimeTask
    {
        [DataMember]
        public OneTimeTaskRequest OneTimeTaskRequest { get; set; }
    }

    [DataContract]
    public class PutOneTimeTaskResponse : SoapTaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class DeleteOneTimeTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class DeleteOneTimeTaskResponse : SoapTaskResponse
    {
    }
}