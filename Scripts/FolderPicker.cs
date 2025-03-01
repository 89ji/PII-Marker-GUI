using Godot;
using System;

public partial class FolderPicker : Control
{
	[Export] FileDialog sourceDialog;
	[Export] FileDialog destinationDialog;
	[Export] RichTextLabel srcTooltip;
	[Export] RichTextLabel destTooltip;
	[Signal] public delegate void NextStateEventHandler();

	public string srcDir = "";
	public string destDir = "";

	public override void _Ready()
	{
		
	}
	void ShowSourcePicker()
	{
		sourceDialog.Show();
	}

	void ShowDestinationPicker()
	{
		destinationDialog.Show();
	}

	void SrcPicked(string path)
	{
		srcDir = path;
		srcTooltip.TooltipText = srcDir;
	}

	void DestPicked(string path)
	{
		destDir = path;
		destTooltip.TooltipText = destDir;
	}

	void GoNext()
	{
		bool valid = true;
		valid &= !srcDir.Equals(destDir);
		valid &= srcDir != "";
		valid &= destDir != "";
		if (valid) EmitSignal(SignalName.NextState);
	}
}
