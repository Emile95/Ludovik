using Library;
using Library.Plugins.ParameterType;
using System.Linq;

namespace StandardPlugins
{
    public class BuildStepParameter : ParameterType
    {
        public sealed override string VerifyValue(string value)
        {
            if (PluginStorage.BuildSteps.Where(o => o.Name == value).Count() == 0)
                return "NoExistence";
            return "ok";
        }
    }
}
