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
		public ComplexLogger() { }

		/// <summary>
		/// The current logging level. Levels are bitwise added or removed.
		/// </summary>
		public FlaggedLoggingLevel CurrentLevel { get; private set; } = new();

		/// <summary>
		/// Add a flag to the existing list
		/// </summary>
		/// <param name="level">The level to add</param>
		public void AddLevel(FlaggedLoggingLevel level)
		{
			CurrentLevel |= level;

			Log(FlaggedLoggingLevel.Debug, $"Added flag {level}");
		}

		/// <summary>
		/// Remove a flag from the list
		/// </summary>
		/// <param name="level">Level to remove</param>
		public void RemoveLevel(FlaggedLoggingLevel level)
		{
			CurrentLevel &= ~level;

			Log(FlaggedLoggingLevel.Debug, $"Removed flag {level}");
		}

        /// <summary>
        /// Print a log if the current level matches the level given.
        /// </summary>
        /// <param name="level">The level of this message (NOT the existing the level)</param>
        /// <param name="message">Formatted string to use in this log</param>
        /// <param name="exception">The exception, if applicable, to display</param>
        /// <param name="parameters">Any additional params</param>
        /// <remarks>
        /// <para>Use <see cref="WriteSeperator(object[])"/> or <see cref="WriteIntraSeparator(string, object[])"/> for seperators</para>
        /// <para>There is also <see cref="WriteStarter"/> if you require a prebuild startup message to display regardless of user settings (DONT DO THIS)</para>
        /// </remarks>
        public void Log(FlaggedLoggingLevel level, string message, System.Exception? exception = null, params object[] parameters)
		{
			//Log(FlaggedLoggingLevel.Trace, $"");

			if (CurrentLevel.HasFlag(level))
			{
				switch (level)
				{
					case FlaggedLoggingLevel.Trace:
						Write($"[TRACE] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Debug:
						Write($"[DEBUG] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Verbose:
						Write($"[INFO] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Warning:
						Write($"[WARNING] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Error:
						Write($"[ERROR] {message}", parameters);
						break;
					case FlaggedLoggingLevel.Critical:
						if (exception == null)
							Write($"[CRITICAL] {message}", Color.red, FontStyle.Bold, parameters);
						else
							WriteException(message, exception, parameters);
						break;
					default:
						break;
				}
				return;
			}
		}

		/// <summary>
		/// The base log method
		/// </summary>
		/// <param name="message">The formated string to add as the message</param>
		/// <param name="parameters">Any additional params</param>
		public void Write(string message, params object[] parameters)
		{
			Melon<Main>.Logger.Msg(message, parameters);
		}

		/// <summary>
		/// Logs a prebuilt startup message
		/// </summary>
		public void WriteStarter()
		{
			Write($"Mod loaded with v{BuildInfo.Version}");
		}
		/// <summary>
		/// Prints a seperator
		/// </summary>
		/// <param name="parameters">Any additional params</param>
		public void WriteSeperator(params object[] parameters)
		{
			Write("==============================================================================", parameters);
		}
		/// <summary>
		/// Logs an "Intra Seperator", allowing you to print headers
		/// </summary>
		/// <param name="message">The header name. Should be short</param>
		/// <param name="parameters">Any additional params</param>
		public void WriteIntraSeparator(string message, params object[] parameters)
		{
			Write($"=========================   {message}   =========================", parameters);
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
		public void WriteException(string message, System.Exception? exception, params object[] parameters)
		{
			StringBuilder sb = new();

			sb.Append("[EXCEPTION]");
			sb.Append(message);

			if (exception != null) sb.AppendLine(exception.Message);
			else sb.AppendLine("Exception was null");

			Write(sb.ToString(), parameters);
		}
	}
}