using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinPlanWebAPi.Services
{
    public class SecurityService : ISecurityService
    {
        public void HideErrorTime()
        {
            Random RNG = new Random(DateTimeOffset.Now.Millisecond*5/3);
            Thread.Sleep(RNG.Next(477, 1500));
        }

    }
}