namespace Vapid.ModLoader
{
	public abstract class UserMod
	{
		/// <summary>
		/// The mod's name. Should be in all lower case and use underscore as spaces.
		/// This name should never be changed once mod is published.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// The mod's display name. Any symbols allowed.
		/// This name may be changed freely after mod is published.
		/// </summary>
		public abstract string DisplayName { get; }

		/// <summary>
		/// The mod's author.
		/// If the mod has several developers seperate them with commas.
		/// </summary>
		public abstract string Author { get; }

		/// <summary>
		/// The mod's version.
		/// </summary>
		public abstract string ModVersion { get; }

		/// <summary>
		/// The mod's targeted game version.
		/// </summary>
		public abstract string GameVersion { get; }

		/// <summary>
		/// Called when your mod is loaded.
		/// It's recommended that you create a new GameObject from here that controls your mod.
		/// </summary>
		public abstract void OnLoad();

		/// <summary>
		/// Called when your mod is unloaded.
		/// Make sure you destroy any GameObjects related to your mod here.
		/// If you have any config files or other resources that needs to be saved, do this here.
		/// </summary>
		public abstract void OnUnload();
	}
}