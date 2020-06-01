using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class BooleanParameter : Parameter
    {
        public BooleanParameter() : this("Default") {}

        public BooleanParameter(string Name, string DefaultValue="false")
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
        }

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
