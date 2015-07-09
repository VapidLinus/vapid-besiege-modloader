using UnityEngine;

namespace Vapid.ModLoader.API
{
	public class GameState : SingleInstance<GameState>
	{
		public override string Name { get { return "Vapid's GameState"; } }

		/// <summary>
		/// Returns an instance of AddPiece if it exists in the current scene, otherwise null.
		/// </summary>
		public static AddPiece AddPiece
		{
			get
			{
				if (_addPiece == null) _addPiece = FindObjectOfType<AddPiece>();
				if (_addPiece == null) Debug.LogWarning("No instance of AddPiece exists in this scene.");
				return _addPiece;
			}
		}
		private static AddPiece _addPiece;

		/// <summary>
		/// Returns an instance of MachineObjectTracker if it exists in the current scene, otherwise null.
		/// </summary>
		public static MachineObjectTracker MachineObjectTracker
		{
			get
			{
				if (_machineObjectTracker == null) _machineObjectTracker = FindObjectOfType<MachineObjectTracker>();
				if (_machineObjectTracker == null) Debug.LogWarning("No instance of MachineObjectTracker exists in this scene.");
				return _machineObjectTracker;
			}
		}
		private static MachineObjectTracker _machineObjectTracker;

		public event OnSimulateToggle OnSimulateToggle;
		public event OnLevelLoaded OnLevelLoaded;

		void Awake()
		{
			VapidModLoader.ActivateModule(this);
		}

		internal void InvokeOnSimulateToggle(bool simulating)
		{
			var handler = OnSimulateToggle;
			if (handler != null) handler(simulating);
		}

		internal void InvokeOnLevelLoaded(int id, string name)
		{
			var handler = OnLevelLoaded;
			if (handler != null) handler(id, name);
		}
	}
}