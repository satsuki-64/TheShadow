using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefaultUnitInfo : UnitInfo
{
	private const int DefaultUnitInfoCost = 1;

	public DefaultUnitInfo() : base(DefaultUnitInfoCost)
	{
		
	}
}
