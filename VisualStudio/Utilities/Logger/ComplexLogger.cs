// ---------------------------------------------
// ComplexLogger - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------
using System.Text;
using AuroraMonitor.Utilities.Enums;

namespace AuroraMonitor.Utilities.Logger
{
	public class ComplexLogger
	{
		/// <summary>
		/// The current logging level. Levels are bitwise added or removed.
		/// </summary>
		private static FlaggedLoggingLevel CurrentLevel { get; set; } = new();

		/// <summary>
		/// Used to retrieve the current level outside this class
		/// </summary>
		/// <returns>The currently set flags</returns>
		public static FlaggedLoggingLevel GetCurrentLevel() => CurrentLevel;

		/// <summary>
		/// Add a flag to the existing list
		/// </summary>
		/// <param name="level">The level to add</param>
		public static void AddLevel<T>(FlaggedLoggingLevel level) where T : MelonBase
		{
			CurrentLevel |= level;

			Log<T>(FlaggedLoggingLevel.Debug, $"Added flag {level}");
		}

		/// <summary>
		/// Remove a flag from the list
		/// </summary>
		/// <param name="level">Level to remove</param>
		public static void RemoveLevel<T>(FlaggedLoggingLevel level) where T : MelonBase
        {
			CurrentLevel &= ~level;

			Log<T>(FlaggedLoggingLevel.Debug, $"Removed flag {level}");
		}

        /// <summary>
        /// Print a log if the current level matches the level given. This uses the <see cref="Logging"/> class previously implemented
        /// </summary>
		/// <typeparam name="T">The main class to target for the Melon logging instance</typeparam>
        /// <param name="level">The level of this message (NOT the existing the level)</param>
        /// <param name="message">Formatted string to use in this log</param>
        /// <param name="exception">The exception, if applicable, to display</param>
        /// <param name="parameters">Any additional params</param>
        /// <remarks>
        /// <para>Use <see cref="WriteSeperator{T}(object[])"/> or <see cref="WriteIntraSeparator{T}(string, object[])"/> for seperators</para>
        /// <para>There is also <see cref="WriteStarter{T}"/> if you require a prebuild startup message to display regardless of user settings (DONT DO THIS)</para>
        /// </remarks>
        public static void Log<T>(FlaggedLoggingLevel level, string message, Exception? exception = null, params object[] parameters) where T : MelonBase
		{
			if (CurrentLevel.HasFlag(level))
			{
				switch (level)
				{
					case FlaggedLoggingLevel.Trace:
						Write<T>($"[TRACE] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Debug:
						Write<T>($"[DEBUG] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Information:
						Write<T>($"[INFO] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Warning:
						Write<T>($"[WARNING] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Error:
						Write<T>($"[ERROR] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Critical:
						if (exception == null)
							Write<T>($"[CRITICAL] {message}", parameters);
						else
							WriteException<T>(message, exception, parameters);
						break;
					default:
						Log<T>(FlaggedLoggingLevel.Debug, $"The current logging level does not match the given log level, Current: {CurrentLevel}, Given: {level}");
						break;
				}
			}
			else return;
		}

        /// <summary>
        /// The base log method
        /// </summary>
        /// <param name="message">The formated string to add as the message</param>
        /// <param name="parameters">Any additional params</param>
        public static void Write<T>(string message, params object[] parameters) where T : MelonBase
        {
            Melon<T>.Logger.Msg(message, parameters);
        }

        /// <summary>
        /// Logs a prebuilt startup message
        /// </summary>
        public static void WriteStarter<T>() where T : MelonBase
		{
            Write<T>($"Mod loaded with v{BuildInfo.Version}");
        }
		/// <summary>
		/// Prints a seperator
		/// </summary>
		/// <param name="parameters">Any additional params</param>
		public static void WriteSeperator<T>(params object[] parameters) where T : MelonBase
		{
            Write<T>("==============================================================================", parameters);
        }
		/// <summary>
		/// Logs an "Intra Seperator", allowing you to print headers
		/// </summary>
		/// <param name="message">The header name. Should be short</param>
		/// <param name="parameters">Any additional params</param>
		public static void WriteIntraSeparator<T>(string message, params object[] parameters) where T : MelonBase
		{
            Write<T>($"=========================   {message}   =========================", parameters);
        }
		/// <summary>
		/// Prints a log with <c>[EXCEPTION]</c> at the start.
		/// </summary>
		/// <param name="message">The formated string to add as the message. Displayed before the exception</param>
		/// <param name="exception">The exception thrown</param>
		/// <param name="parameters">Any additional params</param>
		/// <remarks>
		/// <para>This is done as building the exception otherwise can be tedious</para>
		/// </remarks>
		public static void WriteException<T>(string message, Exception? exception, params object[] parameters) where T : MelonBase
		{
			StringBuilder sb = new();

			sb.Append("[EXCEPTION]");
			sb.Append(message);

			if (exception != null) sb.AppendLine(exception.Message);
			else sb.AppendLine("Exception was null");

			Write<T>(sb.ToString(), parameters);
		}
	}
}