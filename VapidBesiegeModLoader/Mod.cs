using Vapid.ModLoader.API;

namespace Vapid.ModLoader
{
	internal class Mod
	{
		/// <summary>
		/// The mod instance.
		/// </summary>
		public UserMod UserMod { get; private set; }

		/// <summary>
		/// Whether the mod is active.
		/// </summary>
		public bool IsActive { get; private set; }

		/// <summary>
		/// Wrapper for a mod and it's active state.
		/// Initializes as unactivated. Call Activate() to load mod.
		/// </summary>
		/// <param name="mod">The mod.</param>
		public Mod(UserMod mod)
		{
			UserMod = mod;
			IsActive = false;
		}

		/// <summary>
		/// Activates the mod.
		/// This will tell the mod to load all it's resources and start applying changes.
		/// </summary>
		public void Activate()
		{
			if (IsActive) return;
			IsActive = true;

			UserMod.OnLoad();
		}

		/// <summary>
		/// Deactivates the mod.
		/// This will tell the mod to unload all it's resources and revert it's changes.
		/// </summary>
		public void Deactivate()
		{
			if (!IsActive) return;
			IsActive = false;

			UserMod.OnUnload();
		}
	}
}