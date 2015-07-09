﻿using System.Collections.Generic;
using UnityEngine;

namespace Vapid.ModLoader
{
	internal class HierarchyEntry
	{
		public readonly Transform Transform;
		public readonly HashSet<HierarchyEntry> Children;

		public bool HasChildren { get { return Children.Count > 0; } }
		public bool IsExpanded { get; set; }

		public HierarchyEntry(Transform transform)
		{
			Transform = transform;
			Children = new HashSet<HierarchyEntry>();

			UpdateChildrenList();
		}

		void UpdateChildrenList()
		{
			Children.Clear();
			foreach (Transform child in Transform)
			{
				Children.Add(new HierarchyEntry(child));
			}
		}
	}
}