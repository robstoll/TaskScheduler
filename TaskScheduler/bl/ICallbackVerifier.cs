using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Tutteli.TaskScheduler.BL
{
    public  interface ICallbackVerifier
    {
        bool IsSecureCallback(string callbackUrl);
    }
}
