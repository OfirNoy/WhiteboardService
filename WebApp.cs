using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using WhiteboardService.Logic;

namespace WhiteboardService
{
    public class WebApp
    {
        internal static State State { get; private set; }

        public WebApp(IConfiguration configuration)
        {            
            Configuration = new WBConfig(configuration);
            if(!Directory.Exists(Configuration.RepoFolder))
            {
                Directory.CreateDirectory(Configuration.RepoFolder);
            }
            State = new State(Configuration.RepoFolder,
                                Configuration.GitUserName,
                                Configuration.GitUserEmail);
        }

        public static WBConfig Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorsMiddleware();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<WhiteboardHub>("/board");
            });
        }
    }
}
