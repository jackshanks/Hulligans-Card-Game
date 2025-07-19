using Godot;
using System;

public partial class LogoutButton : Button
{
	[Export(PropertyHint.File, "*.tscn")]
	private string loginScenePath;

	public override void _Pressed()
	{
		var globalState = GetNode<GlobalState>("/root/GlobalState");
		globalState.ClearSession();
		
		GD.Print("User logged out, session cleared.");

		if (!string.IsNullOrEmpty(loginScenePath))
		{
			GetTree().ChangeSceneToFile(loginScenePath);
		}
	}
}
