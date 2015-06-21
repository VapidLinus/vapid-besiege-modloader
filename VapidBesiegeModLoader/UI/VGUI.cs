using UnityEngine;
using Vapid.ModLoader.API;

namespace Vapid.ModLoader.UI
{
	public class VGUI : SingleInstance<VGUI>
	{
		public override string Name { get { return "Vapid's GUI Util"; } }

		/// <summary>
		/// The GUISkin used by the modloader and all mod's default config menus.
		/// This value can be used by mods if they want to look consistent to the modloader and other mods.
		/// The value can also be changed to change the skin for all that uses this.
		/// </summary>
		public static GUISkin Skin
		{
			get { return Instance._skin; }
			set { Instance._skin = value; }
		}
		private GUISkin _skin;

		/// <summary>
		/// Returns whether the mouse is hovering over any Unity GUI elements.
		/// <para>Not implemented.</para>
		/// </summary>
		public static bool IsMouseTouchingGUI { get; set; }

		void Awake()
		{
			DontDestroyOnLoad(this);

			Skin = ScriptableObject.CreateInstance<GUISkin>();
			Skin.window = Elements.Windows.Default;
			Skin.label = Elements.Labels.Default;
			Skin.button = Elements.Buttons.Default;
			Skin.textField = Skin.textArea = Elements.InputFields.Default;
			Skin.horizontalScrollbar = Elements.Scrollview.Horizontal;
			Skin.verticalScrollbar = Elements.Scrollview.Vertical;
			Skin.verticalScrollbarThumb = Elements.Scrollview.ThumbVertical;
			Skin.horizontalScrollbar = Elements.Scrollview.Horizontal;
			Skin.horizontalScrollbarThumb = Elements.Scrollview.ThumbHorizontal;
			Skin.scrollView = Elements.Windows.ClearDark;
		}
	}
}