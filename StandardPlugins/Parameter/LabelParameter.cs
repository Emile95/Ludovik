using Library.Plugins.Parameter;
using System.IO;

namespace StandardPlugins
{
    public class LabelParameter : Parameter
    {
        public LabelParameter() : this("Default") { }
        public LabelParameter(string Name, string DefaultValue = "master")
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
        }
        public sealed override string VerifyValue(string value)
        {
            if (!File.Exists("nodes\\" + value + ".json"))
                return "NoExistence";
            return "ok";
        }
    }
}
