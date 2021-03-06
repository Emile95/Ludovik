﻿using Library.Class;

namespace Library.Plugins.PropertyDefinition
{
    public abstract class PropertyDefinition
    {
        #region Properties and Constructor

        public string ClassName { get; set; }

        #endregion

        #region Abstract Methods

        public abstract void Apply(Environment env, Parameter[] parameters, FailedBuildTokenSource failedBuildTokenSource, LoggerList loggers);

        #endregion

    }
}
