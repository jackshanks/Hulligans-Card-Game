using Godot;
using System;
using System.Text;

public partial class LoginButton : Button
{
	[Export]
	private HttpRequest httpRequest;
	[Export]
	private Label errorLabel;
	[Export(PropertyHint.File, "*.tscn")]
	private string nextScenePath;
	
	private string requestString = "http://localhost:5003/api/Users/id?";
	
	public override void _Ready()
	{
		var globalState = GetNode<GlobalState>("/root/GlobalState");
		if (globalState.LoadSession())
		{
			if (!string.IsNullOrEmpty(nextScenePath))
			{
				GetTree().ChangeSceneToFile(nextScenePath);
				return;
			}
		}
		if (httpRequest == null)
		{
			GD.PrintErr("HTTPRequest node not assigned in the Inspector!");
			return;
		}
		httpRequest.RequestCompleted += OnRequestCompleted;
	}
	
	private void _on_pressed()
	{
		var authLine = GetNode<LineEdit>("auth_line");
		var emailLine = GetNode<LineEdit>("email_line");
		
		if (string.IsNullOrWhiteSpace(emailLine.Text) || string.IsNullOrWhiteSpace(authLine.Text))
		{
			if (errorLabel != null) errorLabel.Text = "Email and password cannot be empty.";
			return;
		}
		
		Disabled = true; //Disable button to disallow repeat requests 
		
		string authToken = Uri.EscapeDataString(authLine.Text);
		string emailToken = Uri.EscapeDataString(emailLine.Text);
		
		httpRequest.Request($"{requestString}email={emailToken}&auth={authToken}");
	}
	
	private void OnRequestCompleted(long result, long responseCode, string[] headers, byte[] body)
	{
		Disabled = false; //Reallow requests
		
		if (responseCode == 401 || responseCode == 404)
		{
			if (errorLabel != null) errorLabel.Text = "Invalid email or password.";
			return;
		}
		if (responseCode != 200 || body.Length == 0)
		{
			if (errorLabel != null) errorLabel.Text = "Could not connect to the server.";
			GD.PrintErr($"Request failed! Code: {responseCode}");
			return;
		}
		
		var json = Json.ParseString(Encoding.UTF8.GetString(body));
		if (json.VariantType == Variant.Type.Nil)
		{
			if (errorLabel != null) errorLabel.Text = "Invalid server response.";
			return;
		}
		
		var jsonDict = json.AsGodotDictionary();
		if (!jsonDict.ContainsKey("userId") || !jsonDict.ContainsKey("displayName"))
		{
			if (errorLabel != null) errorLabel.Text = "Incomplete data from server.";
			return;
		}
		
		var globalState = GetNode<GlobalState>("/root/GlobalState");
		globalState.UserId = (long)jsonDict["userId"];
		globalState.DisplayName = (string)jsonDict["displayName"];
		globalState.SaveSession();
		
		GD.Print($"Login successful! Welcome, {globalState.DisplayName} (ID: {globalState.UserId})");

		if (!string.IsNullOrEmpty(nextScenePath))
		{
			GetTree().ChangeSceneToFile(nextScenePath);
		}
		else
		{
			if (errorLabel != null) errorLabel.Text = "Next scene not configured!";
		}
	}
}
