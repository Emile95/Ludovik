using Library.Plugins.Job;
using System.Collections.Generic;

namespace Library
{
    public static class PluginStorage
    {
        public static List<Job> Jobs { get; } = new List<Job>();
    }
}
