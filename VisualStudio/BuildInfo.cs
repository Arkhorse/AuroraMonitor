namespace AuroraMonitor
{
    public static class BuildInfo
    {
        #region Mandatory
        /// <summary>The machine readable name of the mod (no special characters or spaces)</summary>
        public const string Name                            = "AuroraMonitor";
        /// <summary>Who made the mod</summary>
        public const string Author                          = "The Illusion";
        /// <summary>Current version (Using Major.Minor.Build) </summary>
        public const string Version                         = "1.3.0";
        /// <summary>Name used on GUI's, like ModSettings</summary>
        public const string GUIName                         = "Aurora Monitor";
        /// <summary>The minimum Melon Loader version that your mod requires</summary>
        public const string MelonLoaderVersion              = "0.6.1";
		/// <summary>OPTIONAL: Used to define things like build candidits.</summary>
		public const string RevisionString					= "1.3.0 RC2";
        #endregion

        #region Optional
        /// <summary>What the mod does</summary>
        public const string Description                     = "Provides the ability to manage weather, with the emphasis on Aurora atm";
        /// <summary>Company that made it</summary>
        public const string Company                         = null;
        /// <summary>A valid download link</summary>
        public const string DownloadLink                    = null;
        /// <summary>Copyright info</summary>
        /// <remarks>When updating the year, use the StartYear-CurrentYear format</remarks>
        public const string Copyright                       = "Copyright © 2023-2024";
        /// <summary>Trademark info</summary>
        public const string Trademark                       = null;
        /// <summary>Product Name (Generally use the Name)</summary>
        public const string Product                         = "AuroraMonitor";
        /// <summary>Culture info</summary>
        public const string Culture                         = null;
        /// <summary>Priority of your mod. Most of the time you should not need to change this</summary>
        public const int Priority                           = 0;
        #endregion
    }
}
