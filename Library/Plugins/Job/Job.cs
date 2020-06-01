using Library.Interface.ISettable;

namespace Library.Plugins.Job
{
    public abstract class Job : ISettable
    {
        public virtual ParameterType.ParameterType[] GetSettingDefenitions() { return new ParameterType.ParameterType[] { }; }
        public virtual ParameterType.ParameterType[] GetAdvancedSettingDefenitions() { return new ParameterType.ParameterType[] { }; }
        public Section[] Sections { get; protected set;}
    }
}
