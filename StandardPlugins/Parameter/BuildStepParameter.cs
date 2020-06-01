using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class BuildStepParameter : Parameter
    {
        public BuildStepParameter() : this("Default") { }
        public BuildStepParameter(string Name, string DefaultValue = "")
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
