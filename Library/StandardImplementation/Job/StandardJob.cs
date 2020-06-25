using Library.Plugins.Job;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        public sealed override void LoadConfig(string path, string folderName)
        {
            base.LoadConfig(path, folderName);
        }
    }
}
