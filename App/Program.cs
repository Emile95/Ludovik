using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace App
{
    class Program
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

        static void Main(string[] args)
        {
            try
            {
                //Path of implementention of Plugins
                string[] pluginPaths = Directory.GetFiles("F:\\Prog\\Ludovik\\App\\App\\bin\\Debug\\netcoreapp3.1\\plugins", "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".dll")).ToArray();

                /*
                //Load Implmentation of ParameterType
                List<ParameterType> parameterTypes = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreatePlugins<ParameterType>(pluginAssembly);
                }).ToList();

                //Load Implmentation of BuildStep
                List<BuildStep> buildSteps = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreatePlugins<BuildStep>(pluginAssembly);
                }).ToList();

                PluginStorage.ParameterTypes.AddRange(parameterTypes);
                PluginStorage.BuildSteps.AddRange(buildSteps);*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
