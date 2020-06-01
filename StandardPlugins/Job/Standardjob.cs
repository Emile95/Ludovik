using Library.Plugins.Job;
using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class Standardjob : Job
    {
        public Standardjob()
        {

        }
        public sealed override Parameter[] GetSettingDefenitions() 
        {
            return new Parameter[] {
                new StringParameter("Name"),
                new StringParameter("Description")
            }; 
        }
    }
}
