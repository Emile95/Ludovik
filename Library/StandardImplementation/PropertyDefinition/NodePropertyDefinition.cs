using Library.Plugins.PropertyDefinition;

namespace Library.StandardImplementation.NodePropertyDefinition
{
    public class NodePropertyDefinition : PropertyDefinition
    {
        public NodePropertyDefinition()
        {
            ClassName = "NodePropertyDefinition";
            ParamDefs.Add(new LabelParameterDefinition.LabelParameterDefinition());
        }

        #region PropertyDefinition Implementation

        #endregion
    }
}
