using Godot;
using System;

public partial class TextMarker : Control
{
	[Export] public TextEdit TextField;
	
	[Signal] public delegate void PrevStateEventHandler();

	public override void _Process(double delta)
	{
		string txt = TextField.GetSelectedText();
		GD.Print(txt);
	}
}
