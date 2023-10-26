using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotInteractiveUnitInfo : UnitInfo
{
	private const int NotInteractiveUnitInfoCost = int.MaxValue;

	public NotInteractiveUnitInfo() : base(NotInteractiveUnitInfoCost)
	{
		
	}
}
