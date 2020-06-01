namespace Library.Plugins.Parameter
{
    public abstract class Parameter
    {
        public string Name { get; protected set; }
        public string DefaultValue { get; protected set; }
        public abstract string VerifyValue(string value);
        
    }
}
