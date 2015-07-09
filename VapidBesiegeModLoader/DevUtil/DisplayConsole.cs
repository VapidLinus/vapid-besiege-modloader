using System.Collections.Generic;
using UnityEngine;
using Vapid.ModLoader.API;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	class Console : SingleInstance<Console>
	{
		public override string Name { get { return "Vapid's Console"; } }

		private readonly int windowID = WindowID.Next();

		private readonly List<LogEntry> entries = new List<LogEntry>();

		private string inputCommandField = "";
		private Vector2 scroll;
		private Rect rect;

		private bool isVisible;

		void Awake()
		{
			Application.RegisterLogCallback(OnLog);
			VapidModLoader.ActivateModule(this);

			rect = new Rect(20, 20, Elements.Settings.ConsoleSize.x, Elements.Settings.ConsoleSize.y);
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
			{
				isVisible = !isVisible;
			}
		}

		void OnGUI()
		{
			GUI.skin = VGUI.Skin;

			if (isVisible)
				rect = GUILayout.Window(windowID, rect, DoWindow, "Vapid's Console");
		}

		void OnLog(string log, string trace, LogType type)
		{
			AddLog(new LogEntry(type, log, trace));
		}

		internal void AddLog(LogEntry entry)
		{
			entries.Add(entry);
		}

		void DoWindow(int id)
		{
			GUI.DragWindow(new Rect(0, 0, rect.width, GUI.skin.window.padding.top));

			#region Console Log
			scroll = GUILayout.BeginScrollView(scroll, false, true);

			foreach (var entry in entries)
			{
				DoLogEntry(entry);
			}

			GUILayout.EndScrollView();
			#endregion

			#region Control bar
			GUILayout.BeginHorizontal();

			inputCommandField = GUILayout.TextField(inputCommandField, GUILayout.Width(300));
			GUILayout.Button("Execute");
			if (GUILayout.Button("Clear"))
			{
				entries.Clear();
			}

			GUILayout.EndHorizontal();
			#endregion
		}

		static void DoLogEntry(LogEntry entry)
		{
			#region Arrow & Log
			GUILayout.BeginHorizontal();

			// Check if there is a trace
			if (entry.trace == null || entry.trace.Length < 3)
			{
				// If there is no trace, display a disabled arrow
				Elements.Tools.DoCollapseArrow(false, false);
			}
			else
			{
				// If there is a trace, display a toggleable collapsing arrow
				if (Elements.Tools.DoCollapseArrow(entry.IsExpanded))
				{
					// If arrow is pressed, toggle it being collapsed
					entry.IsExpanded = !entry.IsExpanded;
				}
			}

			// Style for log message
			var style = new GUIStyle(Elements.Labels.LogEntry)
			{
				normal = { textColor = entry.Color }
			};
			// Display log message
			GUILayout.TextField(entry.log, style, GUILayout.Height(Elements.Settings.LogEntrySize));

			GUILayout.EndHorizontal();
			#endregion

			#region DisplayTrace
			// If the entry is expanded, show the log trace
			if (entry.IsExpanded)
			{
				GUILayout.BeginHorizontal();

				var traceStyle = GUI.skin.label;
				traceStyle.normal.textColor = new Color(.9f, .9f, .9f);

				GUILayout.Space(Elements.Settings.LogEntrySize + 8); // Move trace text to the right to match expansion arrows // TODO: Magic value
				GUILayout.TextArea(entry.trace, traceStyle);

				GUILayout.EndHorizontal();
			}
			#endregion
		}
	}
}