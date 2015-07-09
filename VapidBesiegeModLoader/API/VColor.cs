using UnityEngine;

namespace Vapid.ModLoader.API
{
	public static class VColor
	{
		/// <summary>
		/// Creates a color where input is 0-255 intead of 0-1.
		/// </summary>
		/// <param name="r">Red attribute. 0-255.</param>
		/// <param name="g">Green attribute. 0-255.</param>
		/// <param name="b">Blue attribute. 0-255.</param>
		/// <returns></returns>
		public static Color FromRGB255(float r, float g, float b)
		{
			return new Color(r / 255f, g / 255f, b / 255f);
		}
	}
}