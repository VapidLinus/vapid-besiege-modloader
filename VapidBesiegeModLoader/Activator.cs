using UnityEngine;

namespace Vapid.ModLoader
{
	public static class Activator
	{
		private static bool activated;

		public static void Activate()
		{
			if (!activated)
			{
				new GameObject("Vapid's ModLoader").AddComponent<VapidModLoader>();
				activated = true;
			}
		}
	}
}