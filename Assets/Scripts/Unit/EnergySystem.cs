using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnergySystem
{
    public const int NormalMoveCostEnergy = 1;

    public static UnitInfo CreateUnitInfo(UnitObjectType unitObjectType,int pushCost = 0,int destroyCost = 0)
    {
        switch (unitObjectType) 
        {
            case UnitObjectType.Tree:
                return new TreeUnitInfo(pushCost, destroyCost);

            case UnitObjectType.Stone:
                return new StoneUnitInfo(pushCost, destroyCost);

            case UnitObjectType.NotInteractive:
                return new NotInteractiveUnitInfo();

            case UnitObjectType.Default:
                return new DefaultUnitInfo();

            default:
                return new DefaultUnitInfo();
        }
    }

    

    public static bool IsMoveCondition() 
    {
        if (PlayerCondition.GetInstance().EnergyValue <= 0)  
        {
            return false;
        }

        return true;
    }

    private static void SetCostToEnergy(int cost)
    {
        PlayerCondition.GetInstance().EnergyValue -= cost;
    }

    public static void NormalMoveCost(int cost = NormalMoveCostEnergy) 
    {
        SetCostToEnergy(NormalMoveCostEnergy);
    }

    public static void LockMoveCost(int cost) 
    {
        SetCostToEnergy(cost);
    }

    public static void DestroyMoveCost(int cost) 
    {
        SetCostToEnergy(cost);
    }

    public static void ThroughMoveCost(int cost) 
    {
        SetCostToEnergy(cost);
    }


    /// <summary>
    /// 尝试锁定子UnitObject物体，并设置其父物体为Player
    /// </summary>
    /// <param name="lockUnitObject"></param>
    /// <param name="playerObject"></param>
    /// <param name="playerState"></param>
    /// <returns></returns>
    public static bool TryToLockUnitObject(UnitObject lockUnitObject, GameObject playerObject, PlayerState playerState)
    {
        if (lockUnitObject == null || lockUnitObject.UnitInfo.CanTryThisState(UnitObjectState.Push) == false)
        {
            return false;
        }

        if (lockUnitObject.UnitInfo.GetState() == UnitObjectState.Ready || playerState == PlayerState.LockMove)
        {
            lockUnitObject.UnitInfo.ChangeState(UnitObjectState.Lock);
            lockUnitObject.transform.SetParent(playerObject.transform);

            return true;
        }

        Debug.LogWarning("[UnitAction]：更改" + lockUnitObject.gameObject.name + "物体的状态失败");
        return false;
    }

    /// <summary>
    /// 尝试解锁UnitObject
    /// </summary>
    /// <param name="lockUnitObject"></param>
    /// <param name="unitObjectParent"></param>
    /// <returns></returns>
    public static bool TryToUnLockUnitObject(UnitObject lockUnitObject, GameObject unitObjectParent)
    {
        if (lockUnitObject == null || lockUnitObject.UnitInfo.CanTryThisState(UnitObjectState.Push) == false)
        {
            return false;
        }

        if (lockUnitObject.UnitInfo.GetState() == UnitObjectState.Lock)
        {
            lockUnitObject.UnitInfo.ChangeState(UnitObjectState.Ready);

            Debug.Log("当前UnitObject的Parent为：" + unitObjectParent.name);
            lockUnitObject.transform.SetParent(unitObjectParent.transform);
        }

        Debug.LogWarning("[UnitAction]：更改" + lockUnitObject.gameObject.name + "物体的状态失败");
        return false;
    }

    public static bool TryToUnLockUnitObject(UnitObject lockUnitObject)
    {
        if (lockUnitObject == null || lockUnitObject.UnitInfo.CanTryThisState(UnitObjectState.Push) == false)
        {
            return false;
        }

        if (lockUnitObject.UnitInfo.GetState() == UnitObjectState.Lock)
        {
            lockUnitObject.UnitInfo.ChangeState(UnitObjectState.Ready);
        }

        Debug.LogWarning("[UnitAction]：更改" + lockUnitObject.gameObject.name + "物体的状态失败");
        return false;
    }


    public static void TryToDestrouUnitObject(UnitObject unitObject) 
    {
        if (unitObject == null) 
        {
            return;
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            if (unitObject.UnitInfo.CanTryThisState(UnitObjectState.Destroy) == true) 
            {
                DestroyMoveCost(unitObject.UnitInfo.DestroyEnergyCost);
                TryToUnLockUnitObject(unitObject);
                unitObject.gameObject.transform.position = new Vector3(10000, 10000, 10000);
                GameAudioManager.Instance().PlaySound(2);
                //GameObject.Destroy(unitObject.gameObject);
            }
        }
    }
}
