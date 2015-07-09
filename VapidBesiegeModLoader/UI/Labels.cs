using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Labels
	{
		public GUIStyle Default { get; set; }
		public GUIStyle LogEntry { get; set; }
		public GUIStyle LogEntryTitle { get; set; }
		public GUIStyle Title { get; set; }

		internal Labels()
		{
			Default = new GUIStyle
			{
				font = GUI.skin.font,
				normal = { textColor = Elements.Colors.DefaultText }
			};

			LogEntry = new GUIStyle(Default)
			{
				margin = { top = 2 },
				alignment = TextAnchor.MiddleLeft
			};

			Title = new GUIStyle(Default)
			{
				fontStyle = FontStyle.Bold
			};

			LogEntryTitle = new GUIStyle(LogEntry)
			{
				fontStyle = Title.fontStyle
			};
		}
	}
}