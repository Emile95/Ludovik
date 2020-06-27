using Library.Plugins.Job;
using Library.Plugins.Logger;
using Library.Plugins.Node;
using Library.Plugins.ParameterDefinition;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Library
{
    public static class PluginStorage
    {
        public static Tuple<Type, List<Type>> JobTypes { get; } = new Tuple<Type, List<Type>>(typeof(Job), new List<Type>());

        public static Tuple<Type, List<Type>> ParameterDefinitionTypes { get; } = new Tuple<Type, List<Type>>(typeof(ParameterDefinition), new List<Type>());

        public static Tuple<Type, List<Type>> LoggerTypes { get; } = new Tuple<Type, List<Type>>(typeof(Logger), new List<Type>());

        public static Tuple<Type, List<Type>> NodeTypes { get; } = new Tuple<Type, List<Type>>(typeof(Node), new List<Type>());

        public static void AddJobPlugin(Type type)
        {
            JobTypes.Item2.Add(type);
        }
        public static void AddParameterDefinitionPlugin(Type type)
        {
            ParameterDefinitionTypes.Item2.Add(type);
        }

        public static void AddLoggerlugin(Type type)
        {
            LoggerTypes.Item2.Add(type);
        }

        public static void AddNodePlugin(Type type)
        {
            NodeTypes.Item2.Add(type);
        }

        public static Job CreateJob(string className)
        {
            Job job = null;
            JobTypes.Item2.ForEach(o => {
                if(o.ToString() == className)
                    job = Activator.CreateInstance(o) as Job;
            });
            return job;
        }
    }
}
