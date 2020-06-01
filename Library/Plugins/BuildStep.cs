using Library.Interface.ISettable;

namespace Library.Plugins.BuildStep
{
    public abstract class BuildStep : ISettable
    {
        public string Name { get; protected set; }
        public virtual ParameterType.ParameterType[] GetSettingDefenitions() { return new ParameterType.ParameterType[] { }; }
        public virtual ParameterType.ParameterType[] GetAdvancedSettingDefenitions() { return new ParameterType.ParameterType[] { }; }
        public abstract void Action(string[] paramValues);
    }
}
