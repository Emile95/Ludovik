using System.Collections.Generic;

namespace Application.JobApplication.PostModel
{
    public class JobConfigModel
    {
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
    }
}
