using Library.Plugins.Job;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        public sealed override void LoadFromConfig(string path, string folderName)
        {
            base.LoadFromConfig(path, folderName);
        }
    }
}
