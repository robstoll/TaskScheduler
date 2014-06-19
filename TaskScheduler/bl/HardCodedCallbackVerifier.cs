using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CH.Tutteli.TaskScheduler.BL
{
    public class HardCodedCallbackVerifier : ICallbackVerifier
    {

        public bool IsSecureCallback(string callbackUrl)
        {
            return callbackUrl.StartsWith("http://localhost:4658");
        }
    }
}