using Godot;
using System;

public partial class BattleButton : Button
{
	public void _ready(){
		this.Text = Tr("Battle_MainScreen_Button");
	}
}
