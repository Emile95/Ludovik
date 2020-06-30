using Library.Plugins;
using System.Collections.Generic;

namespace Library.Class
{
    public class Config
    {
        private List<System.Tuple<PropertyDefinition, string[]>> _props;

        public Config()
        {
            _props = new List<System.Tuple<PropertyDefinition, string[]>>();
        }

        public void AddProperty(string[] values, PropertyDefinition propertyDefinition)
        {
            _props.Add(new System.Tuple<PropertyDefinition, string[]>(
                propertyDefinition,
                values
            ));
        }

        public string[] GetPropertyValues<T>(object key)
        {
            return null;
        }

        public bool ValidateProperties()
        {
            foreach(System.Tuple<PropertyDefinition, string[]> param in _props)
                if (!param.Item1.VerifyIntegrity(param.Item2))
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
