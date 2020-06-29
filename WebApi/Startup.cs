using Application.JobApplication;
using Application.ThreadApplication;
using Library;
using Library.StandardImplementation.BoolParameterDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.LabelParameterDefinition;
using Library.StandardImplementation.StandardJob;
using Library.StandardImplementation.StandardLogger;
using Library.StandardImplementation.StandardNode;
using Library.StandardImplementation.StringParameterDefinition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
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

            services.AddSingleton<ThreadApplication>();

            services.AddScoped<IJobApplication, JobApplication>();

            PluginStorage.AddJobPlugin(typeof(StandardJob));

            PluginStorage.AddParameterDefinitionPlugin(typeof(StringParameterDefinition));
            PluginStorage.AddParameterDefinitionPlugin(typeof(BoolParameterDefinition));
            PluginStorage.AddParameterDefinitionPlugin(typeof(LabelParameterDefinition));

            PluginStorage.AddLoggerlugin(typeof(StandardLogger));
            PluginStorage.AddLoggerlugin(typeof(JobBuildLogger));

            PluginStorage.AddNodePlugin(typeof(StandardNode));
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
