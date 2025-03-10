using Godot;
using System;

public partial class DocumentPicker : Control
{
	[Export] public ItemList unlDocs;
	[Export] public ItemList lDocs;
	[Export] public Button nextButton;
	[Signal] public delegate void NextStateEventHandler();
	[Export] Button backButton;
	[Signal] public delegate void PrevStateEventHandler();

	public int selectedIdx;
	public bool isUnlDoc;

    public override void _Ready()
    {
        nextButton.Pressed += () => {EmitSignal(SignalName.NextState);};
		backButton.Pressed += () => {EmitSignal(SignalName.PrevState);};
    }
    void unlSelected(int idx)
	{
		nextButton.Disabled = false;
		lDocs.DeselectAll();
		selectedIdx = idx;
		isUnlDoc = true;
	}

	void lSelected(int idx)
	{
		nextButton.Disabled = false;
		unlDocs.DeselectAll();
		selectedIdx = idx;
		isUnlDoc = false;
	}

	
}
