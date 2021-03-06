﻿namespace Library.Plugins.ParameterDefinition
{
    public abstract class ParameterDefinition
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract bool VerifyValue(string value);
    }
}
