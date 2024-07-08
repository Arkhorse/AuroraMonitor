
using UnityEngine;

namespace AuroraMonitor.Utilities
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class ImageUtilities
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		/// <summary>
		/// Loads and converts a raw image
		/// </summary>
		/// <param name="FolderName">The name of the folder, without parents eg: "TEMPLATE". See: <see cref="MelonLoader.Utils.MelonEnvironment.ModsDirectory"/></param>
		/// <param name="FileName">The name of the image, without extension or foldername</param>
		/// <param name="ext">The extension of the file eg: "jpg". This is provided to allow extension methods to define this parameter</param>
		/// <returns>The image if all related functions work, otherwise null</returns>
		public static Texture2D? GetImage(string FolderName, string FileName, string ext)
		{
			byte[]? file = null;
			string AbsoluteFileName = Path.Combine(MelonLoader.Utils.MelonEnvironment.ModsDirectory, FolderName, $"{FileName}.{ext}");

			Main.Logger.Log("GetImage", FlaggedLoggingLevel.Debug, LoggingSubType.IntraSeparator);
			if (!File.Exists(AbsoluteFileName))
			{
				Main.Logger.Log($"The file requested was not found {AbsoluteFileName}", FlaggedLoggingLevel.Error);
				return null;
			}

			Texture2D texture = new(4096, 4096, TextureFormat.RGBA32, false) { name = FileName };

			try
			{
				file = File.ReadAllBytes(AbsoluteFileName);

				if (file == null)
				{
					Main.Logger.Log($"Attempting to ReadAllBytes failed for image {FileName}.{ext}", FlaggedLoggingLevel.Warning);
					return null;
				}
			}
			catch (DirectoryNotFoundException dnfe)
			{
				Main.Logger.Log($"Directory was not found {FolderName}", FlaggedLoggingLevel.Exception, dnfe);
			}
			catch (FileNotFoundException fnfe)
			{
				Main.Logger.Log($"File was not found {FileName}", FlaggedLoggingLevel.Exception, fnfe);
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to load requested file failed", FlaggedLoggingLevel.Exception, e);
			}

			if (ImageConversion.LoadImage(texture, file))
			{
				Main.Logger.Log($"Successfully loaded file {FileName}", FlaggedLoggingLevel.Debug);
				texture.DontUnload();

				return texture;
			}

			texture.LoadRawTextureData(file);
			texture.Apply();
			texture.DontUnload();
			Main.Logger.Log($"Successfully loaded file {FileName}", FlaggedLoggingLevel.Debug);
			Main.Logger.Log(FlaggedLoggingLevel.Debug, LoggingSubType.Separator);
			return texture ?? null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="FolderName"></param>
		/// <param name="FileName"></param>
		/// <returns></returns>
		public static Texture2D? GetPNG(string FolderName, string FileName)
		{
			Main.Logger.Log($"GetPNG({FileName})", FlaggedLoggingLevel.Debug);

			return GetImage(FolderName, FileName, "png");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="FolderName"></param>
		/// <param name="FileName"></param>
		/// <returns></returns>
		public static Texture2D? GetJPG(string FolderName, string FileName)
		{
			Main.Logger.Log($"GetJPG({FileName})", FlaggedLoggingLevel.Trace);

			return GetImage(FolderName, FileName, "jpg");
		}
	}
	public static class Extensions
	{
		//https://github.dev/NuclearPowered/Reactor/blob/6eb0bf19c30733b78532dada41db068b2b247742/Reactor/Utilities/DefaultBundle.cs#L17#L40
		/// <summary>
		/// Stops <paramref name="obj"/> from being unloaded.
		/// </summary>
		/// <param name="obj">The object to stop from being unloaded.</param>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <returns>Passed <paramref name="obj"/>.</returns>
		public static T DontUnload<T>(this T obj) where T : UnityEngine.Object
		{
			obj.hideFlags |= HideFlags.DontUnloadUnusedAsset;

			return obj;
		}
	}
}
