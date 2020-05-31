using Library.Plugins;

namespace StandardPlugins
{
    public class StringParameter : ParameterType
    {
        public sealed override string VerifyValue(string value)
        {
            return "ok";
        }
    }
}
