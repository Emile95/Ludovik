using Library.Plugins.Job;
using Library.Plugins.Parameter;

namespace StandardPlugins
{
    public class Standardjob : Job
    {
        public Standardjob()
        {

        }
        public sealed override ParameterType[] GetSettingDefenitions() 
        {
            return new ParameterType[] {
            }; 
        }
    }
}
