using Library.Plugins.PropertyDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Class
{
    public class Config
    {
        public List<Property> Props { get; private set; }

        public Config()
        {
            Props = new List<Property>();
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

            Props.Add(prop);
        }

        public List<Property> GetProperties<T>() where T : class
        {
            List<Property> props = new List<Property>();
            foreach(Property prop in Props)
                if(prop.Definition is T)
                    props.Add(prop);
            return props;
        }
    }
}
