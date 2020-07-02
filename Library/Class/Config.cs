using Library.Plugins.PropertyDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Class
{
    public class Config
    {
        public Dictionary<Type, Property> Props { get; private set; }

        public Config()
        {
            Props = new Dictionary<Type, Property>();
        }

        public void AddProperty(PropertyDefinition definition, Parameter[] parameters)
        {
            Property prop = new Property()
            {
                Definition = definition
            };

            for(int i = 0; i < parameters.Length; i++)
            {
                prop.Parameters.Add(parameters[i]);
            }

            Props.Add(definition.GetType(), prop);
        }

        public Property[] GetProperties()
        {
            return Props.Values.ToArray();
        }

        public Property GetProperty<T>()
        {
            return Props[typeof(T)];
        }
    }
}
