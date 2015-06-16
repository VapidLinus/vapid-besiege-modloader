using UnityEngine;

namespace Vapid.ModLoader
{
	public abstract class SingleInstance<T> : MonoBehaviour where T : SingleInstance<T>
	{
		/// <summary>
		/// The name the GameObject will be given when intialized.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// Returns an instance of object type.
		/// </summary>
		public static T Instance
		{
			get { return instance ?? (instance = CreateOrFind()); }
		}

		private static T instance;

		/// <summary>
		/// Initializes an instance of this type if one doesn't exist.
		/// Useful when you want to make sure this objects exists but don't need a reference to it.
		/// </summary>
		public static void Initialize()
		{
			if (instance == null)
			{
				instance = CreateOrFind();
			}
		}

		private static T CreateOrFind()
		{
			T[] instances = FindObjectsOfType<T>();

			// Warn if there are too many instances
			if (instances.Length > 1)
			{
				Debug.LogWarning("Too many instances of " + typeof(T).Name + ".");
			}

			// If already exists
			if (instances.Length == 1)
			{
				// Return one that already exists
				return instances[0];
			}

			// Create new one and rename it's GameObject
			var t = new GameObject("SingleInstance<" + typeof(T).Name + "> temp").AddComponent<T>();
			t.gameObject.name = t.Name;
			return t;
		}
	}
}