using Godot;
using System;

public partial class GlobalState : Node
{
	public long UserId { get; set; }
	public string DisplayName { get; set; }
	public const string HTTPString = "http://213.249.185.239:9001/api/";
	
	private const string SavePath = "user://session.json";
	
	public void SaveSession()
	{
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		
		var data = new Godot.Collections.Dictionary
		{
			{ "UserId", UserId },
			{ "DisplayName", DisplayName }
		};
		
		string jsonString = Json.Stringify(data);
		file.StoreString(jsonString);
	}
	
	public bool LoadSession()
	{
		if (!FileAccess.FileExists(SavePath))
		{
			return false;
		}

		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
		string content = file.GetAsText();
		
		var json = new Json();
		var error = json.Parse(content);
		if (error != Error.Ok)
		{
			GD.PrintErr("Error parsing session file!");
			return false;
		}

		var data = json.Data.AsGodotDictionary();
		UserId = (long)data["UserId"];
		DisplayName = (string)data["DisplayName"];

		GD.Print($"Session loaded for user: {DisplayName}");
		return true;
	}
	
	public void ClearSession()
	{
		if (FileAccess.FileExists(SavePath))
		{
			Error err = DirAccess.RemoveAbsolute(SavePath);
			if (err != Error.Ok)
			{
				GD.PrintErr("Failed to delete session file.");
			}
		}
		UserId = 0;
		DisplayName = null;
	}
}
