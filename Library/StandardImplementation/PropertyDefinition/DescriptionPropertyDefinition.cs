using Library.Plugins;

namespace Library.StandardImplementation.DescriptionPropertyDefinition
{
    public class DescriptionPropertyDefinition : PropertyDefinition
    {
        public DescriptionPropertyDefinition()
        {
            ClassName = "DescriptionProperty";
            ParamDefs.Add(new StringParameterDefinition.StringParameterDefinition() { Name = "name" });
            ParamDefs.Add(new StringParameterDefinition.StringParameterDefinition() { Name = "description" });
        }

        #region PropertyDefinition Implementation

        public sealed override bool VerifyIntegrity(string[] values)
        {
            return true;
        }

        #endregion
    }
}
