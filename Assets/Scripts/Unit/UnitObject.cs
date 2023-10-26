using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ԫ���嵱ǰ״̬
public enum UnitObjectState 
{
	Ready,
	Lock,
	Push,
	Destroy,
	Through,
	NotInteractive
}

//��Ԫ��������
public enum UnitObjectType 
{
	Default,
	NotInteractive,
	Stone,
	Tree,
}

[RequireComponent(typeof(BoxCollider))]
public partial class UnitObject : MonoBehaviour
{
	[Header("Unit��������")]
	public UnitObjectType unitObjectType;

	[HideInInspector]
	public UnitInfo UnitInfo;

	public bool CanPush;
	public bool CanThrough;
	public bool CanDestroy;


	[Header("�ƶ�����")]
	public int PushCost;

	[Header("�ݻ�����")]
	public int DestroyCost;

	private GameObject unitGameObject;

	private void Awake()
	{
		if (this.gameObject != null) 
		{
			unitGameObject = this.gameObject;
		}

		if (this.gameObject.tag != "UnitObject") 
		{
			this.gameObject.tag = "UnitObject";
		}

		UnitInfo = EnergySystem.CreateUnitInfo(unitObjectType, PushCost,DestroyCost);
		UnitInfo.InitCanStateList(CanPush, CanThrough, CanDestroy, unitObjectType);

		if (this.gameObject.GetComponent<BoxCollider>()==false) 
		{
			this.gameObject.AddComponent<BoxCollider>();
		}

		this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
	}
	
	public GameObject GetUnitObject() 
	{
		if (unitGameObject != null)  
		{
			return unitGameObject;
		}

		Debug.LogWarning("UnitObject������");
		return null;
	}
}
