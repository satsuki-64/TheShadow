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
        //��Ϊ�ĸ���ײ�ж���ͬһ���ű���������Ҫ��ʼ��������ײ�����
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
        //������״̬�£�����泯�������ײ��ǰ�Ƶ�λ���ȣ����������󷵻�ԭλ��
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
