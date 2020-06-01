using Library.Interface.ISettable;

namespace Library.Plugins.ParameterType
{
    public abstract class ParameterType : ISettable
    {
        public abstract ISettable.ParameterDefenition[] GetSettingDefenitions();
        public abstract string VerifyValue(string value);
    }
}
