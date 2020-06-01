using Library.Interface.ISettable;

namespace Library.Plugins.Job
{
    public abstract class Job : ISettable
    {
        public abstract ISettable.ParameterDefenition[] GetSettingDefenitions();
        public Section[] Sections { get; protected set;}
    }
}
