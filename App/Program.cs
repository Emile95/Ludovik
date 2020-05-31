using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Library.Plugins;

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

        static IEnumerable<ParameterType> CreateParameterTypeDefenitions(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ParameterType).IsAssignableFrom(type))
                {
                    ParameterType result = Activator.CreateInstance(type) as ParameterType;
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

        static IEnumerable<BuildStep> CreateBuildSteps(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(BuildStep).IsAssignableFrom(type))
                {
                    BuildStep result = Activator.CreateInstance(type) as BuildStep;
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

                List<ParameterType> parameterTypeDefenitions = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateParameterTypeDefenitions(pluginAssembly);
                }).ToList();

                List<BuildStep> buildSteps = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateBuildSteps(pluginAssembly);
                }).ToList();

                Console.WriteLine("--ParameterType--");
                parameterTypeDefenitions.ForEach(o =>
                {
                    Console.WriteLine(o.ToString());
                });
                Console.WriteLine("--BuildStep--");
                buildSteps.ForEach(o =>
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
