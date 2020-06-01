using Library.Interface.ISettable;
using Library.Plugins.ParameterType;

namespace StandardPlugins
{
    public class BooleanParameter : ParameterType
    {
        public override ISettable.ParameterDefenition[] GetSettingDefenitions()
        {
            return null;
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
