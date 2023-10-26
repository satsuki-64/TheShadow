using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Trigger
{
    up, 
    down, 
    left, 
    right,
    other
}

public class PlayerBlockJudge : MonoBehaviour
{
    private Trigger trigger;
    private Vector3 triggerForward;
    private int unitLength = 1;
    private bool isLock = true;

    private void Start()
    {
        //因为四个碰撞盒都是同一个脚本，所以需要初始化各个碰撞体的量
        if (this.gameObject.name == "TriggerUp")
        {
            trigger = Trigger.up;
            triggerForward = new Vector3(-1, 0, 0);
        }

        if (this.gameObject.name == "TriggerDown")
        {
            trigger = Trigger.down;
            triggerForward = new Vector3(1, 0, 0);
        }

        if (this.gameObject.name == "TriggerLeft")
        {
            trigger = Trigger.left;
            triggerForward = new Vector3(0, 0, -1);
        }

        if (this.gameObject.name == "TriggerRight")
        {
            trigger = Trigger.right;
            triggerForward = new Vector3(0, 0, 1);
        }
    }

    private void Update()
    {
        //当锁定状态下，玩家面朝方向的碰撞盒前移单位长度，锁定结束后返回原位置
        if(UnitAction.isLock && isLock)
        {
            if(UnitAction.blockTrigger == trigger)
            {
                UnitAction.Move(this.transform, triggerForward, unitLength);
                isLock = false;
                UnitAction.blockTrigger = Trigger.other;
            }
        }
        if(!UnitAction.isLock && !isLock)
        {
            UnitAction.Move(this.transform, triggerForward, -unitLength);
            isLock = true;
            UnitAction.blockTrigger = Trigger.other;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.Through) == false)
                UnitAction.isBlock[(int)trigger] = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.Through) == false)
                UnitAction.isBlock[(int)trigger] = false;
        }
    }
}
