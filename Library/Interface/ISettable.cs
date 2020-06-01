using Library.Plugins.ParameterType;

namespace Library.Interface.ISettable
{
    public interface ISettable
    {
        ParameterType[] GetSettingDefenitions();
        ParameterType[] GetAdvancedSettingDefenitions();
    }
}
