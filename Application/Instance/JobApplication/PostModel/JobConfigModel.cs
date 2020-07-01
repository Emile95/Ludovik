using System;
using System.Collections.Generic;

namespace Application.JobApplication.PostModel
{
    public class JobConfigModel
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PropConfig> PropConfigs { get; set; }

        public class PropConfig
        {
            public string ClassName { get; set; }
            public List<Tuple<ParamConfig,List<string>>> ParamConfigs { get; set; }
            public class ParamConfig 
            {
                public string ClassName { get; set; }
            }
        }
    }
}
