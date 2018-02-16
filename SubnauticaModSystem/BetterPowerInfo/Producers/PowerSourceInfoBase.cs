﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BetterPowerInfo.Producers
{
	public class PowerSourceInfoBase
	{
		public virtual string DisplayText { get { return GetPowerSourceDisplayText(); } }
		public virtual int CurrentPower { get { return Mathf.RoundToInt(source.GetPower()); } }
		public virtual int MaxPower { get { return Mathf.RoundToInt(source.GetMaxPower()); } }
		public virtual float ProductionPerMinute { get { return GetPowerProductionPerMinute(); } }

		protected PowerSource source;

		public PowerSourceInfoBase(PowerSource source)
		{
			this.source = source;
		}

		protected virtual string GetPowerSourceDisplayText()
		{
			return GetDefaultPowerSourceDisplayText();
		}

		private string GetDefaultPowerSourceDisplayText()
		{
			string name = source.name.Replace("(Clone)", "").Replace("Base", "").Replace("Module", "");
			name = System.Text.RegularExpressions.Regex.Replace(name, "[A-Z]", " $0").Trim();
			return name;
		}

		protected virtual float GetPowerProductionPerMinute()
		{
			float powerProduction = 0;
			if (GetPowerProductionPerMinute(source.gameObject.GetComponent<RegeneratePowerSource>(), out powerProduction))
			{
				return powerProduction;
			}

			return 0;
		}

		protected bool GetPowerProductionPerMinute(RegeneratePowerSource source, out float result)
		{
			result = source != null ? source.regenerationAmount / (source.regenerationInterval / 60) : 0;
			return source != null;
		}
	}
}
