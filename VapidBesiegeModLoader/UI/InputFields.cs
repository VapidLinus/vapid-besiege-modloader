using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class InputFields
	{
		public GUIStyle Default { get; set; }

		internal InputFields()
		{
			Default = new GUIStyle()
			{
				normal = { background = Elements.LoadImage("background-dark.png"), textColor = Elements.Colors.DefaultText },
				font = GUI.skin.font,
				alignment = TextAnchor.UpperLeft,
				border = new RectOffset(4, 4, 4, 4),
				padding = new RectOffset(10, 10, 10, 10),
				margin = new RectOffset(10, 10, 10, 10),
				fontSize = 14
			};
		}
	}
}