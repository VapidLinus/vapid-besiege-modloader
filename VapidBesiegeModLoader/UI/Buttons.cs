using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Buttons
	{
		public GUIStyle Default { get; set; }
		public GUIStyle ArrowExpanded { get; set; }
		public GUIStyle ArrowCollapsed { get; set; }
		public GUIStyle ArrowDarkExpanded { get; set; }
		public GUIStyle ArrowDarkCollapsed { get; set; }

		internal Buttons()
		{
			var textColor = Elements.Colors.DefaultText;

			Default = new GUIStyle()
			{
				normal = { background = Elements.LoadImage("blue-normal.png"), textColor = textColor },
				hover = { background = Elements.LoadImage("blue-light.png"), textColor = textColor },
				active = { background = Elements.LoadImage("blue-dark.png"), textColor = textColor },
				border = new RectOffset(4, 4, 4, 4),
				padding = new RectOffset(10, 10, 10, 10),
				margin = new RectOffset(10, 10, 10, 10),
				alignment = TextAnchor.MiddleCenter,
				fontSize = 14,
				fontStyle = FontStyle.Bold
			};

			ArrowCollapsed = new GUIStyle()
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