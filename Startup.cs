using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models;
using Akka.Actor;
using MongoDB.Bson.Serialization;
using BlogApp.Models.MongoDB;
using BlogApp.Actors;

namespace BlogApp
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
            var actorSystem = ActorSystem.Create("CQRS");
    
            services.AddMvc();

            services.AddSession();
            services.AddSingleton<IConfiguration>(Configuration);  
            services.AddSingleton<IActorRefFactory>(actorSystem);

            var eventRootActor = actorSystem.ActorOf<EventRootActor>("EventRootActor"); 
            services.AddSingleton<IActorRef>(eventRootActor);

            BsonClassMap.RegisterClassMap<PostDetails>();
            BsonClassMap.RegisterClassMap<PostList>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Posts/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Posts}/{action=Index}/{id?}");
            });
        }
    }
}
