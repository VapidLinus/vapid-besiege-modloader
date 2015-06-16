using UnityEngine;

namespace Vapid.ModLoader
{
	public static class VColor
	{
		public static Color FromRGB255(float r, float g, float b)
		{
			return new Color(r / 255f, g / 255f, b / 255f);
		}
	}
}