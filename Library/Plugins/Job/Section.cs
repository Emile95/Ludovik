using Library.Interface.ISettable;

namespace Library.Plugins.Job
{
    public class Section 
    {
        public ISettable[] Settings { get; }
        public Section()
        {
            Settings = new ISettable[] { }; 
        }
    }
}
