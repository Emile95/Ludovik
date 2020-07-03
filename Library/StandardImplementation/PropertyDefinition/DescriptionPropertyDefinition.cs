using Library.Class;
using Library.Plugins.PropertyDefinition;
using System.Linq;

namespace Library.StandardImplementation.DescriptionPropertyDefinition
{
    public class DescriptionPropertyDefinition : PropertyDefinition
    {
        public DescriptionPropertyDefinition()
        {
            ClassName = "DescriptionPropertyDefinition";
        }

        #region PropertyDefinition Implementation

        public sealed override void Apply(Environment env, Parameter[] parameters, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            env.Properties.Add("jobName", parameters.Single(o => o.Name == "name").Value);
        }

        #endregion
    }
}
