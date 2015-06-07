using UnityEngine;

namespace Vapid.ModLoader
{
	public class VapidModLoader : MonoBehaviour
	{
		string log = "";

		void Awake()
		{
			Application.RegisterLogCallback(OnLog);
			DontDestroyOnLoad(this);

			gameObject.AddComponent<Loader>();

			Debug.Log("Vapid's ModLoader activated.");
		}

		void OnLevelWasLoaded(int level)
		{
			GameState.Instance.InvokeOnLevelLoaded(level, Application.loadedLevelName);
		}

		void OnLog(string log, string trace, LogType type)
		{
			this.log += string.Format("[{0}] {1}\n", type.ToString(), log);
		}

		void OnGUI()
		{
			var rect = new Rect(20, 40, 2000, 2000);
			GUI.Label(rect, log);
		}
	}
}