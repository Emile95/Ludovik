using Library.Class;
using Library.Plugins.Job;
using System.Threading;

namespace Library.StandardImplementation.StandardJob
{
    public class StandardJob : Job
    {
        #region Properties and Constructor

        public StandardJob()
        {
            ClassName = "StandardJob";
        }

        #endregion

        #region Job implementation

        public sealed override void PreBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {

        }

        public sealed override void Build(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {

        }

        public sealed override void AfterBuild(Build build, CancellationToken taskCancelToken, LoggerList loggers)
        {

        }

        #endregion

        #region IConvertable Implementation

        public sealed override string ToJson(bool beautify, int nbTab = 0)
        {
            string depthTab = "";
            for (int i = 0; i < nbTab; i++)
            {
                depthTab += "\t";
            }

            string jsonStr = depthTab + "{\n";
            jsonStr += depthTab + "\t" + "\"_class\":" + "\"" + ClassName + "\"," + "\n";
            jsonStr += depthTab + "\t" + "\"description\":" + "\"" + Description + "\"," + "\n";

            jsonStr += depthTab + "\t" + "\"properties\":" + "[\n";

            for (int i = 0; i < Properties.Count; i++)
            {
                
                jsonStr += Properties[i].ToJson(true, nbTab + 2);
                jsonStr += (i < Properties.Count - 1 ? "," : "") + "\n";
            }

            jsonStr += depthTab + "\t]" + "\n";

            jsonStr += "}";
            return jsonStr;
        }

        #endregion
    }
}
