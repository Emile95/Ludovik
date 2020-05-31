using Plugins;

namespace StandardPlugins
{
    public class StringParameterDefenition : ParameterTypeDefenition
    {
        public sealed override string VerifyValue(string value)
        {
            return "ok";
        }
    }
}
