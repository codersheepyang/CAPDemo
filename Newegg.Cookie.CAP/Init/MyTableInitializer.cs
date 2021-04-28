using DotNetCore.CAP;
using DotNetCore.CAP.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newegg.Cookie.CAP.Init
{
    public class MyTableInitializer : SqlServerStorageInitializer
    {
        //重命名发送消息表与接收消息表
        public MyTableInitializer(ILogger<SqlServerStorageInitializer> logger, IOptions<SqlServerOptions> options) : base(logger, options)
        { 
            
        }
        public override string GetPublishedTableName()
        {
            return "CAPPublisedTable";
        }

        public override string GetReceivedTableName()
        {
            return "CAPReceivedTable";
        }
    }
}
