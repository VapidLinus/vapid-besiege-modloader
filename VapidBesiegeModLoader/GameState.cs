namespace Vapid.ModLoader
{
	public class GameState : SingleInstance<GameState>
	{
		public override string Name { get { return "Vapid's GameState"; } }

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