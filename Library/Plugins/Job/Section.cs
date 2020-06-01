using Library.Interface;

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
