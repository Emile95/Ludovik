using Library.Plugins;

namespace StandardPlugins
{
    public class BooleanParameter : ParameterType
    {
        public sealed override string VerifyValue(string value)
        {
            switch(value)
            {
                case "true":
                case "false":
                case "1":
                case "0":
                    break;
                default:
                    return "BadValue";
            }
            return "ok";
        }
    }
}
