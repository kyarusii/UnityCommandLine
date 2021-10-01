using UnityEngine;

namespace Game.Shared.CommandLine
{
	public class CommandLineExample : MonoBehaviour
	{
		public string[] boolValues;
		public string[] floatValues;
		public string[] intValues;
		public string[] stringValues;

		private void Awake()
		{
			foreach (string key in boolValues)
			{
				Debug.Log($"{key} : {CommandLineParser.GetBool(key)}");
			}
			
			foreach (string key in floatValues)
			{
				Debug.Log($"{key} : {CommandLineParser.GetFloat(key)}");
			}
			
			foreach (string key in intValues)
			{
				Debug.Log($"{key} : {CommandLineParser.GetInt(key)}");
			}
			
			foreach (string key in stringValues)
			{
				Debug.Log($"{key} : {CommandLineParser.GetString(key)}");
			}
		}
	}
}