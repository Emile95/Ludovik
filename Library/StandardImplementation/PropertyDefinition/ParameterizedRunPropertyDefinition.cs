﻿using Library.Plugins;
using Library.Plugins.ParameterDefinition;

namespace Library.StandardImplementation.ParameterizedRunPropertyDefinition
{
    public class ParameterizedRunPropertyDefinition : PropertyDefinition
    {
        public ParameterizedRunPropertyDefinition()
        {
            ClassName = "ParameterizedRunProperty";
        }

        #region PropertyDefinition Implementation

        public sealed override ParameterDefinition[] GetParameterDefinitions()
        {
            return new ParameterDefinition[] {
                new StringParameterDefinition.StringParameterDefinition() { Name = "name" },
                new StringParameterDefinition.StringParameterDefinition() { Name = "description" }
            };
        }

        public override bool VerifyIntegrity(string[] values)
        {
            return true;
        }

        #endregion
    }
}
