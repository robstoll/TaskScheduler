using System.Runtime.Serialization;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
namespace ch.tutteli.taskscheduler.requests
{

    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class GetOneTimeTask
    {
        [DataMember]
        public long Id { get; set; }
    }

    [DataContract]
    public class GetOneTimeTaskResponse : OneTimeTaskRequest
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PostOneTimeTask : OneTimeTaskRequest
    {
    }

    [DataContract]
    public class PostOneTimeTaskResponse : SoapTaskResponse
    {
    }


    [Restrict(EndpointAttributes.Soap12 | EndpointAttributes.Soap11)]
    [DataContract]
    public class PutOneTimeTask : OneTimeTaskRequest
    {
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