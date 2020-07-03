using Library.Plugins.BuildStepDefinition;
using Library.Plugins.Job;
using Library.Plugins.Logger;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using System;
using System.Collections.Generic;

namespace Library
{
    public static class PluginStorage
    {
        public static Dictionary<Type, List<Type>> Plugins = new Dictionary<Type, List<Type>>();

        static PluginStorage()
        {
            Plugins.Add(typeof(Job), new List<Type>());
            Plugins.Add(typeof(ParameterDefinition), new List<Type>());
            Plugins.Add(typeof(PropertyDefinition), new List<Type>());
            Plugins.Add(typeof(BuildStepDefinition), new List<Type>());
            Plugins.Add(typeof(Logger), new List<Type>());

        }

        public static void AddPlugin<T>(Type type)
        {
            Plugins[typeof(T)].Add(type);
        }

        public static T CreateObject<T>(string className) where T : class
        {
            T obj = null;
            Plugins[typeof(T)].ForEach(o => {
                
                string[] str = o.ToString().Split(".");
                if (str[str.Length - 1] == className)
                    obj = Activator.CreateInstance(o) as T;  
            });
            return obj;
        }
    }
}
