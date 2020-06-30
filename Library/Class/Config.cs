using Library.Plugins;
using System.Collections.Generic;

namespace Library.Class
{
    public class Config
    {
        private Dictionary<PropertyDefinition, string[]> _props;

        public Config()
        {
            _props = new Dictionary<PropertyDefinition, string[]>();
        }

        public void AddProperty(PropertyDefinition propertyDefinition, string[] values)
        {
            _props.Add(propertyDefinition, values);
        }

        public string[] GetPropertyValues<T>() where T : PropertyDefinition
        {
            foreach (KeyValuePair<PropertyDefinition, string[]> param in _props)
                if(param.Key is T) return param.Value;
            return null;
        }

        public bool ValidateProperties()
        {
            foreach(KeyValuePair<PropertyDefinition, string[]> param in _props)
                if (!param.Key.VerifyIntegrity(param.Value))
                    return false;
            return true;
        }

        public bool ValidateProperty(int i)
        {
            if (!_props[i].Item1.VerifyIntegrity(_props[i].Item2))
                return false;
            return true;
        }
    }
}
