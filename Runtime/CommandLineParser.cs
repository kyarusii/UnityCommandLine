using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Calci.CommandLine
{
    public class CommandLineParser
    {
        private static CommandLineParser _inst;

        public static bool HasKey(string key)
        {
            if (_inst == null)
            {
                _inst = new CommandLineParser();
            }

            return (arguments.ContainsKey(key));
        }
        
        public static bool GetBool(string arg, bool defaultValue = false)
        {
            if (_inst == null)
            {
                _inst = new CommandLineParser();
            }
            
            if (arguments.ContainsKey(arg))
            {
                if (bool.TryParse(arguments[arg].value, out bool result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        public static float GetFloat(string arg, float defaultValue = 0.0f)
        {
            if (_inst == null)
            {
                _inst = new CommandLineParser();
            }
            
            if (arguments.ContainsKey(arg))
            {
                if (float.TryParse(arguments[arg].value, out var result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }

        public static string GetString(string arg, string defaultValue = "")
        {
            if (_inst == null)
            {
                _inst = new CommandLineParser();
            }
            
            if (arguments.ContainsKey(arg))
            {
                return arguments[arg].value;
            }
            
            return defaultValue;
        }

        public static int GetInt(string arg, int defaultValue = 0)
        {
            if (_inst == null)
            {
                _inst = new CommandLineParser();
            }
            
            if (arguments.ContainsKey(arg))
            {
                if (int.TryParse(arguments[arg].value, out var result))
                {
                    return result;
                }
            }
            
            return defaultValue;
        }

        private static readonly Dictionary<string, CommandLineArgument> arguments = new Dictionary<string, CommandLineArgument>();
        private static readonly List<string> options = new List<string>();

        private CommandLineParser()
        {
            // Debug.LogError(Environment.CommandLine);
#if UNITY_EDITOR
            LoadFromSetting();
#else            
            LoadFromEnvironment();
#endif            
        }

#if UNITY_EDITOR
        public static void LoadFromSetting()
        {
            arguments.Clear();
            
            foreach (CommandLineArgument argument in CommandLineSetting.GetOrCreateSettings().arguments)
            {
                arguments.Add(argument.key, argument);
            }
        }
#endif
        
        private static void LoadFromEnvironment()
        {
            arguments.Clear();
            options.Clear();
            
            var cmds = Environment.GetCommandLineArgs();

            foreach (string cmd in cmds)
            {
                if (cmd.Contains("--"))
                {
                    if (cmd.Contains("="))
                    {
                        string[] block = cmd.Replace("--","").Split('=');
                        arguments.Add(block[0], new CommandLineArgument()
                        {
                            key = block[0],
                            value = block[1],
                        });
                    }
                    else
                    {
                        options.Add(cmd.Replace("--",""));
                    }
                }
            }
        }
    }
}
