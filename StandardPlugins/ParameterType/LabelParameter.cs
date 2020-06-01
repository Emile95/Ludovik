using Library.Interface.ISettable;
using Library.Plugins.ParameterType;
using System.IO;

namespace StandardPlugins
{
    public class LabelParameter : ParameterType
    {
        public override ISettable.ParameterDefenition[] GetSettingDefenitions()
        {
            return null;
        }

        public sealed override string VerifyValue(string value)
        {
            if (!File.Exists("nodes\\" + value + ".json"))
                return "NoExistence";
            return "ok";
        }
    }
}
