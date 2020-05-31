using Library.Plugins;

namespace Library
{
    public class ParameterSetting
    {
        public string Name { get; }
        public string DefaultValue { get; }
        public ParameterType ParameterType { get; }

        public ParameterSetting(string Name, ParameterType ParameterType, string DefaultValue=null)
        {
            this.Name = Name;
            this.DefaultValue = DefaultValue;
            this.ParameterType = ParameterType;
        }
    }
}
