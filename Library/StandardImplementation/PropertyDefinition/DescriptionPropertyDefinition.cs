using Library.Plugins.PropertyDefinition;

namespace Library.StandardImplementation.DescriptionPropertyDefinition
{
    public class DescriptionPropertyDefinition : PropertyDefinition
    {
        public DescriptionPropertyDefinition()
        {
            ClassName = "DescriptionPropertyDefinition";
            ParamDefs.Add(new StringParameterDefinition.StringParameterDefinition() { Name = "name" });
            ParamDefs.Add(new StringParameterDefinition.StringParameterDefinition() { Name = "description" });
        }

        #region PropertyDefinition Implementation

        #endregion
    }
}
