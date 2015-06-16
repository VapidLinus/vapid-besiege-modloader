using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Labels
	{
		public GUIStyle Default { get; set; }

		internal Labels()
		{
			Default = new GUIStyle()
			{
				normal = { textColor = Elements.Colors.DefaultText }
			};
		}
	}
}