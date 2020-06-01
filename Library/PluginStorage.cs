using Library.Plugins.BuildStep;
using Library.Plugins.Job;
using Library.Plugins.ParameterType;

using System.Collections.Generic;

namespace Library
{
    public static class PluginStorage
    {
        public static List<ParameterType> ParameterTypes { get; set; } = new List<ParameterType>();
        public static List<BuildStep> BuildSteps { get; set; } = new List<BuildStep>();
        public static List<Job> Jobs { get; set; } = new List<Job>();
    }
}
