using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newegg.Cookie.CAP.Services
{
    public interface ISubscriberService
    {
        void CheckReceivedMessage(string message);

        void CheckReceivedMessage1(string message);
    }
}
