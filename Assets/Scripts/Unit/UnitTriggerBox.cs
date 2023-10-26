using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTriggerBox : MonoBehaviour
{
	[SerializeField]
	private PlayerMove playerMove;

	private GameObject unitObject = null;
	private bool isUnitTrigger = false;

	private void Start()
	{
		if (playerMove == null) 
		{
			playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "UnitObject")
		{
			if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.NotInteractive) == false) 
			{
				if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.Through) == false)
				{
					isUnitTrigger = true;
					unitObject = other.gameObject;

					playerMove.IsUnitTrigger = true;
					playerMove.UnitObject = unitObject.GetComponent<UnitObject>();
				}
			}	
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "UnitObject")
		{
			if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.NotInteractive) == false) 
			{
				if (other.GetComponent<UnitObject>().UnitInfo.CanTryThisState(UnitObjectState.Through) == false)
				{
					isUnitTrigger = false;
					unitObject = null;

					playerMove.IsUnitTrigger = false;
					playerMove.UnitObject = null;
				}
			}
		}
	}
}
