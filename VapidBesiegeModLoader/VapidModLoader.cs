using UnityEngine;

namespace Vapid.ModLoader
{
    public class VapidModLoader : MonoBehaviour
    {
		string log = "";

		void Awake()
		{
			Application.RegisterLogCallback(OnLog);

			Debug.Log("VapidModLoader activated.");

			DontDestroyOnLoad(this);
		}

		void OnLog(string log, string trace, LogType type)
		{
			this.log += string.Format("[{0}] {1}\n", type.ToString(), log);
		}

		void OnGUI()
		{
			var rect = new Rect(20, 40, 200, 2000);
			GUI.Label(rect, log);
		}
    }
}