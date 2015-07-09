using System;
using System.Collections.Generic;
using UnityEngine;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	public class InspectorPanel : MonoBehaviour
	{
		/* This code is bad, ugly, unreadable, undocumented and unefficent but fuctional for now.
		 * TODO: Refactor structure */

		enum FieldType
		{
			Normal, VectorX, VectorY, VectorZ
		}

		private const string FIELD_EDIT_INPUT_NAME = "field_edit_input";

		private MemberValue activeMember;
		private FieldType activeMemberFieldType = FieldType.Normal;
		private object activeMemberNewValue;

		private GameObject _selectedGameObject;

		/// <summary>
		/// The selected game object.
		/// <para>Can be set to null.</para>
		/// </summary>
		public GameObject SelectedGameObject
		{
			get { return _selectedGameObject; }
			set
			{
				_selectedGameObject = value;
				entries.Clear();

				if (value != null)
				{
					foreach (var component in value.GetComponents<Component>())
					{
						entries.Add(new ComponentEntry(component));
					}
				}
			}
		}

		/// <summary>
		/// Whether a gameobject is selected.
		/// </summary>
		public bool IsGameObjectSelected
		{
			get { return SelectedGameObject != null; }
		}

		private readonly HashSet<ComponentEntry> entries = new HashSet<ComponentEntry>();
		private Vector2 inspectorScroll = Vector2.zero;

		void OnGUI()
		{
			if (activeMember != null && Event.current.keyCode == KeyCode.Return)
			{
				object @object = activeMember.GetValue();

				if (@object is string || @object is bool)
				{
					activeMember.SetValue(activeMemberNewValue);
				}
				else if (@object is Vector3)
				{
					Vector3 vector3 = (Vector3)activeMember.GetValue();
					float v;
					if (activeMemberNewValue != null && float.TryParse(activeMemberNewValue.ToString(), out v))
					{
						if (activeMemberFieldType == FieldType.VectorX) vector3.x = v;
						else if (activeMemberFieldType == FieldType.VectorY) vector3.y = v;
						else if (activeMemberFieldType == FieldType.VectorZ) vector3.z = v;
					}
					activeMember.SetValue(vector3);
				}

				// Reset variables
				activeMember = null;
				activeMemberFieldType = FieldType.Normal;
				activeMemberNewValue = null;
			}
		}

		public void Display()
		{
			float panelWidth = Elements.Settings.InspectorPanelWidth;

			GUILayout.BeginHorizontal(GUILayout.Width(panelWidth), GUILayout.ExpandWidth(false));

			GUILayout.BeginVertical();

			if (IsGameObjectSelected)
			{
				// TODO: Measure performance impact from assigning it every redraw versus checking if it was changed and then assign
				SelectedGameObject.name = GUILayout.TextField(SelectedGameObject.name, Elements.InputFields.ThinNoTopBotMargin, GUILayout.Width(panelWidth), GUILayout.ExpandWidth(false));
			}
			else
			{
				GUILayout.TextField("Select a gameobject in the hierarchy", Elements.InputFields.ThinNoTopBotMargin, GUILayout.Width(panelWidth), GUILayout.ExpandWidth(false));
			}

			inspectorScroll = GUILayout.BeginScrollView(inspectorScroll, GUILayout.Width(panelWidth), GUILayout.ExpandWidth(false));
			GUILayout.Label("Components:", Elements.Labels.Title);

			foreach (var entry in new HashSet<ComponentEntry>(entries))
			{
				// Remove entries that have been deleted
				if (entry.Component == null)
				{
					entries.Remove(entry);
					continue;
				}
				DisplayComponent(entry);
			}

			GUILayout.EndScrollView();

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();
		}

		private void DisplayComponent(ComponentEntry entry)
		{
			GUILayout.BeginHorizontal();
			if (Elements.Tools.DoCollapseArrow(entry.IsExpanded))
			{
				entry.IsExpanded = !entry.IsExpanded;
			}
			GUILayout.TextField(entry.Component.GetType().Name, Elements.Labels.LogEntry); // Show component name
			GUILayout.EndHorizontal();

			// Don't display properties and fields if entry is collapsed
			if (!entry.IsExpanded) return;

			ShowFields("Properties", entry.Properties);
			ShowFields("Fields", entry.Fields);
		}

		void ShowFields(string title, IEnumerable<MemberValue> fields)
		{
			GUILayout.BeginHorizontal();
			Elements.Tools.Indent();
			GUILayout.TextField(title, Elements.Labels.LogEntryTitle);
			GUILayout.EndHorizontal();

			bool hasDisplayedField = false;
			foreach (var member in fields)
			{
				// Ignore properties with no setter and obsolete attributes
				if (!member.HasSetter || member.IsObsolete) continue;

				hasDisplayedField = true;

				GUILayout.BeginHorizontal();
				Elements.Tools.Indent();

				object value = member.GetValue();
				string name = member.Name;
				Type type = member.Type;

				GUILayout.BeginVertical();

				#region Display variable name, type and value
				GUILayout.BeginHorizontal();

				bool canModifyType = IsSupported(value);
				if (Elements.Tools.DoCollapseArrow(member.IsExpanded, canModifyType))
				{
					if (canModifyType)
						member.IsExpanded = !member.IsExpanded;
				}
				
				var typeStyle = new GUIStyle(Elements.Labels.LogEntry)
				{
					fontStyle = FontStyle.Bold,
					normal = { textColor = Elements.Colors.TypeText }
				};
				var nameStyle = new GUIStyle(Elements.Labels.LogEntry)
				{
					normal = { textColor = Elements.Colors.DefaultText * .8f }
				};

				GUILayout.Label(type.Name , typeStyle, GUILayout.ExpandWidth(false));
				GUILayout.Label(" " + name + ":", Elements.Labels.LogEntry, GUILayout.ExpandWidth(false));
				GUILayout.Label(" " + (value == null ? " null" : value.ToString()), nameStyle, GUILayout.ExpandWidth(false));

				GUILayout.EndHorizontal();
				#endregion

				if (member.IsExpanded)
				{
					GUILayout.BeginHorizontal();
					Elements.Tools.Indent();

					var stringValue = value as string;
					if (stringValue != null)
					{
						DoInputField(member, stringValue, FieldType.Normal);
					}
					else if (value is bool)
					{
						if (GUILayout.Button(value.ToString(), Elements.Buttons.ComponentField, GUILayout.Width(60)))
						{
							member.SetValue(!(bool)value);
						}
					}
					else if (value is Vector3)
					{
						Vector3 vector3Value = (Vector3) value;

						// X
						GUILayout.BeginHorizontal();
						GUILayout.Label("X: ", Elements.Labels.LogEntry, GUILayout.ExpandWidth(false));
						DoInputField(member, vector3Value.x, FieldType.VectorX, 80);
						GUILayout.EndHorizontal();
						// Y
						GUILayout.BeginHorizontal();
						GUILayout.Label("Y: ", Elements.Labels.LogEntry, GUILayout.ExpandWidth(false));
						DoInputField(member, vector3Value.y, FieldType.VectorY, 80);
						GUILayout.EndHorizontal();
						// Z
						GUILayout.BeginHorizontal();
						GUILayout.Label("Z: ", Elements.Labels.LogEntry, GUILayout.ExpandWidth(false));
						DoInputField(member, vector3Value.z, FieldType.VectorZ, 80);
						GUILayout.EndHorizontal();
					}
					else
					{
						Debug.LogWarning("[InspectorPanel] Trying to display unsupported type.");
						GUILayout.Label("Unsupported type.");
					}
					GUILayout.EndHorizontal();
				}

				GUILayout.EndVertical();

				GUILayout.EndHorizontal();
			}

			// Display "None" if we didn't display any fields
			if (hasDisplayedField) return;

			GUILayout.BeginHorizontal();
			Elements.Tools.Indent();
			GUILayout.Label("None");
			GUILayout.EndHorizontal();
		}

		void DoInputField(MemberValue member, object value, FieldType fieldType, float width = 0)
		{
			GUILayoutOption option = width > 0 ? GUILayout.Width(width) : GUILayout.ExpandWidth(true);

			// If this one is selected
			if (activeMember == member && activeMemberFieldType == fieldType)
			{
				GUI.SetNextControlName(FIELD_EDIT_INPUT_NAME + member.ID);
				activeMemberNewValue = GUILayout.TextField((string)activeMemberNewValue, Elements.InputFields.ComponentField, option);
			}
			else
			{
				string oldValue = value.ToString();

				GUI.SetNextControlName(FIELD_EDIT_INPUT_NAME + member.ID);
				string newValue = GUILayout.TextField(oldValue, Elements.InputFields.ComponentField, option);

				// Input was changed
				if (oldValue != newValue)
				{
					// Set current member to the active one
					activeMember = member;
					activeMemberFieldType = fieldType;
					activeMemberNewValue = newValue;
				}
			}
		}

		private static bool IsSupported(object value)
		{
			return value is string || value is bool || value is Vector3;
		}
	}
}