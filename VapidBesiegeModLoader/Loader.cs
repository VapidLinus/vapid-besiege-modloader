using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Vapid.ModLoader
{
	internal class Loader : MonoBehaviour
	{
		private readonly List<Mod> mods = new List<Mod>();

		/// <summary>
		/// Returns a list of all loaded mods.
		/// The returned list is a copy of the internal list and may be modified.
		/// </summary>
		public List<Mod> Mods { get { return new List<Mod>(mods); } }

		void Awake()
		{
			DontDestroyOnLoad(this);

			/* Begin the fun!
			 * First make sure all mods are loaded.
			 * Once they're all loaded, activate all mods. */
			LoadMods();
			ActivateMods();
		}

		void OnDestroy()
		{
			/* If the loader is destroyed, deactivate all mods.
			 * This method is also called when the game is exiting
			 * which is useful and this lets the mods save their
			 * configs and deactivate properly. */
			DeactivateMods();
		}

		private void LoadMods()
		{
			try
			{
				// Find all DLL files in the mods directory
				string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, "Mods"), "*.dll", SearchOption.TopDirectoryOnly);
				foreach (string file in files)
				{
					// Ignore the modloader itself
					if (file.Contains("VapidModLoader")) continue;

					Debug.Log("Loading: " + Path.GetFileName(file));
					try
					{
						var assembly = Assembly.LoadFrom(file);

						// Find instance of UserMod in assembly
						UserMod userMod = null;
						foreach (var type in assembly.GetTypes())
						{
							if (!type.IsSubclassOf(typeof(UserMod))) continue;

							// Make sure we haven't already found an instance of IUserMode
							if (userMod == null)
							{
								userMod = System.Activator.CreateInstance(type) as UserMod;
							}
							else
							{
								// Too many IUserMods instances
								Debug.LogWarning("Mod has more than one instance of UserMod. Using first one.");
								break;
							}
						}

						// No instance of UserMod was found
						if (userMod == null)
						{
							Debug.LogWarning("Mod had no instance of UserMod. Skipping.");
							continue;
						}

						// Activate the mod!
						mods.Add(new Mod(userMod));
					}
					catch (Exception e)
					{
						Debug.LogError("Failed to load mod.");
						Debug.LogException(e);
					}
				}
			}
			catch (Exception e)
			{
				Debug.LogError("Failed to load mods.");
				Debug.LogException(e);
			}
		}

		private void ActivateMods()
		{
			foreach (var mod in mods)
			{
				mod.Activate();
			}
		}

		private void DeactivateMods()
		{
			foreach (var mod in mods)
			{
				mod.Deactivate();
			}
		}
	}
}