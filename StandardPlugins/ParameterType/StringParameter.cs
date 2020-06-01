using Library.Plugins.ParameterType;

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
