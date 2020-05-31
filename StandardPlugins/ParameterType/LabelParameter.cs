using Library.Plugins;
using System.IO;

namespace StandardPlugins
{
    public class LabelParameter : ParameterType
    {
        public sealed override string VerifyValue(string value)
        {
            if (!File.Exists("nodes\\" + value + ".json"))
                return "NoExistence";
            return "ok";
        }
    }
}
