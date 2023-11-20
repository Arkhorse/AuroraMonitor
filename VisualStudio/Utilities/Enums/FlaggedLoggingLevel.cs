// ---------------------------------------------
// FlaggedLoggingLevel - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------

namespace AuroraMonitor.Utilities.Enums
{
	[System.Flags]
	public enum FlaggedLoggingLevel
	{
		None 		= 0,
		Trace 		= 1,
		Debug 		= 2,
		Verbose 	= 4,
		Warning 	= 8,
		Error 		= 16,
		Critical 	= 32
	}
}
