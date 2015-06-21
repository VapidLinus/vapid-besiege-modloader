using Vapid.ModLoader.API;

namespace Vapid.ModLoader
{
	public class RecieverTest
	{
		/// <summary>
		/// Called from the injected modloader when the simulation state is toggled.
		/// </summary>
		/// <param name="simulating"></param>
		public static void RecieveSimulationToggle(bool simulating)
		{
			GameState.Instance.InvokeOnSimulateToggle(simulating);
		}
	}
}