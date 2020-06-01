using Library.Plugins.ParameterType;

namespace Library.Interface.ISettable
{
    public interface ISettable
    {
        ParameterDefenition[] GetSettingDefenitions();

        public class ParameterDefenition
        {
            public ParameterType ParamType { get; set; }
            public string DefaultValue { get; }
        }
    }
}
