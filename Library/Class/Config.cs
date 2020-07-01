using Library.Plugins;
using System.Collections.Generic;

namespace Library.Class
{
    public class Config
    {
        public Dictionary<PropertyDefinition, string[]> Props { get; private set; }

        public Config()
        {
            Props = new Dictionary<PropertyDefinition, string[]>();
        }

        public void AddProperty(PropertyDefinition propertyDefinition, string[] values)
        {
            Props.Add(propertyDefinition, values);
        }

        public string[] GetPropertyValues<T>() where T : PropertyDefinition
        {
            foreach (KeyValuePair<PropertyDefinition, string[]> prop in Props)
                if(prop.Key is T) return prop.Value;
            return null;
        }

        public bool ValidateProperties()
        {
            foreach(KeyValuePair<PropertyDefinition, string[]> prop in Props)
                if (!prop.Key.VerifyIntegrity(prop.Value))
                    return false;
            return true;
        }

        public bool ValidateProperty<T>() where T : class, new()
        {
            foreach (KeyValuePair<PropertyDefinition, string[]> prop in Props)
                if (!prop.Key.VerifyIntegrity(prop.Value))
                    return false;
            return true;
        }
    }
}
