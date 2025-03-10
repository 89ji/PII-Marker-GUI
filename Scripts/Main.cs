using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class Main : Control
{
	[Export] FolderPicker fp;
	[Export] DocumentPicker dp;
	[Export] TextMarker tm;

	string srcFolder = "";
	string dstFolder = "";

	Dictionary<string, string> sname2path = new();
	Dictionary<string, string> dname2path = new();
	
	
	public override void _Ready()
	{
		fp.Show();
		dp.Hide();

		fp.NextState += LoadDocsFromFolder;
		dp.NextState += LoadTextToEditor;
		dp.PrevState += () => 
		{
			dp.Hide();
			fp.Show();
			dp.nextButton.Disabled = true;
			sname2path.Clear();
			dname2path.Clear();
		};
	}

	void LoadDocsFromFolder()
	{
		fp.Hide();
		srcFolder = fp.srcDir;
		dstFolder = fp.destDir;
			
		var srcContents = Directory.EnumerateFiles(srcFolder);
		var destContents = Directory.EnumerateFiles(dstFolder);
		
		foreach (var path in srcContents)
		{
			string filename = path.Split('\\').Last();
			sname2path.Add(filename, path);
		}
		
		foreach (var path in destContents)
		{
			string filename = path.Split('\\').Last();
			dname2path.Add(filename, path);
		}

		foreach (var key in  dname2path.Keys) if (sname2path.ContainsKey(key)) sname2path.Remove(key);
		
		// Adding the contents of src to the list
		dp.unlDocs.Clear();
		foreach (string file in sname2path.Keys) dp.unlDocs.AddItem(file);
			
		// Adding the contents of dest to the list
		dp.lDocs.Clear();
		foreach (string file in dname2path.Keys) dp.lDocs.AddItem(file);
			
		dp.Show();
	}

	void LoadTextToEditor()
	{
		dp.Hide();
		int idx = dp.selectedIdx;
		bool isUnl = dp.isUnlDoc;

		string path = (isUnl ? sname2path : dname2path)[(isUnl ? dp.unlDocs : dp.lDocs).GetItemText(idx)];
		StreamReader sr = new(path);
		string contents = sr.ReadToEnd();
		var nl = contents.Split('\n').ToList();
		nl.RemoveAll(s => (s == "\r"));
		sr.Close();

		contents = "";
		foreach (var line in nl) contents += line + "\n\r";
		tm.TextField.Text = contents;
		tm.Show();
	}
}
