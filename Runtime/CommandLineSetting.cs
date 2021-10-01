#if UNITY_EDITOR

using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Calci.CommandLine
{
	internal sealed class CommandLineSetting : ScriptableObject
	{
		private static CommandLineSetting instance;
		
		private const string filePath = "Assets/Editor Default Resources/CommandLineSetting.asset";
		private const string folderPath = "Assets/Editor Default Resources";

		public CommandLineArgument[] arguments;

		public string BuildCommandLine()
		{
			StringBuilder sb = new StringBuilder(1024);
			foreach (CommandLineArgument arg in arguments)
			{
				sb.Append("-");
				sb.Append(arg.key);
				sb.Append(" ");
				sb.Append("\"");
				sb.Append(arg.value);
				sb.Append("\"");
				sb.Append(" ");
			}

			return sb.ToString();
		}

		[InitializeOnLoadMethod]
		private static void Init()
		{
			CommandLineSetting setting = GetOrCreateSettings();
		}
		
		public static CommandLineSetting GetOrCreateSettings()
		{
			if (instance != null)
			{
				return instance;
			}

			instance = EditorGUIUtility.Load(filePath) as CommandLineSetting;

			if (instance == null)
			{
				instance = CreateInstance<CommandLineSetting>();

				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
					AssetDatabase.ImportAsset(folderPath);
				}

				AssetDatabase.CreateAsset(instance, filePath);
				AssetDatabase.SaveAssets();
			}

			return instance;
		}

		internal class SettingsProviderRegister
		{
			[SettingsProvider]
			public static SettingsProvider CreateFromSettingsObject()
			{
				Object settingsObj = GetOrCreateSettings();
				
				AssetSettingsProvider provider =
					AssetSettingsProvider.CreateProviderFromObject($"Project/Command Line", settingsObj);

				provider.keywords =
					SettingsProvider.GetSearchKeywordsFromSerializedObject(
						new SerializedObject(settingsObj));
				return provider;
			}
		}
	}
}

#endif
