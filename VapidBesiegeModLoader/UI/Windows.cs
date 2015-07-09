﻿using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Windows
	{
		public GUIStyle Default { get; set; }
		public GUIStyle ClearDark { get; set; }

		internal Windows()
		{
			const int PADDING_TOP = 56;

			Default = new GUIStyle
			{
				normal = { background = Elements.LoadImage("background-44px.png"), textColor = Elements.Colors.DefaultText },
				fontSize = 16,
				fontStyle = FontStyle.Bold,
				border = new RectOffset(4, 4, 44, 4),
				padding = new RectOffset(12, 12, PADDING_TOP, 12),
				contentOffset = new Vector2(0, -PADDING_TOP + 14)
			};

			ClearDark = new GUIStyle
			{
				normal = { background = Elements.LoadImage("background-dark.png"), textColor = Elements.Colors.DefaultText },
				border = new RectOffset(4, 4, 4, 4),
				padding = Elements.Settings.DefaultPadding,
				margin = Elements.Settings.DefaultMargin
			};
		}
	}
}