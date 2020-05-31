using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Plugins;

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

        static IEnumerable<IParameterTypeDefenition> CreateParameterTypeDefenitions(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IParameterTypeDefenition).IsAssignableFrom(type))
                {
                    IParameterTypeDefenition result = Activator.CreateInstance(type) as IParameterTypeDefenition;
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
                    $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                string[] pluginPaths = new string[]
                {
                    "F:\\Prog\\Ludovik\\App\\App\\bin\\Debug\\netcoreapp3.1\\plugins\\StandardPlugins\\StandardPlugin.dll"
                };

                List<IParameterTypeDefenition> parameterTypeDefenitions = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateParameterTypeDefenitions(pluginAssembly);
                }).ToList();

                parameterTypeDefenitions.ForEach(item => {
                    Console.WriteLine("Parameter Type Definition -- Name : " + item.Name + ", Description : " + item.Description);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
