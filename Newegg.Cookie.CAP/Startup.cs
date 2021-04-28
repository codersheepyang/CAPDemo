using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP.Dashboard.NodeDiscovery;
using DotNetCore.CAP.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newegg.Cookie.CAP.Init;
using Newegg.Cookie.CAP.Services;

namespace Newegg.Cookie.CAP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton<IStorageInitializer, MyTableInitializer>();
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(x =>
            {
                x.UseSqlServer("data source=172.16.170.103,4433;database=Warehouse;uid=whdbo;Password=2Dev4WH;Connection Timeout=300;Connection Lifetime=300;min pool size=0; max pool size=50;MultipleActiveResultSets=True;App=DotnetCoreTemplate");
                x.UseKafka(k => 
                {
                    k.Servers = "localhost:9092";
                    //创建CAP Header消息
                    k.CustomHeaders = kafka => new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("my.kafka.offset",kafka.Offset.ToString()),
                        new KeyValuePair<string, string>("my.kafka.partition",kafka.Partition.ToString())
                    };
                    
                });

                x.UseDashboard();
                x.UseDiscovery(d =>
                {
                    //API地址
                    d.DiscoveryServerHostName = "localhost";
                    d.DiscoveryServerPort = 5001;
                    //Kafka地址
                    d.CurrentNodeHostName = "localhost";
                    d.CurrentNodePort = 9092;
                    d.NodeId = "1";
                    d.NodeName = "CAP No.1 Node";
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
