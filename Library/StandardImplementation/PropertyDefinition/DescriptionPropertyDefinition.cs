using Library.Plugins;
using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.DescriptionPropertyDefinition
{
    public class DescriptionPropertyDefinition : PropertyDefinition
    {
        public DescriptionPropertyDefinition()
        {
            ClassName = "DescriptionProperty";
        }

        #region PropertyDefinition Implementation

        public sealed override ParameterDefinition[] GetParameterDefinitions()
        {
            return new ParameterDefinition[] {
                new StringParameterDefinition.StringParameterDefinition() { Name = "name" },
                new StringParameterDefinition.StringParameterDefinition() { Name = "description" }
            };
        }

        public sealed override bool VerifyIntegrity(string[] values)
        {
            return true;
        }

        #endregion
    }
}
