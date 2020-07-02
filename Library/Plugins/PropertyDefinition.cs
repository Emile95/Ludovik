using Library.Class;

namespace Library.Plugins.PropertyDefinition
{
    public abstract class PropertyDefinition
    {
        #region Properties and Constructor

        public string ClassName { get; set; }

        #endregion

        #region Abstract Methods

        public abstract void AddToEnvironment(Environment env, Parameter[] parameters);

        #endregion

    }
}
