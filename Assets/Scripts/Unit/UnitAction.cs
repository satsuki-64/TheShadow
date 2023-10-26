using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class UnitAction
{
    public static PlayerMoveDirect oldPlayerMoveDirect;

    //是否向前
    private static bool isForward = true;

    //和运动模块交互的变量
    public static PlayerMoveDirect PlayerMoveDirect;

    //输入判定
    public static bool PlayerInput(PlayerState playerState,ref PlayerMoveDirect playerMoveDirect)
    {
        if (playerState == PlayerState.NormalMove) 
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                playerMoveDirect = PlayerMoveDirect.Left;
                oldPlayerMoveDirect = playerMoveDirect;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                playerMoveDirect = PlayerMoveDirect.Back;
                oldPlayerMoveDirect = playerMoveDirect;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerMoveDirect = PlayerMoveDirect.Right;
                oldPlayerMoveDirect = playerMoveDirect;
                return true;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                playerMoveDirect = PlayerMoveDirect.Forward;
                oldPlayerMoveDirect = playerMoveDirect;
                return true;
            }

            return false;
        }

        if (playerState == PlayerState.LockMove) 
        {

            //add by zhao:这里重新写了，因为之前锁定状态下180度转向会带动物体一起转，改完的效果就是只能前进后退，不能向后或者向左右转向

            //Debug.Log("oldPlayerMoveDirect：" + oldPlayerMoveDirect);
            //Debug.Log("newPlayerMoveDirect：" + newPlayerMoveDirect);
            //oldPlayerMoveDirect = newPlayerMoveDirect;

            if (oldPlayerMoveDirect == PlayerMoveDirect.Left) 
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    playerMoveDirect = PlayerMoveDirect.Left;
                    isForward = true;
                    return true;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    playerMoveDirect = PlayerMoveDirect.Left;
                    isForward = false;
                    return true;
                }
            }

            if (oldPlayerMoveDirect == PlayerMoveDirect.Right)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    playerMoveDirect = PlayerMoveDirect.Right;
                    isForward = false;
                    return true;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    playerMoveDirect = PlayerMoveDirect.Right;
                    isForward = true;
                    return true;
                }
            }

            if (oldPlayerMoveDirect == PlayerMoveDirect.Forward)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    playerMoveDirect = PlayerMoveDirect.Forward;
                    isForward = false;
                    return true;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    playerMoveDirect = PlayerMoveDirect.Forward;
                    isForward = true;
                    return true;
                }
            }

            if (oldPlayerMoveDirect == PlayerMoveDirect.Back)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    playerMoveDirect = PlayerMoveDirect.Back;
                    isForward = true;
                    return true;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    playerMoveDirect = PlayerMoveDirect.Back;
                    isForward = false;
                    return true;
                }
            }

            return false;
        }

        return false;
    }
    
    public static void AlignUnitObject(UnitObject unitObject, float unitLength)
    {
        Vector3Int ratioIntNtemp = Vector3Int.CeilToInt(unitObject.transform.position / unitLength);
        Vector3 ratioIntN = ratioIntNtemp;
        Vector3 difference = unitObject.transform.position / unitLength - ratioIntN;
        Vector3 temp = Vector3.zero;
        for (int i = 0; i <= 2; i++)
        {
            if (difference[i] <= 0.5)
                temp[i] = 0;
            else
                temp[i] = 1;
        }
        unitObject.GetUnitObject().transform.position = (ratioIntN + temp) * unitLength;
    }

    public static void Move(Transform moveTransform, Vector3 forwardDirect, float speed)
    {
        moveTransform.Translate(forwardDirect * speed, Space.World);
    }


    public static void PlayerMove(GameObject player,GameObject playerTrigger, PlayerMoveDirect playerMoveDirect, bool isBlock, PlayerState playerState = PlayerState.NormalMove, float speed = 1f)
    {
        //移动以及转动方向
        Vector3 playerMoveForward = Vector3.zero;
        Vector3 playerRotateForward = Vector3.zero;

        switch (playerMoveDirect)
        {
            case PlayerMoveDirect.Forward:
                playerRotateForward = new Vector3(0, 0, 0);
                playerMoveForward = Vector3.forward;
                break;
            case PlayerMoveDirect.Left:
                playerRotateForward = new Vector3(0, -90, 0);
                playerMoveForward = Vector3.left;
                break;
            case PlayerMoveDirect.Back:
                playerRotateForward = new Vector3(0, -180, 0);
                playerMoveForward = Vector3.back;
                break;
            case PlayerMoveDirect.Right:
                playerRotateForward = new Vector3(0, -270, 0);
                playerMoveForward = Vector3.right;
                break;
        }

        //执行旋转，然后位移
        if (playerState == PlayerState.NormalMove)
        {
            player.transform.rotation = Quaternion.Euler(playerRotateForward);

            if (!isBlock) 
            {
                GameAudioManager.Instance().PlaySound(0);
                Move(player.transform, playerMoveForward, speed);
            }
        }

		if (playerState == PlayerState.LockMove) 
        {
            //add by zhao:锁定状态下前进的时候判断前方是否阻挡，后退的时候判断后方是否阻挡
            if (isForward && !isBlock)
            {
                GameAudioManager.Instance().PlaySound(1);
                Move(player.transform, playerMoveForward, speed);
            }
            else if (!isForward && !IsPlayerBlock(TranslateBackDirection(playerMoveDirect)))
            {
                GameAudioManager.Instance().PlaySound(1);
                Move(player.transform, playerMoveForward, -speed);
            }
        }
        
        //同步碰撞盒物体和玩家的位置
        UnitAction.TriggerPositionCorrect(playerTrigger);
    }


	public static void UnitObjectMove(UnitObject unitObject, GameObject playerObject, PlayerMoveDirect playerMoveDirect)
	{
		if (unitObject.UnitInfo.GetState() == UnitObjectState.Lock)
		{
            unitObject.transform.SetParent(playerObject.transform);
		}
	}

    public static bool MoveTimeCal(ref float calMoveTime, float moveIntervalTime)
    {
        calMoveTime += Time.deltaTime;

        if (calMoveTime >= moveIntervalTime)
        {
            return true;
        }

        return false;
    }
    
	#region 东凯编写部分

	//上、下、左、右四个方向上是否被阻挡
	public static bool[] isBlock = new bool[] { false, false, false, false };
    //是否允许碰撞盒物体进入锁定模式
    public static bool isLock = false;
    //被锁定方向上的触发器
    public static Trigger blockTrigger = Trigger.other;


    //add by zhao:判断前方是否有物体阻挡
    public static bool IsLockJudging(PlayerMoveDirect playerMoveDirect, PlayerState playerState)
    {
        int triggerDirection = 0;
        switch (playerMoveDirect)
        {
            case PlayerMoveDirect.Forward:
                triggerDirection = 3;
                break;
            case PlayerMoveDirect.Left:
                triggerDirection = 0;
                break;
            case PlayerMoveDirect.Back:
                triggerDirection = 2;
                break;
            case PlayerMoveDirect.Right:
                triggerDirection = 1;
                break;
        }
        if (playerState == PlayerState.NormalMove)
        {
            isLock = false;
        }

        if (playerState == PlayerState.LockMove)
        {
            isLock = true;
            switch (triggerDirection)
            {
                case 0:
                    blockTrigger = Trigger.up;
                    break;
                case 1:
                    blockTrigger = Trigger.down;
                    break;
                case 2:
                    blockTrigger = Trigger.left;
                    break;
                case 3:
                    blockTrigger = Trigger.right;
                    break;
            }
        }

        return IsPlayerBlock(triggerDirection);
    }

    //add by zhao:简单地返回参数指定的布尔数组中的值
    public static bool IsPlayerBlock(int triggerDirection)
    {
        return isBlock[triggerDirection];
    }

    //add by zhao:特殊用处的函数，用于返回玩家后方的碰撞盒在布尔数组中的参数
    public static int TranslateBackDirection(PlayerMoveDirect playerMoveDirect)
    {
        switch (playerMoveDirect)
        {
            case PlayerMoveDirect.Forward:
                return 2;
            case PlayerMoveDirect.Left:
                return 1;
            case PlayerMoveDirect.Back:
                return 3;
            case PlayerMoveDirect.Right:
                return 0;
            default:
                return -1;
        }
    }

    //将碰撞盒物体与玩家的坐标同步
    public static void TriggerPositionCorrect(GameObject PlayerTrigger)
    {
        if (PlayerTrigger == null)
            PlayerTrigger = GameObject.Find("PlayerTrigger");
        //找到玩家物体
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTrigger.transform.position = Player.transform.position;
    }

	#endregion
}
