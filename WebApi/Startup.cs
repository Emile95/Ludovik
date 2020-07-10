using Application.JobApplication;
using Application.NodeApplication;
using Application.ThreadApplication;
using Library;
using Library.Class;
using Library.Plugins.BuildStepDefinition;
using Library.Plugins.Job;
using Library.Plugins.Logger;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.StandardLogger;
using Library.StandardImplementation.WindowsBatchBuildStepDefinition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebApi
{
    public class Startup
    {
        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        static void CreatePlugins<T>(Assembly assembly) where T : class
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    count++;
                    if (!PluginStorage.IsExistingImplementation<T>(type))
                        PluginStorage.AddPlugin<T>(type);
                }
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\" + "plugins"))
            {
                string[] pluginPaths = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + "plugins", "*.dll", SearchOption.AllDirectories);

                foreach (string path in pluginPaths)
                {
                    CreatePlugins<ParameterDefinition>(LoadPlugin(path));
                    CreatePlugins<PropertyDefinition>(LoadPlugin(path));
                    CreatePlugins<Job>(LoadPlugin(path));
                }
            }
            
            services.AddControllers();

            services.AddSingleton<ThreadApplication>();
            services.AddSingleton<NodeApplication>();

            services.AddScoped<IJobApplication, JobApplication>();

            PluginStorage.AddPlugin<PropertyDefinition>(typeof(WindowsBatchBuildStepDefinition));

            PluginStorage.AddPlugin<BuildStepDefinition>(typeof(WindowsBatchBuildStepDefinition));

            PluginStorage.AddPlugin<Logger>(typeof(StandardLogger));
            PluginStorage.AddPlugin<Logger>(typeof(JobBuildLogger));
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
