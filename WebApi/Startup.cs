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
using Library.StandardImplementation.BoolParameterDefinition;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using Library.StandardImplementation.JobBuildLogger;
using Library.StandardImplementation.LabelParameterDefinition;
using Library.StandardImplementation.NodePropertyDefinition;
using Library.StandardImplementation.ParameterizedRunPropertyDefinition;
using Library.StandardImplementation.StandardJob;
using Library.StandardImplementation.StandardLogger;
using Library.StandardImplementation.StringParameterDefinition;
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

        static IEnumerable<T> CreatePlugins<T>(Assembly assembly) where T : class
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    T result = Activator.CreateInstance(type) as T;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements {typeof(T).ToString()} in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
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
            services.AddControllers();

            services.AddSingleton<ThreadApplication>();
            services.AddSingleton<NodeApplication>();

            services.AddScoped<IJobApplication, JobApplication>();

            PluginStorage.AddPlugin<Job>(typeof(StandardJob));

            PluginStorage.AddPlugin<ParameterDefinition>(typeof(StringParameterDefinition));
            PluginStorage.AddPlugin<ParameterDefinition>(typeof(BoolParameterDefinition));
            PluginStorage.AddPlugin<ParameterDefinition>(typeof(LabelParameterDefinition));

            PluginStorage.AddPlugin<PropertyDefinition>(typeof(DescriptionPropertyDefinition));
            PluginStorage.AddPlugin<PropertyDefinition>(typeof(ParameterizedRunPropertyDefinition));
            PluginStorage.AddPlugin<PropertyDefinition>(typeof(NodePropertyDefinition));
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
