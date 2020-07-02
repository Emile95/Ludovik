using Application.JobApplication;
using Application.ThreadApplication;
using Library;
using Library.Plugins.Job;
using Library.Plugins.Logger;
using Library.Plugins.Node;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using Library.StandardImplementation.BoolParameterDefinition;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.LabelParameterDefinition;
using Library.StandardImplementation.NodePropertyDefinition;
using Library.StandardImplementation.ParameterizedRunPropertyDefinition;
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

            PluginStorage.AddPlugin<Job>(typeof(StandardJob));

            PluginStorage.AddPlugin<ParameterDefinition>(typeof(StringParameterDefinition));
            PluginStorage.AddPlugin<ParameterDefinition>(typeof(BoolParameterDefinition));
            PluginStorage.AddPlugin<ParameterDefinition>(typeof(LabelParameterDefinition));

            PluginStorage.AddPlugin<PropertyDefinition>(typeof(DescriptionPropertyDefinition));
            PluginStorage.AddPlugin<PropertyDefinition>(typeof(ParameterizedRunPropertyDefinition));
            PluginStorage.AddPlugin<PropertyDefinition>(typeof(NodePropertyDefinition));

            PluginStorage.AddPlugin<Logger>(typeof(StandardLogger));
            PluginStorage.AddPlugin<Logger>(typeof(JobBuildLogger));

            PluginStorage.AddPlugin<Node>(typeof(StandardNode));
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
