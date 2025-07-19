using Godot;
using System;

public partial class MainMenu : Control
{
	[Export]
	public string BattleScene { get; set; }
	[Export]
	public string CollectionScene { get; set;}

	
	public void onBattleButtonPressed(){
		var scene = ResourceLoader.Load<PackedScene>(BattleScene).Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.RemoveChild(this);
	}
	
	public void onCollectionButtonPressed(){
		var scene = ResourceLoader.Load<PackedScene>(CollectionScene).Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.RemoveChild(this);
	}
	
	public void onExitButtonPressed(){
		GetTree().Quit();
	}
}
