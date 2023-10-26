using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitInfo
{
	[Header("能量消耗")]
	public readonly int PushEnergyCost = 0;
	public readonly int DestroyEnergyCost = 0;

	[Header("重量")]
	public int Mass = 1;

	//物体的可动作状态链表
	protected List<UnitObjectState> objectCanStateList;

	private UnitObjectState state;
	
	private UnitInfo() { }

	protected UnitInfo(int pushCost,int destroyCost = 0) 
	{
		this.state = UnitObjectState.Ready;
		this.PushEnergyCost = pushCost;
		this.DestroyEnergyCost = destroyCost;

		objectCanStateList = new List<UnitObjectState>();
	}
	
	public UnitObjectState GetState()
	{
		return state;
	}

	public void ChangeState(UnitObjectState newUnitObjectState)
	{
		this.state = newUnitObjectState;
	}

	public void InitCanStateList(bool canPush, bool canThrough, bool canDestroy, UnitObjectType unitObjectType)
	{
		this.objectCanStateList.Clear();

		if (unitObjectType == UnitObjectType.NotInteractive) 
		{
			this.objectCanStateList.Add(UnitObjectState.NotInteractive);
		}
		if (canPush == true)
		{
			this.objectCanStateList.Add(UnitObjectState.Push);
		}

		if (canThrough == true)
		{
			this.objectCanStateList.Add(UnitObjectState.Through);
		}

		if (canDestroy == true)
		{
			this.objectCanStateList.Add(UnitObjectState.Destroy);
		}
	}

	public bool CanTryThisState(UnitObjectState unitObjectState) 
	{
		if (objectCanStateList.Contains(unitObjectState)) 
		{
			return true;
		}

		return false;
	}

	public bool CanTryThisState(UnitObjectState unitObjectState1, UnitObjectState unitObjectState2)
	{
		if (objectCanStateList.Contains(unitObjectState1) && objectCanStateList.Contains(unitObjectState2))
		{
			return true;
		}

		return false;
	}
}
