using System;
using UnityEngine;

namespace Vapid.ModLoader.UI
{
	public class Tools
	{
		public bool DoCollapseArrow(bool isCollapsed, bool enabled, float width, float height, params GUILayoutOption[] options)
		{
			int length = options.Length;

			Array.Resize(ref options, options.Length + 2);
			options[length] = GUILayout.Width(width);
			options[length + 1] = GUILayout.Height(height);

			var style = enabled
				? (isCollapsed ? Elements.Buttons.ArrowCollapsed : Elements.Buttons.ArrowExpanded)
				: (isCollapsed ? Elements.Buttons.ArrowDarkCollapsed : Elements.Buttons.ArrowDarkExpanded);

			return GUILayout.Button("", style, options);
		}
	}
}
