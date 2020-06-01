using Library.Plugins.BuildStep;
using Library.Plugins.Job;
using Library.Plugins.Parameter;

using System.Collections.Generic;

namespace Library
{
    public static class PluginStorage
    {
        public static List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public static List<BuildStep> BuildSteps { get; set; } = new List<BuildStep>();
        public static List<Job> Jobs { get; set; } = new List<Job>();
    }
}
