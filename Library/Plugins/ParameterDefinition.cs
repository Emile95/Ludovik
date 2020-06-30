namespace Library.Plugins.ParameterDefinition
{
    public abstract class ParameterDefinition
    {
        protected string _className;

        public string Name { get; set; }

        public string Description { get; set; }

        public abstract bool VerifyValue(string value);
    }
}
