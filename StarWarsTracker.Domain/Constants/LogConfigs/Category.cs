namespace StarWarsTracker.Domain.Constants.LogConfigs
{
    /// <summary>
    /// The Constants belonging to this class are related to LogConfig Categories from the appsettings.
    /// </summary>
    public static class Category
    {
        /// <summary>
        /// This default is for the Default Category Configurations
        /// </summary>
        public const string Default = "Default";

        /// <summary>
        /// This Category is for Custom Configurable LogLevels. 
        /// New Config Sections/Keys can be added in appsettings under this Category.
        /// </summary>
        public const string CustomLogLevels = "CustomLogLevels";

        /// <summary>
        /// This Category is for Overriding LogLevels by Namespace.        
        /// Namespace Overrides will be overwritten by ClassName Overrides.
        /// </summary>
        public const string OverrideLogLevelByNameSpace = "OverrideLogLevelByNameSpace";
        
        /// <summary>
        /// This Category is for Overriding LogLevels by ClassName.
        /// ClassName Overrides will overwrite NameSpace Overrides.
        /// </summary>
        public const string OverrideLogLevelByClassName = "OverrideLogLevelByClassName";
    }
}
