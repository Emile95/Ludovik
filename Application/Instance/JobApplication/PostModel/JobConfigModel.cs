using Library;
using Library.Class;
using Library.Interface;
using Library.Plugins.ParameterDefinition;
using Library.Plugins.PropertyDefinition;
using Library.StandardImplementation.DescriptionPropertyDefinition;
using System.Collections.Generic;

namespace Application.JobApplication.PostModel
{
    public class JobConfigModel : IConfigurable
    {
        #region Properties

        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<Property> Properties { get; set; }


        public class Property
        {
            public string ClassName { get; set; }
            public List<Parameter> Parameters { get; set; }

            public class Parameter
            {
                public string ClassName { get; set; }
                public string Name { get; set; }
                public string Value { get; set; }
            }
        }

        #endregion

        #region IConfigurable Implementations

        public Config GetConfig()
        {
            Config config = new Config();

            config.AddProperty(
                new DescriptionPropertyDefinition(),
                new Parameter[] {
                    new Parameter() {
                        Name = "name",
                        Value = Name
                    },
                    new Parameter() {
                        Name = "description",
                        Value = Description
                    }
                }
            );

            Properties.ForEach(o =>
            {
                List<Parameter> parameters = new List<Parameter>();
                foreach (Property.Parameter p in o.Parameters)
                {
                    parameters.Add(new Parameter()
                    {
                        Definition = PluginStorage.CreateObject<ParameterDefinition>(p.ClassName),
                        Name = p.Name,
                        Value = p.Value
                    });
                }

                config.AddProperty(
                    PluginStorage.CreateObject<PropertyDefinition>(o.ClassName),
                    parameters.ToArray()
                );
            });

            return config;
        }

        public void LoadConfig(Config config) { }

        #endregion
    }
}
