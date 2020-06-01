using Library.Interface;

namespace Library.Plugins.Job
{
    public abstract class Job : ISettable
    {
        public Section[] Sections { get; protected set;}
    }
}
