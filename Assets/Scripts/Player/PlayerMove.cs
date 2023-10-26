using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState 
{
    NormalMove,
    LockMove,
    StopMove
}

public enum PlayerMoveDirect
{
    Forward,
    Back,
    Left,
    Right
}

public class PlayerMove : MonoBehaviour
{
	[Header("玩家运动")]
	[Header("移动速度")]
    public float PlayerMoveSpeed = 1;

	[Header("运动状态")]
    public PlayerState PlayerMoveState = PlayerState.NormalMove;

    [Header("方向")]
    public PlayerMoveDirect PlayerMoveDirect = PlayerMoveDirect.Forward;


	[Space]
    

	[HideInInspector]
    public bool IsUnitTrigger = false;

    public bool isBlock = false;

    [Header("当前UnitObject")]
    [SerializeField]
    private UnitObject unitObject = null;

    public UnitObject UnitObject 
    {
        get 
        {
            return unitObject;
        }
        
        set 
        {
            unitObject = value;
        }
    }

	[Header("所有UnitObject的父物体")]
    public GameObject UnitObjectParent = null;

    [Header("触发器物体")]
    public GameObject PlayerTrigger = null;


	[SerializeField]
    private GameObject playerObject;

	[Header("运动间隔时间")]
    public float moveIntervalTime = 0.25f;

	[SerializeField]
    private float calMoveTime = 0f;

	[SerializeField]
    private bool isMoveIntervalTime = true;

    private bool isInputMove = false;

    private void Start()
    {
        UnitAction.PlayerMoveDirect = PlayerMoveDirect.Forward;

        if (UnitObjectParent == null) 
        {
            UnitObjectParent = GameObject.Find("UnitObjectParent");    
        }

        if (this.gameObject.name == "Player" || this.gameObject.tag == "Player")
        {
            playerObject = this.gameObject;
        }
        else 
        {
            //暴力寻找
            playerObject = GameObject.Find("Player");
        }

        playerObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    
    private void Update()
    {
        //确定玩家是否进入锁定状态
        if (IsUnitTrigger == true && UnitObject != null && Input.GetMouseButton(0)) 
        {
            PlayerMoveState = PlayerState.LockMove;
        }
        else 
        {
            PlayerMoveState = PlayerState.NormalMove;
        }

        //尝试锁定物体
        if (Input.GetMouseButtonDown(0))
        {
            EnergySystem.TryToLockUnitObject(UnitObject, playerObject, PlayerMoveState);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EnergySystem.TryToUnLockUnitObject(UnitObject, UnitObjectParent);
        }

        //判断玩家是否可以有能量进行动作
        if (EnergySystem.IsMoveCondition() == false)
        {
            PlayerMoveState = PlayerState.StopMove;
            return;
        }

        //尝试摧毁物体
        EnergySystem.TryToDestrouUnitObject(UnitObject);

        //获得用户输入
        isInputMove = UnitAction.PlayerInput(PlayerMoveState, ref PlayerMoveDirect);

        //add by zhao:判断前方是否有物体阻挡
        isBlock = UnitAction.IsLockJudging(PlayerMoveDirect, PlayerMoveState);

        //判断上下次运动间隔时间
        isMoveIntervalTime = UnitAction.MoveTimeCal(ref calMoveTime, moveIntervalTime);

        //执行运动
        if (isInputMove == true && isMoveIntervalTime == true) 
        {
            UnitAction.PlayerMove(playerObject, PlayerTrigger, PlayerMoveDirect, isBlock, PlayerMoveState, PlayerMoveSpeed);
            isMoveIntervalTime = false;
            calMoveTime = 0f;

            //计算消耗的能量值
            switch (PlayerMoveState) 
            {
                case PlayerState.StopMove:

                    break;

                case PlayerState.LockMove:
                    EnergySystem.LockMoveCost(UnitObject.UnitInfo.PushEnergyCost);
                    break;
            }
        }


    }
}
