using System.Collections.Generic;
using UnityEngine;
using Vapid.ModLoader.API;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	class Console : SingleInstance<Console>
	{
		private const float LOG_ENTRY_HEIGHT = 16;
		private const float CONSOLE_WIDTH = 400;
		private const float CONSOLE_HEIGHT = 600;

		public override string Name { get { return "Vapid's Console"; } }

		private readonly List<LogEntry> entries = new List<LogEntry>();

		private string inputCommandField = "";
		private Vector2 scroll;
		private Rect rect;

		private bool isVisible = true;

		void Awake()
		{
			Application.RegisterLogCallback(OnLog);
			DontDestroyOnLoad(this);

			rect = new Rect(20, 20, CONSOLE_WIDTH, CONSOLE_HEIGHT);
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
			{
				isVisible = !isVisible;
			}
		}

		void OnGUI()
		{
			GUI.skin = VGUI.Skin;

			if (isVisible)
				GUILayout.Window(3712, rect, DoWindow, "Vapid's Console");
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
			GUILayout.Button("Clear");

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
				Elements.Tools.DoCollapseArrow(true, false, LOG_ENTRY_HEIGHT, LOG_ENTRY_HEIGHT);
			}
			else
			{
				// If there is a trace, display a toggleable collapsing arrow
				if (Elements.Tools.DoCollapseArrow(!entry.IsExpanded, true, LOG_ENTRY_HEIGHT, LOG_ENTRY_HEIGHT))
				{
					// If arrow is pressed, toggle it being collapsed
					entry.IsExpanded = !entry.IsExpanded;
				}
			}

			// Style for log message
			var style = GUI.skin.label;
			style.margin.top = 3;
			style.normal.textColor = entry.Color;
			// Display log message
			GUILayout.TextField(entry.log, style);

			GUILayout.EndHorizontal();
			#endregion

			#region DisplayTrace
			// If the entry is expanded, show the log trace
			if (entry.IsExpanded)
			{
				GUILayout.BeginHorizontal();

				var traceStyle = GUI.skin.label;
				traceStyle.normal.textColor = new Color(.9f, .9f, .9f);

				GUILayout.Space(LOG_ENTRY_HEIGHT + 8); // Move trace text to the right to match expansion arrows
				GUILayout.TextArea(entry.trace, traceStyle);

				GUILayout.EndHorizontal();
			}
			#endregion
		}
	}
}