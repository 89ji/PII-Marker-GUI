using Godot;
using System;

public partial class TextMarker : Control
{
	[Export] public TextEdit TextField;
	[Export] public PopupMenu Menu;
	
	[Signal] public delegate void PrevStateEventHandler();

	public override void _Ready()
	{
		var phil = new[] { "ssn", "asdf" };
		foreach (var phi in phil) Menu.AddItem(phi);
	}

	public override void _Process(double delta)
	{
		string txt = TextField.GetSelectedText();
		//GD.Print(txt);
		
	}

	void TextBoxInputHandler(InputEvent e)
	{
		if (e is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right)
			{
				
				Menu.Show();
			}
		}
		
	}
}
