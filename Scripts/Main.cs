using Godot;
using System;
using System.IO;
using System.Linq;

public partial class Main : Control
{
	[Export] FolderPicker fp;
	[Export] DocumentPicker dp;

	string srcFolder = "";
	string dstFolder = "";
	
	public override void _EnterTree()
	{
		fp.Show();
		dp.Hide();

		fp.NextState += () => 
		{
			fp.Hide();
			srcFolder = fp.srcDir;
			dstFolder = fp.destDir;
			
			// Adding the contents of src to the list
			dp.unlDocs.Clear();
			var contents = Directory.EnumerateFiles(srcFolder);
			foreach (string file in contents)
			{
				dp.unlDocs.AddItem(file.Split('\\').Last());
			}
			
			// Adding the contents of dest to the list
			dp.lDocs.Clear();
			contents = Directory.EnumerateFiles(dstFolder);
			foreach (string file in contents)
			{
				dp.lDocs.AddItem(file.Split('\\').Last());
			}
			
			dp.Show();
		};

		dp.PrevState += () => 
		{
			dp.Hide();
			fp.Show();
		};


	}
}
