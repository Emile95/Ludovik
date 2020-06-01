using Library;
using Library.Plugins.Parameter;
using System.Linq;

namespace StandardPlugins
{
    public class BuildStepParameter : ParameterType
    {
        public BuildStepParameter() : this("Default") { }
        public BuildStepParameter(string Name, string DefaultValue = "")
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
        }
        public sealed override string VerifyValue(string value)
        {
            if (PluginStorage.BuildSteps.Where(o => o.Name == value).Count() == 0)
                return "NoExistence";
            return "ok";
        }
    }
}
