using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CH.Tutteli.TaskScheduler.Common;

namespace CH.Tutteli.TaskScheduler.Requests
{
    [DataContract]
    public abstract class ATaskRequest : ITaskRequest
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime DateUpdated { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CallbackUrl { get; set; }
    }
}
