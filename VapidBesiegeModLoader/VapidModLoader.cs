using UnityEngine;
using Vapid.ModLoader.API;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	public class VapidModLoader : SingleInstance<VapidModLoader>
	{
		public override string Name { get { return "Vapid's ModLoader"; } }

		void Awake()
		{
			Debug.Log(Name + " activated.");
			DontDestroyOnLoad(this);

			InitializeModules();

			// Load and activate mods
			gameObject.AddComponent<Loader>();
		}

		void InitializeModules()
		{
			VGUI.Initialize();

			Console.Initialize();
			GameInspector.Initialize();
			BlockPrefabs.Initialize();
		}

		void OnLevelWasLoaded(int level)
		{
			GameState.Instance.InvokeOnLevelLoaded(level, Application.loadedLevelName);
		}

		/// <summary>
		/// Sets a monobehaviour's gameobject not to destroy on load and also makes it's gameobject child of the modloader gameobject.
		/// </summary>
		/// <param name="mono"></param>
		internal static void ActivateModule(MonoBehaviour mono)
		{
			DontDestroyOnLoad(mono.gameObject);
			mono.transform.parent = Instance.transform;
		}
	}
}