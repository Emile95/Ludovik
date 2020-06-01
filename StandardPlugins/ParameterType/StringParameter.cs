using Library.Interface.ISettable;
using Library.Plugins.ParameterType;

namespace StandardPlugins
{
    public class StringParameter : ParameterType
    {
        public override ISettable.ParameterDefenition[] GetSettingDefenitions()
        {
            return null;
        }

        public sealed override string VerifyValue(string value)
        {
            return "ok";
        }
    }
}
