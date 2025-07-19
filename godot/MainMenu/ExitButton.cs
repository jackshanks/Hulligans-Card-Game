using Godot;
using System;

public partial class ExitButton : Button
{
	public void _ready(){
		this.Text = Tr("Exit_MainScreen_Button");
	}
}
