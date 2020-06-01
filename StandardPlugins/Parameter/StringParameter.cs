using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class StringParameter : Parameter
    {
        public StringParameter() : this("Default") { }
        public StringParameter(string Name, string DefaultValue = "")
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
        }
        public sealed override string VerifyValue(string value)
        {
            return "ok";
        }
    }
}
