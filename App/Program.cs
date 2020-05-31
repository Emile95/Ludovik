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

        static IEnumerable<ParameterTypeDefenition> CreateParameterTypeDefenitions(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ParameterTypeDefenition).IsAssignableFrom(type))
                {
                    ParameterTypeDefenition result = Activator.CreateInstance(type) as ParameterTypeDefenition;
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
                string[] pluginPaths = Directory.GetFiles("F:\\Prog\\Ludovik\\App\\App\\bin\\Debug\\netcoreapp3.1\\plugins", "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".dll")).ToArray();

                List<ParameterTypeDefenition> parameterTypeDefenitions = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateParameterTypeDefenitions(pluginAssembly);
                }).ToList();

                Console.WriteLine("--ParameterTypeDefenition--");
                parameterTypeDefenitions.ForEach(o =>
                {
                    Console.WriteLine(o.ToString());
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
