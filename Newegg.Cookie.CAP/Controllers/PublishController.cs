using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Newegg.Cookie.CAP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly ICapPublisher _capBus;

        public PublishController(ICapPublisher capPublisher)
        {
            _capBus = capPublisher;
        }


        //获取到CAP Header消息
        [CapSubscribe("test")]
        public void CheckReceivedMessage1(string message,[FromCap]CapHeader header)
        {
            Console.WriteLine(header["my.kafka.offset"]);
            Console.WriteLine(header["my.kafka.partition"]);
        }

        [HttpGet]
        [Route("adonet/transaction")]
        public IActionResult AdonetWithTransaction()
        {

            _capBus.Publish("test", "1");
            return Ok();
        }
    }
}
