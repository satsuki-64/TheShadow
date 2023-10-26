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
	[Header("����˶�")]
	[Header("�ƶ��ٶ�")]
    public float PlayerMoveSpeed = 1;

	[Header("�˶�״̬")]
    public PlayerState PlayerMoveState = PlayerState.NormalMove;

    [Header("����")]
    public PlayerMoveDirect PlayerMoveDirect = PlayerMoveDirect.Forward;


	[Space]
    

	[HideInInspector]
    public bool IsUnitTrigger = false;

    public bool isBlock = false;

    [Header("��ǰUnitObject")]
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

	[Header("����UnitObject�ĸ�����")]
    public GameObject UnitObjectParent = null;

    [Header("����������")]
    public GameObject PlayerTrigger = null;


	[SerializeField]
    private GameObject playerObject;

	[Header("�˶����ʱ��")]
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
            //����Ѱ��
            playerObject = GameObject.Find("Player");
        }

        playerObject.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    
    private void Update()
    {
        //ȷ������Ƿ��������״̬
        if (IsUnitTrigger == true && UnitObject != null && Input.GetMouseButton(0)) 
        {
            PlayerMoveState = PlayerState.LockMove;
        }
        else 
        {
            PlayerMoveState = PlayerState.NormalMove;
        }

        //������������
        if (Input.GetMouseButtonDown(0))
        {
            EnergySystem.TryToLockUnitObject(UnitObject, playerObject, PlayerMoveState);
        }
        if (Input.GetMouseButtonUp(0))
        {
            EnergySystem.TryToUnLockUnitObject(UnitObject, UnitObjectParent);
        }

        //�ж�����Ƿ�������������ж���
        if (EnergySystem.IsMoveCondition() == false)
        {
            PlayerMoveState = PlayerState.StopMove;
            return;
        }

        //���Դݻ�����
        EnergySystem.TryToDestrouUnitObject(UnitObject);

        //����û�����
        isInputMove = UnitAction.PlayerInput(PlayerMoveState, ref PlayerMoveDirect);

        //add by zhao:�ж�ǰ���Ƿ��������赲
        isBlock = UnitAction.IsLockJudging(PlayerMoveDirect, PlayerMoveState);

        //�ж����´��˶����ʱ��
        isMoveIntervalTime = UnitAction.MoveTimeCal(ref calMoveTime, moveIntervalTime);

        //ִ���˶�
        if (isInputMove == true && isMoveIntervalTime == true) 
        {
            UnitAction.PlayerMove(playerObject, PlayerTrigger, PlayerMoveDirect, isBlock, PlayerMoveState, PlayerMoveSpeed);
            isMoveIntervalTime = false;
            calMoveTime = 0f;

            //�������ĵ�����ֵ
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
