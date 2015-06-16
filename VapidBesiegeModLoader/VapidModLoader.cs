using UnityEngine;

namespace Vapid.ModLoader
{
	public class VapidModLoader : SingleInstance<VapidModLoader>
	{
		public override string Name { get { return "Vapid's ModLoader"; } }

		void Awake()
		{
			Debug.Log(Name + " activated.");
			DontDestroyOnLoad(this);

			Console.Initialize();

			// Load and activate mods
			gameObject.AddComponent<Loader>();
		}

		void OnLevelWasLoaded(int level)
		{
			GameState.Instance.InvokeOnLevelLoaded(level, Application.loadedLevelName);
		}
	}
}