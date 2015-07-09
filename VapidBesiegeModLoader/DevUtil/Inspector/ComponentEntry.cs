using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Vapid.ModLoader
{
	internal class ComponentEntry
	{
		public readonly Component Component;
		public bool IsExpanded;

		public List<MemberValue> Properties = new List<MemberValue>();
		public List<MemberValue> Fields = new List<MemberValue>();

		public ComponentEntry(Component component)
		{
			Component = component;
			IsExpanded = false;

			foreach (var property in component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				Properties.Add(new MemberValue(Component, property));
			}
			foreach (var field in component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				Fields.Add(new MemberValue(Component, field));
			}
		}
	}
}