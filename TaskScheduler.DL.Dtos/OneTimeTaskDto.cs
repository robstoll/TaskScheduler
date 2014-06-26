using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CH.Tutteli.TaskScheduler.Common;
using CH.Tutteli.TaskScheduler.DL.Interfaces;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace CH.Tutteli.TaskScheduler.DL.Dtos
{
    
    public class OneTimeTaskDto : ITaskDto
    {
        #region general properties - code duplication in all request objects

        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CallbackUrl { get; set; }

        #endregion

        public DateTime Trigger { get; set; }

    }
}