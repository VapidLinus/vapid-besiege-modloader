using UnityEngine;

namespace Vapid.ModLoader
{
	public class GameState : MonoBehaviour
	{
		private static GameState _instance;

		/// <summary>
		/// Gets the GameState instance.
		/// A new one will be created if none exists.
		/// </summary>
		public static GameState Instance
		{
			get { return _instance ?? (_instance = new GameObject("Vapid's GameState").AddComponent<GameState>()); }
		}

		public event OnSimulateToggle OnSimulateToggle;
		public event OnLevelLoaded OnLevelLoaded;

		void Awake()
		{
			DontDestroyOnLoad(this);
		}

		internal void InvokeOnSimulateToggle(bool simulating)
		{
			var handler = OnSimulateToggle;
			if (handler != null) handler(simulating);
		}

		internal void InvokeOnLevelLoaded(int id, string name)
		{
			var handler = OnLevelLoaded;
			if (handler != null) handler(id, name);
		}
	}
}