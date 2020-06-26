namespace Library.Plugins.ParameterDefinition
{
    public abstract class ParameterDefinition
    {
        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public string DefaultValue { get; protected set; }

        public abstract bool VerifyValue(string value);
    }
}
