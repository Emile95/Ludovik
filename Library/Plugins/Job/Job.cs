using Library.Interface.ISettable;

namespace Library.Plugins.Job
{
    public abstract class Job : ISettable
    {
        public virtual Parameter.Parameter[] GetSettingDefenitions() { return new Parameter.Parameter[] { }; }
        public virtual Parameter.Parameter[] GetAdvancedSettingDefenitions() { return new Parameter.Parameter[] { }; }
        public Section[] Sections { get; protected set;}
    }
}
