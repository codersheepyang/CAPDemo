using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newegg.Cookie.CAP.Services
{
    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("cookieStudy", Group = "group1")]
        public void CheckReceivedMessage(string message)
        {
            Console.WriteLine("group1: " + message);
        }

        [CapSubscribe("cookieStudy", Group = "group1")]
        public void CheckReceivedMessage1(string message)
        {
            Console.WriteLine("group2: " + message);
        }

        
    }
}
