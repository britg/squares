using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public static class Builder
{

	public static string[] scenes {
		get {
			return new string[] {
				"Assets/Squares/Scenes/Play.unity"
			};
		}
	}

	[MenuItem("Squares/Patch")]
	public static void Both () {
		ClientMac();
		ClientWin();
	}

	[MenuItem("Squares/Client Mac %&c")]
	public static void ClientMac () {
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneOSXIntel);
		BuildPipeline.BuildPlayer(scenes, "Patchie/_current/Squares_mac.app", BuildTarget.StandaloneOSXIntel, BuildOptions.AutoRunPlayer);
	}
	
	[MenuItem("Dispatch/Client Win")]
	public static void ClientWin () {
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
		BuildPipeline.BuildPlayer(scenes, "Patchie/_current/Squares_win.exe", BuildTarget.StandaloneWindows, BuildOptions.AutoRunPlayer);
	}
	
}
