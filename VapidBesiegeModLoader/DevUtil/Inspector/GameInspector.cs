using UnityEngine;
using Vapid.ModLoader.API;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	public class GameInspector : SingleInstance<GameInspector>
	{
		public override string Name { get { return "Vapid's GameInspector"; } }

		public HierarchyPanel HierarchyPanel { get; private set; }
		public InspectorPanel InspectorPanel { get; private set; }

		public string WindowTitle = "Vapid's GameInspector";
		public bool PinkWindowTitle = false;

		private readonly int windowID = WindowID.Next();
		private Rect rect = new Rect(20, 20, 800, 600);

		private bool isVisible = false;

		void Awake()
		{
			VapidModLoader.ActivateModule(this);

			HierarchyPanel = gameObject.AddComponent<HierarchyPanel>();
			InspectorPanel = gameObject.AddComponent<InspectorPanel>();
		}

		void Update()
		{
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.V))
			{
				isVisible = !isVisible;
			}
		}

		void OnGUI()
		{
			GUI.skin = VGUI.Skin;

			var style = new GUIStyle(GUI.skin.window)
			{
				normal = {textColor = PinkWindowTitle ? Color.magenta : GUI.skin.window.normal.textColor}
			};

			if (isVisible)
				rect = GUILayout.Window(windowID, rect, DoWindow, WindowTitle, style);
		}

		void DoWindow(int id)
		{
			GUI.DragWindow(new Rect(0, 0, rect.width, GUI.skin.window.padding.top));

			// Display panels
			GUILayout.BeginHorizontal();
			HierarchyPanel.Display();
			InspectorPanel.Display();
			GUILayout.EndHorizontal();
		}
	}
}