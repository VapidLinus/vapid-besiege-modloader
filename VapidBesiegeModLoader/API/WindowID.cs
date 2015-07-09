using System;

namespace Vapid.ModLoader.API
{
	public static class WindowID
	{
		private static int current = Int32.MaxValue;

		/// <summary>
		/// Returns a Window ID.
		/// <para>Garunteed to not conflict with other windows getting an ID from this method.</para>
		/// </summary>
		/// <returns></returns>
		public static int Next()
		{
			return current--;
		}
	}
}