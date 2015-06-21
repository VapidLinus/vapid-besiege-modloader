using System;
using UnityEngine;
using Vapid.ModLoader.UI;

namespace Vapid.ModLoader
{
	internal class LogEntry
	{
		public readonly LogType type;
		public readonly string log;
		public readonly string trace;

		public bool IsExpanded { get; set; }

		internal LogEntry(LogType type, string log, string trace)
		{
			this.type = type;
			this.log = log;
			this.trace = trace.TrimEnd(Environment.NewLine.ToCharArray()); // Remove new line at end of string

			IsExpanded = false;
		}

		public Color Color
		{
			get
			{
				switch (type)
				{
					case LogType.Log: return Elements.Colors.LogNormal;
					case LogType.Warning: return Elements.Colors.LogWarning;
					case LogType.Error: return Elements.Colors.LogError;
					case LogType.Exception: return Elements.Colors.LogException;
					case LogType.Assert: return Elements.Colors.LogAssert;

					default: throw new NotImplementedException();
				}
			}
		}
	}
}