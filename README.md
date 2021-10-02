# UnityCommandLine

## Editor
![image](https://user-images.githubusercontent.com/79823287/135699649-8142430f-d98e-4e6a-b688-71ac26460acb.png)

## Standalone
`Uber\AutoChessFramework.exe --server=true --address=127.0.0.1 --port=29401 --serverPort=29320 --matchHost=127.0.0.1 --matchPortServer=13982 --matchPortClient=12982 --overrideMatchServer=true --width=1600 --height=900 --fullScreen=false`

## Code
```csharp
/// <summary>
/// Parse overriding commandlines.
/// </summary>
private IEnumerator ParseArgument()
{
	if (CommandLineParser.GetBool("overrideMatchServer"))
	{
		matchmakingServerHost = CommandLineParser.GetString("matchHost", "127.0.0.1");
		matchmakingServerPort = CommandLineParser.GetInt("matchPortServer");
			
		matchmakingServerParsed = true;	
	}
	else
	{
		// Download from CDN
		yield return Registry.Get();	
				
		matchmakingServerHost = Registry.Host;
		matchmakingServerPort = Registry.Port + 1000;
					
		ServerLog.Verbose(L("MM Host : ") + matchmakingServerHost);
		ServerLog.Verbose(L("MM Port : ") + matchmakingServerPort);
	}
}
```
