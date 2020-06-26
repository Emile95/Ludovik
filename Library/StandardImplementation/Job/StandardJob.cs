using Library.Class;
using Library.Plugins.Job;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        public sealed override void LoadFromFolder(string path, string folderName)
        {
            base.LoadFromFolder(path, folderName);
        }

        public sealed override Config GetConfig()
        {
            return null;
        }

        public sealed override void SaveConfig(Config config)
        {
            
        }
    }
}
