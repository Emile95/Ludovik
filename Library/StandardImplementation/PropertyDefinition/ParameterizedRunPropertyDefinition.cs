using Library.Class;
using Library.Plugins.PropertyDefinition;

namespace Library.StandardImplementation.ParameterizedRunPropertyDefinition
{
    public class ParameterizedRunPropertyDefinition : PropertyDefinition
    {
        public ParameterizedRunPropertyDefinition()
        {
            ClassName = "ParameterizedRunPropertyDefinition";
        }

        #region PropertyDefinition Implementation

        public sealed override void Apply(Environment env, Parameter[] parameters, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers)
        {
            foreach(Parameter parameter in parameters)
            {
                env.Properties.Add(parameter.Name, parameter.Value);
            }
            
        }

        #endregion
    }
}
