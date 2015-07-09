using UnityEngine;
using Vapid.ModLoader.API;

namespace Vapid.ModLoader.UI
{
	public class Colors
	{
		public Color DefaultText { get; set; }
		public Color LowlightText { get; set; }

		public Color TypeText { get; set; }

		public Color LogNormal { get; set; }
		public Color LogWarning { get; set; }
		public Color LogError { get; set; }
		public Color LogException { get; set; }
		public Color LogAssert { get; set; }

		internal Colors()
		{
			DefaultText = Color.white;
			LowlightText = VColor.FromRGB255(200, 200, 200);

			TypeText = VColor.FromRGB255(78, 201, 176);

			LogNormal = DefaultText;
			LogWarning = VColor.FromRGB255(240, 76, 23);
			LogError = VColor.FromRGB255(238, 78, 16);
			LogException = LogWarning;
			LogAssert = VColor.FromRGB255(191, 88, 203);
		}
	}
}