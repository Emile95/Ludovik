using Library.Plugins.Parameter;

namespace Library.Interface.ISettable
{
    public interface ISettable
    {
        Parameter[] GetSettingDefenitions();
        Parameter[] GetAdvancedSettingDefenitions();

        public class ParameterDefenition
        {
            public Parameter ParamType { get; set; }
            public string DefaultValue { get; }
        }
    }
}
