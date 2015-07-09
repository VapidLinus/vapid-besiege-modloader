﻿using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Buttons
	{
		public GUIStyle Default { get; set; }
		public GUIStyle ComponentField { get; set; }
		public GUIStyle LogEntryLabel { get; set; }
		public GUIStyle ThinNoTopBotMargin { get; set; }
		public GUIStyle ArrowExpanded { get; set; }
		public GUIStyle ArrowCollapsed { get; set; }
		public GUIStyle ArrowDarkExpanded { get; set; }
		public GUIStyle ArrowDarkCollapsed { get; set; }

		internal Buttons()
		{
			var textColor = Elements.Colors.DefaultText;

			Default = new GUIStyle
			{
				normal = { background = Elements.LoadImage("blue-normal.png"), textColor = textColor },
				hover = { background = Elements.LoadImage("blue-light.png"), textColor = textColor },
				active = { background = Elements.LoadImage("blue-dark.png"), textColor = textColor },
				border = new RectOffset(4, 4, 4, 4),
				padding = Elements.Settings.DefaultPadding,
				margin = Elements.Settings.DefaultMargin,
				alignment = TextAnchor.MiddleCenter,
				fontSize = 14,
				fontStyle = FontStyle.Bold
			};

			var margin = Elements.Settings.LowMargin;
			margin.left = 0;
			ComponentField = new GUIStyle(Default)
			{
				margin = margin,
				padding = Elements.Settings.LowPadding,
				fontSize = 13
			};

			LogEntryLabel = new GUIStyle(Elements.Labels.LogEntry)
			{
				hover = { background = Elements.LoadImage("blue-light.png"), textColor = textColor },
				active = { background = Elements.LoadImage("blue-dark.png"), textColor = Elements.Colors.LowlightText }
			};

			ThinNoTopBotMargin = new GUIStyle(Default)
			{
				margin = new RectOffset(Default.margin.left, Default.margin.right, 0, 0)
			};

			ArrowCollapsed = new GUIStyle
			{
				normal = { background = Elements.LoadImage("arrow-normal-right.png") },
				hover = { background = Elements.LoadImage("arrow-hover-right.png") },
				active = { background = Elements.LoadImage("arrow-disabled-right.png") },
				alignment = TextAnchor.MiddleCenter,
				margin = new RectOffset(0, 6, 2, 2)
			};

			ArrowExpanded = new GUIStyle(ArrowCollapsed)
			{
				normal = { background = Elements.LoadImage("arrow-normal-down.png") },
				hover = { background = Elements.LoadImage("arrow-hover-down.png") },
				active = { background = Elements.LoadImage("arrow-disabled-down.png") }
			};

			ArrowDarkCollapsed = new GUIStyle(ArrowCollapsed)
			{
				normal = { background = Elements.LoadImage("arrow-disabled-right.png") },
				hover = { background = Elements.LoadImage("arrow-disabled-right.png") }
			};

			ArrowDarkExpanded = new GUIStyle(ArrowExpanded)
			{
				normal = { background = Elements.LoadImage("arrow-disabled-down.png") },
				hover = { background = Elements.LoadImage("arrow-disabled-down.png") }
			};
		}
	}
}