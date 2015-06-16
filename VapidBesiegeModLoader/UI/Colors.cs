﻿using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Colors
	{
		public Color DefaultText { get; set; }
		public Color LogNormal { get; set; }
		public Color LogWarning { get; set; }
		public Color LogError { get; set; }
		public Color LogException { get; set; }
		public Color LogAssert { get; set; }

		internal Colors()
		{
			DefaultText = Color.white;

			LogNormal = DefaultText;
			LogWarning = VColor.FromRGB255(240, 76, 23);
			LogError = VColor.FromRGB255(238, 78, 16);
			LogException = LogWarning;
			LogAssert = VColor.FromRGB255(191, 88, 203);
		}
	}
}