using Library.Plugins;

namespace Library.StandardImplementation.ParameterizedRunPropertyDefinition
{
    public class ParameterizedRunPropertyDefinition : PropertyDefinition
    {
        public ParameterizedRunPropertyDefinition()
        {
            ClassName = "ParameterizedRunProperty";
            Description = "Parameters for the run";
        }

        #region PropertyDefinition Implementation

        public override bool VerifyIntegrity(string[] values)
        {
            return true;
        }

        #endregion
    }
}
