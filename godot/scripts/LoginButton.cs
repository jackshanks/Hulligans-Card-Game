using Godot;
using System;
using System.Text;

public partial class LoginButton : Button
{
	[Export]
	private HttpRequest httpRequest;
	
	private string Connection = "http://localhost:5003/api/";
	
	public override void _Ready()
	{
		if (httpRequest == null)
		{
			GD.PrintErr("HTTPRequest node not assigned in the Inspector!");
			return;
		}
		httpRequest.RequestCompleted += OnRequestCompleted;
	}
	
	private void _on_pressed()
	{
		httpRequest.Request(Connection + "Users/id?email=jacklshanks%40gmail.com&auth=booger");
	}
	
	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		Godot.Collections.Dictionary json = Json.ParseString(Encoding.UTF8.GetString(body)).AsGodotDictionary();
		GD.Print(json["displayName"]);
	}
}
