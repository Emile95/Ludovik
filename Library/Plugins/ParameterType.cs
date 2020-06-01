using Library.Interface.ISettable;

namespace Library.Plugins.ParameterType
{
    public abstract class ParameterType : ISettable
    {
        public ParameterType[] GetAdvancedSettingDefenitions()
        {
            throw new System.NotImplementedException();
        }

        public ParameterType[] GetSettingDefenitions()
        {
            throw new System.NotImplementedException();
        }

        public abstract string VerifyValue(string value);
    }
}
