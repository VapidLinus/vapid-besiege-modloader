using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Windows
	{
		public GUIStyle Default { get; set; }
		public GUIStyle ClearDark { get; set; }

		internal Windows()
		{
			const int PADDING_TOP = 64;

			Default = new GUIStyle
			{
				normal = { background = Elements.LoadImage("background-44px.png"), textColor = Elements.Colors.DefaultText },
				fontSize = 16,
				fontStyle = FontStyle.Bold,
				border = new RectOffset(4, 4, 44, 4),
				padding = new RectOffset(20, 20, PADDING_TOP, 20),
				contentOffset = new Vector2(0, -PADDING_TOP + 14)
			};

			ClearDark = new GUIStyle()
			{
				normal = { background = Elements.LoadImage("background-dark.png"), textColor = Elements.Colors.DefaultText },
				border = new RectOffset(4, 4, 4, 4),
				padding = new RectOffset(13, 13, 13, 13)
			};
		}
	}
}