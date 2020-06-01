using Library.Interface;

namespace Library.Plugins.ParameterType
{
    public abstract class ParameterType : ISettable
    {
        public abstract string VerifyValue(string value);
    }
}
